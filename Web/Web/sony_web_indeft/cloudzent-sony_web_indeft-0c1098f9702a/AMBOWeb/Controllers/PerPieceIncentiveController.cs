using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMBOModels.IncentiveManagement;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using ClosedXML.Excel;
using AMBOWeb.Filters;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class PerPieceIncentiveController : SonyBaseController
    {
        private readonly IAPIIncentiveData _APIIncentiveData;

        public PerPieceIncentiveController(IAPIIncentiveData IAPIIncentiveData)
        {
            _APIIncentiveData = IAPIIncentiveData;
        }


        [HttpPost]
        public ActionResult GetPerPieceIncentiveDefinitionBySchemeId(GetPerPieceIncentiveValues objFormData)
        {
            var output = _APIIncentiveData.GetPerPieceIncentiveDefinitionBySchemeId(objFormData);
            if (output.MessageCode == (int)Acceptable.Found)
                return View("Create", output.Data);
            else
            {
                AddErrorNotification(output.Message);
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.PerPieceIncentiveDefinition, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult NavigationIndexPerPiece(PerPieceIncentiveGet param)
        {
            PerPieceIncentiveCreate model = new PerPieceIncentiveCreate();
            model.ProductCategoryId = param.ProductCategoryId;
            model.ProductCategoryName = param.ProductCategoryName;            
            model.MaterialName = param.MaterialName;
            
            if(param.MaterialCode!=null)
            {
                model.MaterialCode = param.MaterialCode;
                MaterialCodeGet input = new MaterialCodeGet();
                input.MaterialCode = param.MaterialCode;
                var outputmaterialid = _APIIncentiveData.GetMaterialIdByMaterialCode(input);
                if (outputmaterialid.Data != null)
                    model.MaterialId = outputmaterialid.Data.MaterialId;
            }
            
            return PartialView("NavigationIndexPerPiece", model);
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.PerPieceIncentiveDefinition, Rights = (int)Right.Create)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.PerPieceIncentiveDefinition, Rights = (int)Right.Create)]
        public ActionResult Create(CreatePerPieceIncentive objFormData)
        {
            return View("Create", objFormData);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.PerPieceIncentiveDefinition, Rights = (int)Right.View)]
        public ActionResult Download(DownloadPerPieceIncentiveExcel objDownloadData)
        {
            var output = _APIIncentiveData.GetPerPieceIncentiveExcelFile(objDownloadData);
            if (output.MessageCode == (int)Acceptable.Created)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(output.Data.ExcelData, "Incentive Data");
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
                            FileName = "PerPieceIncentiveDefinition.xlsx"
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

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.PerPieceIncentiveDefinition, Rights = (int)Right.View)]
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

        [HttpPost]
        [ValidateInput(false)]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.PerPieceIncentiveDefinition, Rights = (int)Right.Create)]
        public ActionResult Upload(CreatePerPieceIncentive objFormData, HttpPostedFileBase ExcelUploadFile)
        {
            string filePath = "";
            if (ExcelUploadFile == null || ExcelUploadFile.ContentLength == 0)
            {
                AddErrorNotification("Please select an excel file for upload.");
                return Create(objFormData);
            }

            if (!(Path.GetExtension(ExcelUploadFile.FileName) == ".xls" || Path.GetExtension(ExcelUploadFile.FileName) == ".xlsx"))
            {
                AddErrorNotification("Only .xls and .xlsx file extensions supported.");
                return Create(objFormData);
            }
            
            filePath = Server.MapPath("~/Uploads/") + Path.GetFileNameWithoutExtension(ExcelUploadFile.FileName) + '_' + objFormData.UserId + Path.GetExtension(ExcelUploadFile.FileName);
            ExcelUploadFile.SaveAs(filePath);
            objFormData.ExcelData = ConvertExcelToDataTable(filePath);
            string message = "";
            if (!(objFormData.ExcelData == null || objFormData.ExcelData.Rows.Count == 0) && ValidateExcelData(objFormData.ExcelData,out message))
            {
                var output = _APIIncentiveData.ManagePerPieceIncentiveDefinition(objFormData);
                if (output.MessageCode == (int)Acceptable.Created)
                {
                    AddSuccessNotification(output.Message);
                    return new RedirectResult(Url.Action("Index", "PerPieceIncentive"));
                }
                objFormData.ExcelData = null;
                AddErrorNotification(output.Message);
                return Create(objFormData);
            }
            objFormData.ExcelData = null;
            AddErrorNotification(message);
            return Create(objFormData);

        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.PerPieceIncentiveDefinition, Rights = (int)Right.Delete)]
        public ActionResult Delete(DeletePerPieceIncentive objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APIIncentiveData.DeletePerPieceIncentiveDefinition(objFormData);
                return Json(output);
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

        private DataTable ConvertExcelToDataTable(string filePath)
        {
            try
            {
                using (XLWorkbook wb = new XLWorkbook(filePath))
                {
                    IXLWorksheet workSheet = wb.Worksheet(1);
                    DataTable dt = new DataTable();
                    bool firstRow = true;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                                dt.Columns.Add(cell.Value.ToString());
                            firstRow = false;
                        }
                        else
                        {
                            if (row.IsEmpty())
                                row.Delete();
                            else
                            {
                                dt.Rows.Add();
                                int i = 0;
                                foreach (IXLCell cell in row.Cells(workSheet.FirstCellUsed().Address.ColumnNumber, workSheet.LastCellUsed().Address.ColumnNumber))
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                    i++;
                                }
                            }
                        }
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        private bool ValidateExcelData(DataTable dt, out string Message)
        {
            Message = string.Empty;
            if (dt != null)
            {
                if (dt.Columns.Count == 9)
                {
                    if (dt.Columns[0].ColumnName.Replace(" ", string.Empty).ToUpper() == "PRODUCTCATEGORY" &&                    
                        dt.Columns[1].ColumnName.Replace(" ", string.Empty).ToUpper() == "TARGETCATEGORY" &&
                        dt.Columns[2].ColumnName.Replace(" ", string.Empty).ToUpper() == "PRODUCTNAME" &&
                        dt.Columns[3].ColumnName.Replace(" ", string.Empty).ToUpper() == "MATERIALCODE" &&
                        dt.Columns[4].ColumnName.Replace(" ", string.Empty).ToUpper() == "MINQTY" &&
                        dt.Columns[5].ColumnName.Replace(" ", string.Empty).ToUpper() == "MAXQTY" &&
                        dt.Columns[6].ColumnName.Replace(" ", string.Empty).ToUpper() == "INCENTIVEAMOUNT" &&
                        dt.Columns[7].ColumnName.Replace(" ", string.Empty).ToUpper() == "MIN%ACH" &&
                        dt.Columns[8].ColumnName.Replace(" ", string.Empty).ToUpper() == "MAX%ACH")
                        return true;
                    else
                        Message = "Unexpected column name found in uploaded Excel file";
                }
                else
                    Message = "Uploaded excel file has invalid number of columns.";
            }
            else
                Message = "Unable to read data from Excel file.";
            return false;
        }

        [HttpPost]
        public ActionResult GetSchemeList(PerPieceIncentiveSchemeByProductId param)
        {
            var output = _APIIncentiveData.GetPerPieceIncentiveSchemeByProductId(param);
            return Json(output.Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.PerPieceIncentiveDefinition, Rights = (int)Right.Create)]
        public ActionResult NavigationCreate(PerPieceIncentiveCreate param)
        {
            param.UserId = objUserSession.UserId;
            var output = _APIIncentiveData.ManagePerPieceMaterialMapping(param);

            return Json(output, JsonRequestBehavior.AllowGet);
        }
    }
}