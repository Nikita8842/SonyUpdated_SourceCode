using AMBOModels.GlobalAccessible;
using AMBOModels.IncentiveManagement;
using AMBOModels.Reports;
using AMBOModels.UserValidation;
using AMBOWeb.Classes;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMBOWeb.Controllers
{
    public class DeviationController : SonyBaseController
    {
        private readonly IAPIDeviationData _APIDeviationData;
        private readonly IAPIIncentiveData _APIIncentiveData;
        private readonly IAPICommon _APICommon;
        private readonly IAPIMappingData _APIMappingData;

        public DeviationController(IAPIDeviationData IAPIDeviationData, IAPIIncentiveData IAPIIncentiveData, IAPICommon IAPICommon, IAPIMappingData IAPIMappingData)
        {
            _APIDeviationData = IAPIDeviationData;
            _APIIncentiveData = IAPIIncentiveData;
            _APICommon = IAPICommon;
            _APIMappingData = IAPIMappingData;
        }

        [HttpGet]
        public virtual ActionResult Download(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            else
                return new EmptyResult();
        }

        #region Deviation Upload By RDI
        [HttpGet]
        public ActionResult UploadByRDI()
        {
            var userDetails = (BaseSession)(Session["BaseSession"]);
            if (userDetails.RoleName == "RDI")
                ViewBag.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
            else
                ViewBag.BranchId = 0;
            return View();
        }

        [HttpPost]
        public ActionResult UploadByRDIDownloadExcel(DeviationUploadByRDISearch objSearchData)
        {
            BasePerPieceIncentiveReportInputParam objGridData = new BasePerPieceIncentiveReportInputParam();
            objGridData.Month = objSearchData.IncentiveMonth.Replace("/","");

            GetBranch BInputParam = new GetBranch();
            BInputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            //if (rolen == "RDI")
            //    objGridData.BranchId = Convert.ToInt32(_APICommon.GetBranchByUserId(BInputParam).BranchId);

            objGridData.BranchId = objSearchData.BranchId;

            var output = _APIIncentiveData.GetBasePerPieceIncentiveReport(objGridData);

            DataTable dt = AMBOWeb.Classes.Common.ToDataTable(output.Data.BasePerPieceIncentiveData);
            //var output = _APIDeviationData.GetDeviationUploadByRDIExcel(objSearchData);
            if (dt != null)
            {
                dt.Columns.Remove("FinalPayableAmount");
                //dt.Columns.Remove("ApprovedDeviationAmount");
                dt.Columns.Remove("HORemark");
            }
            //dt.Columns.Remove("DeviationStage");
            string filePath = Server.MapPath("~/DownloadTemplate/DeviationUploadByRDI.xlsx");

            if (output.MessageCode == (int)Acceptable.Found)
            {
                using (XLWorkbook wb = new XLWorkbook(filePath))
                {

                    var sheet1 = wb.Worksheets.Add(dt,"DeviationUploadByRDI");
                    //sheet1.Position = 1;
                    //sheet1.FirstCell().InsertTable(dt, false);
                    sheet1.Table("Table1").ShowAutoFilter = false;

                    wb.Worksheet(1).Position = wb.Worksheets.Count() + 1;


                    string handle = Guid.NewGuid().ToString();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        stream.Position = 0;
                        TempData[handle] = stream.ToArray();
                        return Json(new
                        {
                            status = true,
                            FileGuid = handle,
                            FileName = "ProposedDeviationData.xlsx"
                        });
                    }
                }
            }
            else
                return Json(new
                {
                    status = false,
                    message = output.Message,
                    FileGuid = "",
                    FileName = ""
                });
        }

        [HttpPost]
        public ActionResult UploadByRDIDeviationReasons(DeviationUploadByRDIExcel objGridData)
        {
            return View("UploadByRDI",objGridData);
        }

        [HttpPost]
        public ActionResult GetSavedDeviationReasons(DeviationUploadByRDISearch objSearchData)
        {
            var output = _APIDeviationData.GetDeviationUploadByRDIReasons(objSearchData);
            if(output.MessageCode != (int)Acceptable.Created)
            {
                output.Data = new DeviationUploadByRDIExcel();
                AddErrorNotification(output.Message);
            }
            return View("UploadByRDI", output.Data);
        }

        [HttpPost]
        public ActionResult UploadByRDISaveDeviationReasons(DeviationUploadByRDISaveReasons objGridData)
        {
            var output = _APIDeviationData.ManageDeviationUploadByRDI_SaveReasons(objGridData);
            return Json(output);
        }

        [HttpPost]
        public ActionResult UploadByRDIApproveDeviationReasons(DeviationUploadByRDISaveReasons objGridData)
        {
            var output = _APIDeviationData.ManageDeviationUploadByRDI_ApproveReasons(objGridData);
            return Json(output);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UploadByRDIUploadExcel(HttpPostedFileBase ExcelUploadFile,string IncentiveMonth)
        {
            string filePath = "";
            if (ExcelUploadFile == null || ExcelUploadFile.ContentLength == 0)
            {
                AddErrorNotification("Please select an excel file for upload.");
                return new RedirectResult(Url.Action("UploadByRDI"));
            }

            if (!(Path.GetExtension(ExcelUploadFile.FileName) == ".xls" || Path.GetExtension(ExcelUploadFile.FileName) == ".xlsx"))
            {
                AddErrorNotification("Only .xls and .xlsx file extensions supported.");
                return new RedirectResult(Url.Action("UploadByRDI"));
            }

            filePath = Server.MapPath("~/Uploads/") + Path.GetFileNameWithoutExtension(ExcelUploadFile.FileName) + '_' + objUserSession.UserId + Path.GetExtension(ExcelUploadFile.FileName);
            ExcelUploadFile.SaveAs(filePath);
            DeviationUploadByRDIExcel objData = new DeviationUploadByRDIExcel();
            objData.Month = IncentiveMonth;
            objData.Flag = 0;
            objData.UserId = Convert.ToInt32(objUserSession.UserId);

            //objData.ExcelData = new DataView(ConvertExcelToDataTable(filePath)).ToTable(false, "SFACode","ProposedDeviation", "Reasons", "FirstHeader", "FirstRemark", "SecondHeader", "SecondRemark", "FinalPayableAmount", "HORemark", "DeviationStage");
            objData.ExcelData = new DataView(ConvertExcelToDataTable(filePath)).ToTable(false, "SFACode", "ProposedDeviation", "Reasons", "FirstHeader", "FirstRemark", "SecondHeader", "SecondRemark");
            objData.ExcelData.Columns.Add("ApprovedDeviationAmount", typeof(Int64));
            objData.ExcelData.Columns.Add("HORemark", typeof(string));
            //objData.ExcelData.Columns.Add("DeviationStage", typeof(string));
            string message = "";
            if (!(objData.ExcelData == null || objData.ExcelData.Rows.Count == 0))
            {
                DataTable dt1 = objData.ExcelData.AsEnumerable().Where(r => r.Field<string>("ProposedDeviation") != "0").CopyToDataTable();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string Reasons = Convert.ToString(dt1.Rows[i]["Reasons"]);

                    if (Reasons == "")
                    {
                        AddErrorNotification("OOPS! Deviation reasons can not be empty.Please check.");
                        return new RedirectResult(Url.Action("UploadByRDI"));
                    }
                }
                //objData.ExcelData = objData.ExcelData.AsEnumerable().Where(r => r.Field<string>("ProposedDeviation") != "0").CopyToDataTable();
                var output = _APIDeviationData.ManageDeviationUploadByRDI(objData);
                if (output.MessageCode == (int)Acceptable.Created)
                {
                    AddSuccessNotification(output.Message);
                    return UploadByRDIDeviationReasons(output.Data);
                }
                objData.ExcelData = null;
                AddErrorNotification(output.Message);
                return new RedirectResult(Url.Action("UploadByRDI"));
            }
            objData.ExcelData = null;
            AddErrorNotification(message);
            return new RedirectResult(Url.Action("UploadByRDI"));

        }
        #endregion Deviation Upload By RDI

        #region Deviation Approval
        [HttpGet]
        public ActionResult Approval()
        {
            var userDetails = (BaseSession)(Session["BaseSession"]);
            if (userDetails.RoleName == "RDI")
                ViewBag.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
            else
                ViewBag.BranchId = 0;
            return View();
        }

        [HttpPost]
        public ActionResult ApprovalSearch(DeviationApprovalSearch objSearchData)
        {
            //var output = _APIDeviationData.GetDeviationApprovalExcel(objSearchData);

            BasePerPieceIncentiveReportInputParam objGridData = new BasePerPieceIncentiveReportInputParam();
            objGridData.Month = objSearchData.IncentiveMonth.Replace("/", "");

            GetBranch BInputParam = new GetBranch();
            BInputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            //if (rolen == "RDI")
            //    objGridData.BranchId = Convert.ToInt32(_APICommon.GetBranchByUserId(BInputParam).BranchId);

            objGridData.BranchId = objSearchData.BranchId;

            var output = _APIIncentiveData.GetBasePerPieceIncentiveReport(objGridData);

            DataTable dt = AMBOWeb.Classes.Common.ToDataTable(output.Data.BasePerPieceIncentiveData);
            //var output = _APIDeviationData.GetDeviationUploadByRDIExcel(objSearchData);
            string filePath = Server.MapPath("~/DownloadTemplate/DeviationUploadByRDI.xlsx");

            if (output.MessageCode == (int)Acceptable.Found)
            {
                using (XLWorkbook wb = new XLWorkbook(filePath))
                {
                    //wb.Worksheets.Add(dt, "Deviation Data");

                    var sheet1 = wb.Worksheets.Add(dt, "DeviationData");
                    //sheet1.Position = 1;
                    //sheet1.FirstCell().InsertTable(dt, false);
                    sheet1.Table("Table1").ShowAutoFilter = false;

                    wb.Worksheet(1).Position = wb.Worksheets.Count() + 1;

                    string handle = Guid.NewGuid().ToString();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        stream.Position = 0;
                        TempData[handle] = stream.ToArray();
                        return Json(new
                        {
                            status = true,
                            FileGuid = handle,
                            FileName = "ProposedDeviationForApproval.xlsx"
                        });
                    }
                }
            }
            else
                return Json(new
                {
                    status = false,
                    message = output.Message,
                    FileGuid = "",
                    FileName = ""
                });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Approval(HttpPostedFileBase ExcelUploadFile, string IncentiveMonth)
        {
            string filePath = "";
            if (ExcelUploadFile == null || ExcelUploadFile.ContentLength == 0)
            {
                AddErrorNotification("Please select an excel file for upload.");
                return new RedirectResult(Url.Action("Approval"));
            }

            if (!(Path.GetExtension(ExcelUploadFile.FileName) == ".xls" || Path.GetExtension(ExcelUploadFile.FileName) == ".xlsx"))
            {
                AddErrorNotification("Only .xls and .xlsx file extensions supported.");
                return new RedirectResult(Url.Action("Approval"));
            }

            filePath = Server.MapPath("~/Uploads/") + Path.GetFileNameWithoutExtension(ExcelUploadFile.FileName) + '_' + objUserSession.UserId + Path.GetExtension(ExcelUploadFile.FileName);
            ExcelUploadFile.SaveAs(filePath);
            DeviationApprovalExcel objData = new DeviationApprovalExcel();
            objData.Month = IncentiveMonth;
            objData.Flag = 1;
            objData.UserId = Convert.ToInt32(objUserSession.UserId);

            objData.ExcelData = new DataView(ConvertExcelToDataTable(filePath)).ToTable(false, "SFACode", "ProposedDeviation", "Reasons", "FirstHeader", "FirstRemark", "SecondHeader", "SecondRemark", "ApprovedDeviationAmount", "HORemark");
            //objData.ExcelData.Columns.Add("Status", typeof(Int64));
            //foreach (DataRow dr in objData.ExcelData.Rows)
            //{
            //    if (Convert.ToString(dr["Approve_Reject"]).Trim().ToUpper() == "APPROVE")
            //        dr["Status"] = 1;
            //    else if (Convert.ToString(dr["Approve_Reject"]).Trim().ToUpper() == "REJECT")
            //        dr["Status"] = 2;
            //    else
            //        dr["Status"] = 0;
            //}
            //objData.ExcelData = new DataView(objData.ExcelData).ToTable(false, "SFACode", "ProposedDeviation", "Reasons", "FirstHeader", "FirstRemark", "SecondHeader", "SecondRemark", "FinalPayableAmount", "HORemark", "DeviationStage", "Status");
            string message = "";
            if (!(objData.ExcelData == null || objData.ExcelData.Rows.Count == 0))
            {
                //objData.ExcelData = objData.ExcelData.AsEnumerable().Where(r => r.Field<Int64>("Status") != 0).CopyToDataTable();
                DataTable dt1 = objData.ExcelData.AsEnumerable().Where(r => r.Field<string>("ProposedDeviation") != "0").CopyToDataTable();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string Reasons = Convert.ToString(dt1.Rows[i]["Reasons"]);

                    if (Reasons == "")
                    {
                        AddErrorNotification("OOPS! Deviation reasons can not be empty.Please check.");
                        return new RedirectResult(Url.Action("Approval"));
                    }
                }
                var output = _APIDeviationData.UploadDeviationApprovalExcel(objData);
                if (output.MessageCode == (int)Acceptable.Created)
                {
                    AddSuccessNotification(output.Message);
                    return new RedirectResult(Url.Action("Approval"));
                }
                objData.ExcelData = null;
                AddErrorNotification(output.Message);
                return new RedirectResult(Url.Action("Approval"));
            }
            objData.ExcelData = null;
            AddErrorNotification(message);
            return new RedirectResult(Url.Action("Approval"));
        }
        #endregion Deviation Approval

        #region Deviation FinalUpload
        [HttpGet]
        public ActionResult FinalUpload()
        {
            var userDetails = (BaseSession)(Session["BaseSession"]);
            if (userDetails.RoleName == "RDI")
                ViewBag.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
            else
                ViewBag.BranchId = 0;
            return View();
        }

        [HttpPost]
        public ActionResult FinalUploadSearch(DeviationFinalUploadSearch objSearchData)
        {
            BasePerPieceIncentiveReportInputParam objGridData = new BasePerPieceIncentiveReportInputParam();
            objGridData.Month = objSearchData.IncentiveMonth.Replace("/", "");

            GetBranch BInputParam = new GetBranch();
            BInputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            //if (rolen == "RDI")
            //    objGridData.BranchId = Convert.ToInt32(_APICommon.GetBranchByUserId(BInputParam).BranchId);

            objGridData.BranchId = objSearchData.BranchId;

            var output = _APIIncentiveData.GetBasePerPieceIncentiveReport(objGridData);

            DataTable dt = AMBOWeb.Classes.Common.ToDataTable(output.Data.BasePerPieceIncentiveData);
            string filePath = Server.MapPath("~/DownloadTemplate/DeviationUploadByRDI.xlsx");


            //var output = _APIDeviationData.GetDeviationFinalUploadExcel(objSearchData);
            if (output.MessageCode == (int)Acceptable.Found)
            {
                using (XLWorkbook wb = new XLWorkbook(filePath))
                {
                    //wb.Worksheets.Add(dt, "Deviation Data");
                    var sheet1 = wb.Worksheets.Add(dt, "DeviationData");
                    //sheet1.Position = 1;
                    //sheet1.FirstCell().InsertTable(dt, false);
                    sheet1.Table("Table1").ShowAutoFilter = false;

                    wb.Worksheet(1).Position = wb.Worksheets.Count() + 1;

                    string handle = Guid.NewGuid().ToString();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        stream.Position = 0;
                        TempData[handle] = stream.ToArray();
                        return Json(new
                        {
                            status = true,
                            FileGuid = handle,
                            FileName = "FinalDeviationData.xlsx"
                        });
                    }
                }
            }
            else
                return Json(new
                {
                    status = false,
                    message = output.Message,
                    FileGuid = "",
                    FileName = ""
                });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FinalUpload(HttpPostedFileBase ExcelUploadFile, string IncentiveMonth)
        {
            string filePath = "";
            if (ExcelUploadFile == null || ExcelUploadFile.ContentLength == 0)
            {
                AddErrorNotification("Please select an excel file for upload.");
                return new RedirectResult(Url.Action("FinalUpload"));
            }

            if (!(Path.GetExtension(ExcelUploadFile.FileName) == ".xls" || Path.GetExtension(ExcelUploadFile.FileName) == ".xlsx"))
            {
                AddErrorNotification("Only .xls and .xlsx file extensions supported.");
                return new RedirectResult(Url.Action("FinalUpload"));
            }

            filePath = Server.MapPath("~/Uploads/") + Path.GetFileNameWithoutExtension(ExcelUploadFile.FileName) + '_' + objUserSession.UserId + Path.GetExtension(ExcelUploadFile.FileName);
            ExcelUploadFile.SaveAs(filePath);
            DeviationFinalUploadExcel objData = new DeviationFinalUploadExcel();
            objData.Month = IncentiveMonth;
            objData.Flag = 2;
            objData.UserId = Convert.ToInt32(objUserSession.UserId);

            objData.ExcelData = new DataView(ConvertExcelToDataTable(filePath)).ToTable(false, "SFACode", "ProposedDeviation", "Reasons", "FirstHeader", "FirstRemark", "SecondHeader", "SecondRemark", "ApprovedDeviationAmount", "HORemark");

            //objData.ExcelData = new DataView(ConvertExcelToDataTable(filePath)).ToTable(false, "Record Id", "HO Approved Deviation", "HO Remark");
            string message = "";
            if (!(objData.ExcelData == null || objData.ExcelData.Rows.Count == 0))
            {
                DataTable dt1 = objData.ExcelData.AsEnumerable().Where(r => r.Field<string>("ProposedDeviation") != "0").CopyToDataTable();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string Reasons = Convert.ToString(dt1.Rows[i]["Reasons"]);

                    if (Reasons == "")
                    {
                        AddErrorNotification("OOPS! Deviation reasons can not be empty.Please check.");
                        return new RedirectResult(Url.Action("FinalUpload"));
                    }
                }
                var output = _APIDeviationData.UploadDeviationFinalUploadExcel(objData);
                if (output.MessageCode == (int)Acceptable.Created)
                {
                    AddSuccessNotification(output.Message);
                    return new RedirectResult(Url.Action("FinalUpload"));
                }
                objData.ExcelData = null;
                AddErrorNotification(output.Message);
                return new RedirectResult(Url.Action("FinalUpload"));
            }
            objData.ExcelData = null;
            AddErrorNotification(message);
            return new RedirectResult(Url.Action("FinalUpload"));
        }
        #endregion Deviation FinalUpload

        #region FestivalIncentiveDeviation
        #region Deviation Upload By RDI
        [HttpGet]
        public ActionResult UploadByRDI_Festival()
        {
            var userDetails = (BaseSession)(Session["BaseSession"]);
            if (userDetails.RoleName == "RDI")
                ViewBag.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
            else
                ViewBag.BranchId = 0;
            return View();
        }

        [HttpPost]
        public ActionResult UploadByRDIDownloadExcel_Festival(DeviationUploadByRDISearch objSearchData)
        {
            FestivalIncentiveReportInputParam objGridData = new FestivalIncentiveReportInputParam();
            //objGridData.Month = objSearchData.IncentiveMonth.Replace("/", "");

            GetBranch BInputParam = new GetBranch();
            BInputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            //if (rolen == "RDI")
            //    objGridData.BranchId = Convert.ToInt32(_APICommon.GetBranchByUserId(BInputParam).BranchId);

            objGridData.BranchId = objSearchData.BranchId;
            objGridData.FestivalSchemeId = objSearchData.FestivalSchemeId;

            var output = _APIIncentiveData.GetFestivalIncentiveReport(objGridData);

            DataTable dt = AMBOWeb.Classes.Common.ToDataTable(output.Data.FestivalIncentiveData);
            //var output = _APIDeviationData.GetDeviationUploadByRDIExcel(objSearchData);
            if (dt != null)
            {
                dt.Columns.Remove("FinalPayableAmount");
                //dt.Columns.Remove("ApprovedDeviationAmount");
                dt.Columns.Remove("HORemark");
            }
            //dt.Columns.Remove("DeviationStage");
            string filePath = Server.MapPath("~/DownloadTemplate/DeviationUploadByRDI.xlsx");

            if (output.MessageCode == (int)Acceptable.Found)
            {
                using (XLWorkbook wb = new XLWorkbook(filePath))
                {

                    var sheet1 = wb.Worksheets.Add(dt, "DeviationUploadByRDI");
                    //sheet1.Position = 1;
                    //sheet1.FirstCell().InsertTable(dt, false);
                    sheet1.Table("Table1").ShowAutoFilter = false;

                    wb.Worksheet(1).Position = wb.Worksheets.Count() + 1;


                    string handle = Guid.NewGuid().ToString();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        stream.Position = 0;
                        TempData[handle] = stream.ToArray();
                        return Json(new
                        {
                            status = true,
                            FileGuid = handle,
                            FileName = "ProposedDeviationData.xlsx"
                        });
                    }
                }
            }
            else
                return Json(new
                {
                    status = false,
                    message = output.Message,
                    FileGuid = "",
                    FileName = ""
                });
        }

        [HttpPost]
        public ActionResult UploadByRDIDeviationReasons_Festival(DeviationUploadByRDIExcel objGridData)
        {
            return View("UploadByRDI_Festival", objGridData);
        }

        [HttpPost]
        public ActionResult GetSavedDeviationReasons_Festival(DeviationUploadByRDISearch objSearchData)
        {
            var output = _APIDeviationData.GetDeviationUploadByRDIReasons(objSearchData);
            if (output.MessageCode != (int)Acceptable.Created)
            {
                output.Data = new DeviationUploadByRDIExcel();
                AddErrorNotification(output.Message);
            }
            return View("UploadByRDI_Festival", output.Data);
        }

        [HttpPost]
        public ActionResult UploadByRDISaveDeviationReasons_Festival(DeviationUploadByRDISaveReasons objGridData)
        {
            var output = _APIDeviationData.ManageDeviationUploadByRDI_SaveReasons(objGridData);
            return Json(output);
        }

        [HttpPost]
        public ActionResult UploadByRDIApproveDeviationReasons_Festival(DeviationUploadByRDISaveReasons objGridData)
        {
            var output = _APIDeviationData.ManageDeviationUploadByRDI_ApproveReasons(objGridData);
            return Json(output);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UploadByRDIUploadExcel_Festival(HttpPostedFileBase ExcelUploadFile, string Id)
        {
            string filePath = "";
            if (ExcelUploadFile == null || ExcelUploadFile.ContentLength == 0)
            {
                AddErrorNotification("Please select an excel file for upload.");
                return new RedirectResult(Url.Action("UploadByRDI_Festival"));
            }

            if (!(Path.GetExtension(ExcelUploadFile.FileName) == ".xls" || Path.GetExtension(ExcelUploadFile.FileName) == ".xlsx"))
            {
                AddErrorNotification("Only .xls and .xlsx file extensions supported.");
                return new RedirectResult(Url.Action("UploadByRDI_Festival"));
            }

            filePath = Server.MapPath("~/Uploads/") + Path.GetFileNameWithoutExtension(ExcelUploadFile.FileName) + '_' + objUserSession.UserId + Path.GetExtension(ExcelUploadFile.FileName);
            ExcelUploadFile.SaveAs(filePath);
            DeviationUploadByRDIExcel objData = new DeviationUploadByRDIExcel();
            objData.FestivalSchemeId = Convert.ToInt32(Id);
            objData.Flag = 0;
            objData.UserId = Convert.ToInt32(objUserSession.UserId);

            //objData.ExcelData = new DataView(ConvertExcelToDataTable(filePath)).ToTable(false, "SFACode","ProposedDeviation", "Reasons", "FirstHeader", "FirstRemark", "SecondHeader", "SecondRemark", "FinalPayableAmount", "HORemark", "DeviationStage");
            string message = "";
            try
            {
                objData.ExcelData = new DataView(ConvertExcelToDataTable(filePath)).ToTable(false, "SFACode", "ProposedDeviation", "Reasons", "FirstHeader", "FirstRemark", "SecondHeader", "SecondRemark");

                objData.ExcelData.Columns.Add("ApprovedDeviationAmount", typeof(Int64));
                objData.ExcelData.Columns.Add("HORemark", typeof(string));
                //objData.ExcelData.Columns.Add("DeviationStage", typeof(string));

                if (!(objData.ExcelData == null || objData.ExcelData.Rows.Count == 0))
                {
                    if (objData.ExcelData.AsEnumerable().Where(r => r.Field<string>("ProposedDeviation") != "0").Count() > 0)
                    {
                        DataTable dt1 = objData.ExcelData.AsEnumerable().Where(r => r.Field<string>("ProposedDeviation") != "0").CopyToDataTable();
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string Reasons = Convert.ToString(dt1.Rows[i]["Reasons"]);

                            if (Reasons == "")
                            {
                                AddErrorNotification("OOPS! Deviation reasons can not be empty.Please check.");
                                return new RedirectResult(Url.Action("UploadByRDI_Festival"));
                            }
                        }
                        //objData.ExcelData = objData.ExcelData.AsEnumerable().Where(r => r.Field<string>("ProposedDeviation") != "0").CopyToDataTable();
                        var output = _APIDeviationData.ManageDeviationUploadByRDI_Festival(objData);
                        if (output.MessageCode == (int)Acceptable.Created)
                        {
                            AddSuccessNotification(output.Message);
                            //return UploadByRDIDeviationReasons_Festival(output.Data);
                            return new RedirectResult(Url.Action("UploadByRDI_Festival"));
                        }
                        else
                        {
                            AddErrorNotification("OOPS! Some error occured. Please check the uploaded file.");
                            return new RedirectResult(Url.Action("UploadByRDI_Festival"));
                        }
                        objData.ExcelData = null;
                        AddErrorNotification(output.Message);
                        return new RedirectResult(Url.Action("UploadByRDI_Festival"));
                    }
                    else
                    {
                        AddErrorNotification("OOPS! At least one record required with proposed deviation amount greater than 0. Please check the uploaded file.");
                        return new RedirectResult(Url.Action("UploadByRDI_Festival"));
                    }
                }
            }
            catch(Exception ex)
            {
                AddErrorNotification("OOPS! Please upload correct format as downloaded.");
                return new RedirectResult(Url.Action("UploadByRDI_Festival"));
            }
            objData.ExcelData = null;
            AddErrorNotification(message);
            return new RedirectResult(Url.Action("UploadByRDI_Festival"));

        }
        #endregion Deviation Upload By RDI

        #region Deviation Approval
        [HttpGet]
        public ActionResult Approval_Festival()
        {
            var userDetails = (BaseSession)(Session["BaseSession"]);
            if (userDetails.RoleName == "RDI")
                ViewBag.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
            else
                ViewBag.BranchId = 0;
            return View();
        }

        [HttpPost]
        public ActionResult ApprovalSearch_Festival(DeviationApprovalSearch objSearchData)
        {
            //var output = _APIDeviationData.GetDeviationApprovalExcel(objSearchData);

            FestivalIncentiveReportInputParam objGridData = new FestivalIncentiveReportInputParam();
            //objGridData.Month = objSearchData.IncentiveMonth.Replace("/", "");

            GetBranch BInputParam = new GetBranch();
            BInputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            //if (rolen == "RDI")
            //    objGridData.BranchId = Convert.ToInt32(_APICommon.GetBranchByUserId(BInputParam).BranchId);

            objGridData.BranchId = objSearchData.BranchId;
            objGridData.FestivalSchemeId = objSearchData.FestivalSchemeId;

            var output = _APIIncentiveData.GetFestivalIncentiveReport(objGridData);

            DataTable dt = AMBOWeb.Classes.Common.ToDataTable(output.Data.FestivalIncentiveData);
            //var output = _APIDeviationData.GetDeviationUploadByRDIExcel(objSearchData);
            string filePath = Server.MapPath("~/DownloadTemplate/DeviationUploadByRDI.xlsx");

            if (output.MessageCode == (int)Acceptable.Found)
            {
                using (XLWorkbook wb = new XLWorkbook(filePath))
                {
                    //wb.Worksheets.Add(dt, "Deviation Data");

                    var sheet1 = wb.Worksheets.Add(dt, "DeviationData");
                    //sheet1.Position = 1;
                    //sheet1.FirstCell().InsertTable(dt, false);
                    sheet1.Table("Table1").ShowAutoFilter = false;

                    wb.Worksheet(1).Position = wb.Worksheets.Count() + 1;

                    string handle = Guid.NewGuid().ToString();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        stream.Position = 0;
                        TempData[handle] = stream.ToArray();
                        return Json(new
                        {
                            status = true,
                            FileGuid = handle,
                            FileName = "ProposedDeviationForApproval.xlsx"
                        });
                    }
                }
            }
            else
                return Json(new
                {
                    status = false,
                    message = output.Message,
                    FileGuid = "",
                    FileName = ""
                });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Approval_Festival(HttpPostedFileBase ExcelUploadFile, string Id)
        {
            string filePath = "";
            if (ExcelUploadFile == null || ExcelUploadFile.ContentLength == 0)
            {
                AddErrorNotification("Please select an excel file for upload.");
                return new RedirectResult(Url.Action("Approval_Festival"));
            }

            if (!(Path.GetExtension(ExcelUploadFile.FileName) == ".xls" || Path.GetExtension(ExcelUploadFile.FileName) == ".xlsx"))
            {
                AddErrorNotification("Only .xls and .xlsx file extensions supported.");
                return new RedirectResult(Url.Action("Approval_Festival"));
            }

            filePath = Server.MapPath("~/Uploads/") + Path.GetFileNameWithoutExtension(ExcelUploadFile.FileName) + '_' + objUserSession.UserId + Path.GetExtension(ExcelUploadFile.FileName);
            ExcelUploadFile.SaveAs(filePath);
            DeviationApprovalExcel objData = new DeviationApprovalExcel();
            objData.FestivalSchemeId = Convert.ToInt32(Id);
            objData.Flag = 1;
            objData.UserId = Convert.ToInt32(objUserSession.UserId);
            string message = "";
            try
            {
                objData.ExcelData = new DataView(ConvertExcelToDataTable(filePath)).ToTable(false, "SFACode", "ProposedDeviation", "Reasons", "FirstHeader", "FirstRemark", "SecondHeader", "SecondRemark", "ApprovedDeviationAmount", "HORemark");
            //objData.ExcelData.Columns.Add("Status", typeof(Int64));
            //foreach (DataRow dr in objData.ExcelData.Rows)
            //{
            //    if (Convert.ToString(dr["Approve_Reject"]).Trim().ToUpper() == "APPROVE")
            //        dr["Status"] = 1;
            //    else if (Convert.ToString(dr["Approve_Reject"]).Trim().ToUpper() == "REJECT")
            //        dr["Status"] = 2;
            //    else
            //        dr["Status"] = 0;
            //}
            //objData.ExcelData = new DataView(objData.ExcelData).ToTable(false, "SFACode", "ProposedDeviation", "Reasons", "FirstHeader", "FirstRemark", "SecondHeader", "SecondRemark", "FinalPayableAmount", "HORemark", "DeviationStage", "Status");
          
                if (!(objData.ExcelData == null || objData.ExcelData.Rows.Count == 0))
                {
                    //objData.ExcelData = objData.ExcelData.AsEnumerable().Where(r => r.Field<Int64>("Status") != 0).CopyToDataTable();
                    if (objData.ExcelData.AsEnumerable().Where(r => r.Field<string>("ProposedDeviation") != "0").Count() > 0)
                    {
                        DataTable dt1 = objData.ExcelData.AsEnumerable().Where(r => r.Field<string>("ProposedDeviation") != "0").CopyToDataTable();
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string Reasons = Convert.ToString(dt1.Rows[i]["Reasons"]);

                            if (Reasons == "")
                            {
                                AddErrorNotification("OOPS! Deviation reasons can not be empty.Please check.");
                                return new RedirectResult(Url.Action("Approval_Festival"));
                            }
                        }
                        var output = _APIDeviationData.UploadDeviationApprovalExcel_Festival(objData);
                        if (output.MessageCode == (int)Acceptable.Created)
                        {
                            AddSuccessNotification(output.Message);
                            //return new RedirectResult(Url.Action("Approval_Festival"));
                            return new RedirectResult(Url.Action("Approval_Festival"));
                        }
                        else
                        {
                            AddErrorNotification("OOPS! Some error occured. Please check the uploaded file.");
                            return new RedirectResult(Url.Action("Approval_Festival"));
                        }
                        objData.ExcelData = null;
                        AddErrorNotification(output.Message);
                        return new RedirectResult(Url.Action("Approval_Festival"));
                    }
                    else
                    {
                        AddErrorNotification("OOPS! At least one record required with proposed deviation amount greater than 0. Please check the uploaded file.");
                        return new RedirectResult(Url.Action("Approval_Festival"));
                    }
                }
            }
            catch (Exception ex)
            {
                AddErrorNotification("OOPS! Please upload correct format as downloaded.");
                return new RedirectResult(Url.Action("Approval_Festival"));
            }
            objData.ExcelData = null;
            AddErrorNotification(message);
            return new RedirectResult(Url.Action("Approval_Festival"));
        }
        #endregion Deviation Approval

        #region Deviation FinalUpload
        [HttpGet]
        public ActionResult FinalUpload_Festival()
        {
            var userDetails = (BaseSession)(Session["BaseSession"]);
            if (userDetails.RoleName == "RDI")
                ViewBag.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
            else
                ViewBag.BranchId = 0;
            return View();
        }

        [HttpPost]
        public ActionResult FinalUploadSearch_Festival(DeviationFinalUploadSearch objSearchData)
        {
            FestivalIncentiveReportInputParam objGridData = new FestivalIncentiveReportInputParam();
            //objGridData.Month = objSearchData.IncentiveMonth.Replace("/", "");

            GetBranch BInputParam = new GetBranch();
            BInputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            //if (rolen == "RDI")
            //    objGridData.BranchId = Convert.ToInt32(_APICommon.GetBranchByUserId(BInputParam).BranchId);

            objGridData.BranchId = objSearchData.BranchId;
            objGridData.FestivalSchemeId = objSearchData.FestivalSchemeId;

            var output = _APIIncentiveData.GetFestivalIncentiveReport(objGridData);

            DataTable dt = AMBOWeb.Classes.Common.ToDataTable(output.Data.FestivalIncentiveData);
            string filePath = Server.MapPath("~/DownloadTemplate/DeviationUploadByRDI.xlsx");


            //var output = _APIDeviationData.GetDeviationFinalUploadExcel(objSearchData);
            if (output.MessageCode == (int)Acceptable.Found)
            {
                using (XLWorkbook wb = new XLWorkbook(filePath))
                {
                    //wb.Worksheets.Add(dt, "Deviation Data");
                    var sheet1 = wb.Worksheets.Add(dt, "DeviationData");
                    //sheet1.Position = 1;
                    //sheet1.FirstCell().InsertTable(dt, false);
                    sheet1.Table("Table1").ShowAutoFilter = false;

                    wb.Worksheet(1).Position = wb.Worksheets.Count() + 1;

                    string handle = Guid.NewGuid().ToString();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        stream.Position = 0;
                        TempData[handle] = stream.ToArray();
                        return Json(new
                        {
                            status = true,
                            FileGuid = handle,
                            FileName = "FinalDeviationData.xlsx"
                        });
                    }
                }
            }
            else
                return Json(new
                {
                    status = false,
                    message = output.Message,
                    FileGuid = "",
                    FileName = ""
                });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FinalUpload_Festival(HttpPostedFileBase ExcelUploadFile, string Id)
        {
            string filePath = "";
            if (ExcelUploadFile == null || ExcelUploadFile.ContentLength == 0)
            {
                AddErrorNotification("Please select an excel file for upload.");
                return new RedirectResult(Url.Action("FinalUpload_Festival"));
            }

            if (!(Path.GetExtension(ExcelUploadFile.FileName) == ".xls" || Path.GetExtension(ExcelUploadFile.FileName) == ".xlsx"))
            {
                AddErrorNotification("Only .xls and .xlsx file extensions supported.");
                return new RedirectResult(Url.Action("FinalUpload_Festival"));
            }

            filePath = Server.MapPath("~/Uploads/") + Path.GetFileNameWithoutExtension(ExcelUploadFile.FileName) + '_' + objUserSession.UserId + Path.GetExtension(ExcelUploadFile.FileName);
            ExcelUploadFile.SaveAs(filePath);
            DeviationFinalUploadExcel objData = new DeviationFinalUploadExcel();
            objData.FestivalSchemeId = Convert.ToInt32(Id);
            objData.Flag = 2;
            objData.UserId = Convert.ToInt32(objUserSession.UserId);
            string message = "";
            try
            {
                objData.ExcelData = new DataView(ConvertExcelToDataTable(filePath)).ToTable(false, "SFACode", "ProposedDeviation", "Reasons", "FirstHeader", "FirstRemark", "SecondHeader", "SecondRemark", "ApprovedDeviationAmount", "HORemark");

            //objData.ExcelData = new DataView(ConvertExcelToDataTable(filePath)).ToTable(false, "Record Id", "HO Approved Deviation", "HO Remark");
           
                if (!(objData.ExcelData == null || objData.ExcelData.Rows.Count == 0))
                {
                    if (objData.ExcelData.AsEnumerable().Where(r => r.Field<string>("ProposedDeviation") != "0").Count() > 0)
                    {
                        DataTable dt1 = objData.ExcelData.AsEnumerable().Where(r => r.Field<string>("ProposedDeviation") != "0").CopyToDataTable();
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string Reasons = Convert.ToString(dt1.Rows[i]["Reasons"]);

                            if (Reasons == "")
                            {
                                AddErrorNotification("OOPS! Deviation reasons can not be empty.Please check.");
                                return new RedirectResult(Url.Action("FinalUpload"));
                            }
                        }
                        var output = _APIDeviationData.UploadDeviationFinalUploadExcel_Festival(objData);
                        if (output.MessageCode == (int)Acceptable.Created)
                        {
                            AddSuccessNotification(output.Message);
                            //return new RedirectResult(Url.Action("FinalUpload_Festival"));
                            return new RedirectResult(Url.Action("FinalUpload_Festival"));
                        }
                        else
                        {
                            AddErrorNotification("OOPS! Some error occured. Please check the uploaded file.");
                            return new RedirectResult(Url.Action("FinalUpload_Festival"));
                        }
                        objData.ExcelData = null;
                        AddErrorNotification(output.Message);
                        return new RedirectResult(Url.Action("FinalUpload_Festival"));
                    }
                    else
                    {
                        AddErrorNotification("OOPS! At least one record required with proposed deviation amount greater than 0. Please check the uploaded file.");
                        return new RedirectResult(Url.Action("FinalUpload_Festival"));
                    }
                }
            }
            catch (Exception ex)
            {
                AddErrorNotification("OOPS! Please upload correct format as downloaded.");
                return new RedirectResult(Url.Action("FinalUpload_Festival"));
            }
            objData.ExcelData = null;
            AddErrorNotification(message);
            return new RedirectResult(Url.Action("FinalUpload_Festival"));
        }
        #endregion Deviation FinalUpload
        #endregion
    }
}