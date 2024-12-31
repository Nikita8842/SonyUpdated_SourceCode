using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.INTERFACE;
using APIAccessLayer.Helper;
using static AMBOWeb.Classes.Common;
using AMBOWeb.Filters;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
using ClosedXML.Excel;
using AMBOModels.GlobalAccessible;

namespace AMBOWeb.Controllers
{
    public class SFASalaryMasterController : SonyBaseController
    {
        private readonly IAPISFASalaryMaster _SFASalaryMasterData;
        private readonly IAPICommon _APICommon;

        private string[] ErrorStatus = new string[2];

        public SFASalaryMasterController(IAPISFASalaryMaster _ISFASalaryMasterData, IAPICommon IAPICommon)
        {
            _SFASalaryMasterData = _ISFASalaryMasterData;
            _APICommon = IAPICommon;
        }


        // GET: SFASalaryMaster
        [SessionAuthorize(ModuleId = (Int64)Modules.HR, SubModuleId = (Int64)SubModules.SFASalaryMaster, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            GetBranch InputParam = new GetBranch();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;
            SFASalaryMasterGrid view = new SFASalaryMasterGrid();
            if (rolen == "Branch HR")
            {
                var details = _APICommon.GetBranchMappedForHR(InputParam);
                if (details != null && details.BranchIds != null)
                {
                    view.BranchIds = details.BranchIds.ToArray();
                }
            }
            else
                view.BranchId = 0;

            return View(view);
        }

        [HttpPost]
        [ValidateInput(false)]
        [SessionAuthorize(ModuleId = (Int64)Modules.HR, SubModuleId = (Int64)SubModules.SFASalaryMaster, Rights = (int)Right.Delete)]
        public JsonResult DeleteSFASalaryMaster(SFASalaryMasterDelete objFormData)
        {
            objFormData.UserId = objUserSession.UserId;
            var output = _SFASalaryMasterData.DeleteSFASalaryMaster(objFormData);
            return Json(output);
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.HR, SubModuleId = (Int64)SubModules.SFASalaryMaster, Rights = (int)Right.Edit)]
        public ActionResult UpdateSFASalaryMaster(Int64 LoginId)
        {
            SFASalaryMasterGrid Data = new SFASalaryMasterGrid();
            SFASalaryMasterGrid InputData = new SFASalaryMasterGrid();
            InputData.LoginId = Convert.ToInt64(LoginId);
            Data = _SFASalaryMasterData.GetSFASalaryMasterbyId(InputData);
            return View(Data);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.HR, SubModuleId = (Int64)SubModules.SFASalaryMaster, Rights = (int)Right.Edit)]
        public ActionResult UpdateSFASalaryMaster(SFASalaryMasterGrid objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                Envelope<bool> output = null;

                if (objFormData.Id == 0 || objFormData.Id == null)
                    output = _SFASalaryMasterData.CreateSFASalaryMaster(objFormData);
                else
                    output = _SFASalaryMasterData.UpdateSFASalaryMaster(objFormData);
                if (output.Data)
                    AddSuccessNotification(output.Message);
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string errorMessages = "";
                foreach (ModelState modelState in ModelState.Values)
                    foreach (ModelError error in modelState.Errors)
                        errorMessages += error.ErrorMessage + " ";
                return Json(new Envelope<bool> { Data = false, Message = errorMessages, MessageCode = 0 });
            }
        }

        [HttpPost]
        public ActionResult CreateFormat(SFASalaryMasterDownload InputParam)
        {
            try
            {
                var fileName = "SFASalaryMaster_Format.xlsx";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                DataTable dt = new DataTable("SFASalaryMaster");
                dt.Columns.AddRange(new DataColumn[14] { new DataColumn("SFA Code"),
                                        new DataColumn("SFA Name"),
                                        new DataColumn("Region"),
                                        new DataColumn("Branch"),
                                        new DataColumn("City"),
                                        new DataColumn("Division"),
                                        new DataColumn("Sub Level"),
                                        new DataColumn("Basic"),
                                        new DataColumn("HRA"),
                                        new DataColumn("Medical"),
                                        new DataColumn("Conv"),
                                        new DataColumn("Other Allowance"),
                                        new DataColumn("Airtime"),
                                        new DataColumn("Insurance"),});
                                        

                if (InputParam.BranchIds != null || (InputParam.FromDate != null && InputParam.ToDate != null))
                {
                    List<SFASalaryMasterGrid> listOutput = new List<SFASalaryMasterGrid>();
                    listOutput = _SFASalaryMasterData.SFASalaryMasterDataDownload(InputParam);

                    if (listOutput != null && listOutput.Count > 0)
                    {
                        foreach (var record in listOutput)
                        {
                            dt.Rows.Add(record.SFACode, record.SFAName, record.Region, record.Branch, record.City, record.Division,
                                record.SFALevel, record.Basic, record.HRA, record.Med, record.Conv, record.Other, record.Airtime, 
                                record.Insurance);
                        }
                    }
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, dt.TableName);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        stream.Position = 0;
                        FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                        stream.WriteTo(file);
                        file.Close();

                        //var file= File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Enum.GetName(typeof(Module), ModuleId) + "Data.xlsx");
                        //return Json(file, JsonRequestBehavior.AllowGet);
                        return Json(new { fileName = fileName, errorMessage = "" });
                    }

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public ActionResult SFAMasterforHRUpload()
        {
            return PartialView("PartialViews/SFASalaryMasterUpload", new DataTable());
        }

        [HttpPost]
        public ActionResult UploadData(HttpPostedFileBase ExcelUploadFile)
        {
            List<SFASalaryMasterGrid> listInputData = new List<SFASalaryMasterGrid>();
            List<SFASalaryMasterUpload> listOutputData = new List<SFASalaryMasterUpload>();

            //string filePath = "";

            DataTable dtResult = new DataTable();
            dtResult = null;

            //if (ExcelUploadFile == null || ExcelUploadFile.ContentLength == 0)
            //{
            //    AddErrorNotification("Please select an excel file for upload.");
            //    return View("Index");
            //}
            //if (!(Path.GetExtension(ExcelUploadFile.FileName) == ".xls" || Path.GetExtension(ExcelUploadFile.FileName) == ".xlsx"))
            //{
            //    AddErrorNotification("Only .xls and .xlsx file extensions supported.");
            //    return View("Index");
            //}

            //filePath = Server.MapPath("~/DownloadTemplate/") + Path.GetFileName(ExcelUploadFile.FileName);
            //if (System.IO.File.Exists(filePath))
            //{
            //    // delete file from server 
            //    System.IO.File.Delete(filePath);
            //}
            //ExcelUploadFile.SaveAs(filePath);

            DataTable dt = UploadExcel(ExcelUploadFile);
            
            Envelope<DataTable> output = null;

            if (dt.Rows.Count != 0)
            {
                listInputData = (from DataRow dr in dt.Rows
                                 select new SFASalaryMasterGrid()
                                 {
                                     UserId = objUserSession.UserId,
                                     SFACode = dr["SFA Code"].ToString(),
                                     SFAName = dr["SFA Name"].ToString(),                                     
                                     Region = dr["Region"].ToString(),
                                     Branch = dr["Branch"].ToString(),
                                     City = dr["City"].ToString(),
                                     Division = dr["Division"].ToString(),
                                     SFALevel = dr["Sub Level"].ToString(),
                                     Basic = dr["Basic"].ToString(),
                                     HRA = dr["HRA"].ToString(),
                                     Med = dr["Medical"].ToString(),
                                     Conv = dr["Conv"].ToString(),
                                     Other = dr["Other Allowance"].ToString(),
                                     Airtime = dr["Airtime"].ToString(),
                                     Insurance = dr["Insurance"].ToString(),
                                 }).ToList();

                output = _SFASalaryMasterData.ManageSFASalaryMasterData(listInputData);

                if (output.Data != null && output.MessageCode == (int)Acceptable.Created)
                {
                    dtResult = output.Data;
                    //AddInfoNotification(output.Message);
                }
                else
                    ViewBag.Message = output.Message;

            }
            else
                ViewBag.Message = ErrorStatus[1];
            return PartialView("PartialViews/SFASalaryMasterUpload", dtResult);

        }

        private DataTable UploadExcel(HttpPostedFileBase postedFile)
        {
            string fileName = postedFile.FileName;

            try
            {
                DataTable dt = new DataTable();

                if (!string.IsNullOrEmpty(fileName))
                {
                    string FileName = Path.GetFileName(fileName);
                    string Extension = Path.GetExtension(fileName);
                    if (Extension == ".xls" || Extension == ".xlsx")
                    {
                        string FilePath = Server.MapPath("~/DownloadTemplate/" + FileName);
                        if (System.IO.File.Exists(Server.MapPath("~/DownloadTemplate/" + FileName)))
                        {
                            // delete file from server 
                            System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                        }
                        postedFile.SaveAs(FilePath);
                        string conString = string.Empty;
                        switch (Extension)
                        {
                            case ".xls": //Excel 97-03
                                conString = ConfigurationManager.AppSettings["Excel03ConString"];
                                break;
                            case ".xlsx": //Excel 07 or higher
                                conString = ConfigurationManager.AppSettings["Excel07ConString"];
                                break;

                        }
                        conString = string.Format(conString, FilePath);
                        OleDbConnection oledbConn = new OleDbConnection(conString);
                        if (oledbConn.State == ConnectionState.Closed)
                            oledbConn.Open();
                        using (DataTable dtSheet = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null))
                        {
                            string olQuery = string.Empty;
                            foreach (DataRow dr in dtSheet.Rows)
                            {
                                string sheetName = dr["TABLE_NAME"].ToString();
                                sheetName = sheetName.Replace("'", "");
                                if (!sheetName.EndsWith("$"))
                                    break;

                                // Get all rows from the Sheet
                                olQuery = "SELECT * FROM [" + sheetName + "]";
                                if (!string.IsNullOrEmpty(olQuery))
                                    break;

                            }

                            OleDbCommand cmd = new OleDbCommand(olQuery, oledbConn);
                            OleDbDataAdapter oleda = new OleDbDataAdapter();
                            oleda.SelectCommand = cmd;
                            DataSet ds = new DataSet();
                            oleda.Fill(ds);
                            oledbConn.Dispose();
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0) //validate column names
                            {
                                if (!(dt.Columns[0].ColumnName == "SFA Code" &&
                                        dt.Columns[1].ColumnName == "SFA Name" &&
                                        dt.Columns[2].ColumnName == "Region" &&
                                        dt.Columns[3].ColumnName == "Branch" &&
                                        dt.Columns[4].ColumnName == "City" &&
                                        dt.Columns[5].ColumnName == "Division" &&
                                        dt.Columns[6].ColumnName == "Sub Level" &&
                                        dt.Columns[7].ColumnName == "Basic" &&
                                        dt.Columns[8].ColumnName == "HRA" &&
                                        dt.Columns[9].ColumnName == "Medical" &&
                                        dt.Columns[10].ColumnName == "Conv" &&
                                        dt.Columns[11].ColumnName == "Other Allowance" &&
                                        dt.Columns[12].ColumnName == "Airtime" &&
                                        dt.Columns[13].ColumnName == "Insurance"))
                                {
                                    System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                                    ErrorStatus[0] = "0";
                                    ErrorStatus[1] = "Kindly Correct the column names.";
                                    return null;
                                }
                                //foreach (DataColumn column in dt.Columns) //validate null value in any cell
                                //{
                                //    if (dt.Rows.OfType<DataRow>().Any(r => r.IsNull(column)))
                                //    {
                                //        System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + fileName));
                                //        ViewBag.Status = "0";
                                //        ViewBag.Message = "Blank or invalid values in " + column + " .";
                                //        return null;
                                //    }
                                //}
                            }
                        }
                    }
                    else
                    {
                        System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                        ErrorStatus[0] = "0";
                        ErrorStatus[1] = "Sorry! Kindly Select Excel file only.";
                        return null;
                    }
                    System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                }
                else
                {
                    ErrorStatus[0] = "0";
                    ErrorStatus[1] = "Please Select valid excel file.";
                    return null;
                }

                return dt;
            }
            catch (Exception ex)
            {
                ErrorStatus[0] = "0";
                ErrorStatus[1] = ex.Message;
                return null;
            }
        }

        //private DataTable ConvertExcelToDataTable(string filePath)
        //{
        //    try
        //    {
        //        using (XLWorkbook wb = new XLWorkbook(filePath))
        //        {
        //            IXLWorksheet workSheet = wb.Worksheet(1);
        //            DataTable dt = new DataTable();
        //            bool firstRow = true;
        //            foreach (IXLRow row in workSheet.Rows())
        //            {
        //                if (firstRow)
        //                {
        //                    foreach (IXLCell cell in row.Cells())
        //                        dt.Columns.Add(cell.Value.ToString());
        //                    firstRow = false;
        //                }
        //                else
        //                {
        //                    if (row.IsEmpty())
        //                        row.Delete();
        //                    else
        //                    {
        //                        dt.Rows.Add();
        //                        int i = 0;
        //                        foreach (IXLCell cell in row.Cells(workSheet.FirstCellUsed().Address.ColumnNumber, workSheet.LastCellUsed().Address.ColumnNumber))
        //                        {
        //                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
        //                            i++;
        //                        }
        //                    }
        //                }
        //            }
        //            return dt;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return null;
        //}

        //private bool ValidateExcelData(DataTable dt)
        //{
        //    if (dt != null)
        //    {
        //        if (dt.Columns.Count == 14)
        //        {
        //            if (dt.Columns[0].ColumnName == "SFA Code" &&
        //                                dt.Columns[1].ColumnName == "SFA Name" &&
        //                                dt.Columns[2].ColumnName == "Region" &&
        //                                dt.Columns[3].ColumnName == "Branch" &&
        //                                dt.Columns[4].ColumnName == "City" &&
        //                                dt.Columns[5].ColumnName == "Division" &&
        //                                dt.Columns[6].ColumnName == "Sub Level" &&
        //                                dt.Columns[7].ColumnName == "Basic" &&
        //                                dt.Columns[8].ColumnName == "HRA" &&
        //                                dt.Columns[9].ColumnName == "Medical" &&
        //                                dt.Columns[10].ColumnName == "Conv" &&
        //                                dt.Columns[11].ColumnName == "Other Allowance" &&
        //                                dt.Columns[12].ColumnName == "Airtime" &&
        //                                dt.Columns[13].ColumnName == "Insurance")
        //                return true;
        //            else
        //                messageoutput = "Unexpected column name found in uploaded Excel file";
        //        }
        //        else
        //            messageoutput = "Uploaded excel file has invalid number of columns.";
        //    }
        //    else
        //        messageoutput = "Unable to read data from Excel file.";
        //    return false;
        //}

    }
}