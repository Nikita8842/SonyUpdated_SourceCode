using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using System.IO;
using AMBOWeb.Filters;
using static AMBOWeb.Classes.Common;
using APIAccessLayer.INTERFACE;
using AMBOModels.IncentiveManagement;
using APIAccessLayer.Helper;

namespace AMBOWeb.Controllers
{
    public class FestivalIncentiveController : SonyBaseController
    {
        private readonly IAPIIncentiveData _APIIncentiveData;

        public FestivalIncentiveController(IAPIIncentiveData IAPIIncentiveData)
        {
            _APIIncentiveData = IAPIIncentiveData;
        }

        [HttpPost]
        public ActionResult GetFestivalIncentiveDefinitionBySchemeId(GetFestivalIncentiveValues objFormData)
        {
            var output = _APIIncentiveData.GetFestivalIncentiveDefinitionBySchemeId(objFormData);
            if (output.MessageCode == (int)Acceptable.Found)
                return View("Create", output.Data);
            else
            {
                AddErrorNotification(output.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult SlabDefinition(GetFestivalIncentiveValues objFormData)
        {
            var output = _APIIncentiveData.GetFestivalIncentiveSlabDefinitionBySchemeId(objFormData);
            if (output.MessageCode == (int)Acceptable.Found)
                return View(output.Data);
            else
            {
                AddErrorNotification(output.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.FestivalIncentiveDefinition, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.FestivalIncentiveDefinition, Rights = (int)Right.Create)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.FestivalIncentiveDefinition, Rights = (int)Right.Create)]
        public ActionResult Create(CreateFestivalIncentive objFormData)
        {
            return View("Create", objFormData);
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.FestivalIncentiveDefinition, Rights = (int)Right.View)]
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
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.FestivalIncentiveDefinition, Rights = (int)Right.View)]
        public ActionResult Download(DownloadFestivalIncentiveDefinitionExcel objDownloadData)
        {
            var output = _APIIncentiveData.GetFestivalIncentiveDefinitionExcelFile(objDownloadData);
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
                            FileName = "FestivalIncentiveDefinition.xlsx"
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
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.FestivalIncentiveDefinition, Rights = (int)Right.View)]
        public ActionResult DownloadSlab(DownloadFestivalIncentiveDefinitionExcel objDownloadData)
        {
            var output = _APIIncentiveData.GetFestivalIncentiveSlabDefinitionExcelFile(objDownloadData);
            if (output.MessageCode == (int)Acceptable.Created)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(output.Data.ExcelData, "Festival Incentive Slab");
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
                            FileName = "FestivalIncentiveSlabDefinition.xlsx"
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
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.FestivalIncentiveDefinition, Rights = (int)Right.Create)]
        public ActionResult Upload(CreateFestivalIncentive objFormData, HttpPostedFileBase ExcelUploadFile)
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
            if (!(objFormData.ExcelData == null || objFormData.ExcelData.Rows.Count == 0))
            {
                var output = _APIIncentiveData.ManageFestivalIncentiveDefinition(objFormData);
                if (output.MessageCode == (int)Acceptable.Created)
                {
                    AddSuccessNotification(output.Message);
                    return new RedirectResult(Url.Action("Index", "FestivalIncentive"));
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
        [ValidateInput(false)]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.FestivalIncentiveDefinition, Rights = (int)Right.Create)]
        public ActionResult UploadSlab(CreateFestivalIncentiveSlab objFormData, HttpPostedFileBase ExcelUploadFile)
        {
            string filePath = "";
            if (ExcelUploadFile == null || ExcelUploadFile.ContentLength == 0)
            {
                AddErrorNotification("Please select an excel file for upload.");
                return View("SlabDefinition",objFormData);
            }

            if (!(Path.GetExtension(ExcelUploadFile.FileName) == ".xls" || Path.GetExtension(ExcelUploadFile.FileName) == ".xlsx"))
            {
                AddErrorNotification("Only .xls and .xlsx file extensions supported.");
                return View("SlabDefinition", objFormData);
            }

            filePath = Server.MapPath("~/Uploads/") + Path.GetFileNameWithoutExtension(ExcelUploadFile.FileName) + '_' + objFormData.UserId + Path.GetExtension(ExcelUploadFile.FileName);
            ExcelUploadFile.SaveAs(filePath);
            objFormData.ExcelData = ConvertExcelToDataTable(filePath);
            string message = "";
            if (!(objFormData.ExcelData == null || objFormData.ExcelData.Rows.Count == 0))
            {
                var output = _APIIncentiveData.ManageFestivalIncentiveSlabDefinition(objFormData);
                if (output.MessageCode == (int)Acceptable.Created)
                {
                    AddSuccessNotification(output.Message);
                    return new RedirectResult(Url.Action("Index", "FestivalIncentive"));
                }
                objFormData.ExcelData = null;
                AddErrorNotification(output.Message);
                return View("SlabDefinition", objFormData);
            }
            objFormData.ExcelData = null;
            AddErrorNotification(message);
            return View("SlabDefinition", objFormData);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.FestivalIncentiveDefinition, Rights = (int)Right.Delete)]
        public ActionResult Delete(DeleteFestivalIncentive objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APIIncentiveData.DeleteFestivalIncentiveDefinition(objFormData);
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
    }
}