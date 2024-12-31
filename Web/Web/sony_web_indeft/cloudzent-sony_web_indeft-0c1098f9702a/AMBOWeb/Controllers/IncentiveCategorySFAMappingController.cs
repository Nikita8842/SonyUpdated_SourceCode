using AMBOModels.Mappings;
using AMBOWeb.Filters;
using APIAccessLayer.INTERFACE;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class IncentiveCategorySFAMappingController : SonyBaseController
    {
        private readonly IAPIMappingData _APIMappingData;

        public IncentiveCategorySFAMappingController(IAPIMappingData IAPIMappingData)
        {
            _APIMappingData = IAPIMappingData;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.AssignIncentiveCategory, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.AssignIncentiveCategory, Rights = (int)Right.View)]
        public ActionResult Download(Int64 RDIId)
        {
            RDIId = RDIId == 0 ? RDIId = objUserSession.UserId : RDIId;
            var output = _APIMappingData.GetSFAForIncentiveCategorySFAMapping(RDIId);

            if (output.MappingExcelData != null)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(output.MappingExcelData,"Mapping Data");
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AssigningIncentiveCatToSFA.xlsx");
                    }
                }
            }
            else
                return null;
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.AssignIncentiveCategory, Rights = (int)Right.Create)]
        public ActionResult Upload(HttpPostedFileBase ExcelUploadFile)
        {
            string filePath = "";
            IncentiveCategorySFAMapping objModel = new IncentiveCategorySFAMapping();
            if (ExcelUploadFile == null || ExcelUploadFile.ContentLength == 0)
            {
                AddErrorNotification("Please select an excel file for upload.");
                return View("Index");
            }
            if (!(Path.GetExtension(ExcelUploadFile.FileName) == ".xls" || Path.GetExtension(ExcelUploadFile.FileName) == ".xlsx"))
            {
                AddErrorNotification("Only .xls and .xlsx file extensions supported.");
                return View("Index");
            }

            filePath = Server.MapPath("~/Uploads/") + Path.GetFileName(ExcelUploadFile.FileName);
            ExcelUploadFile.SaveAs(filePath);

            DataTable dt = ConvertExcelToDataTable(filePath);
            if (!(dt == null || dt.Rows.Count == 0) && ValidateExcelData(dt))
            {
                objModel.MappingExcelData = dt;
                var output = _APIMappingData.ManageIncentiveCategorySFAMapping(objModel);
                if (output.Data.MappingExcelData == null || output.Data.MappingExcelData.Rows.Count == 0 || 
                    output.Data.MappingExcelData.Columns[0].ColumnName.ToUpper() == "STATUS")
                    AddErrorNotification(output.Message);
                else
                    AddSuccessNotification(output.Message);
                objModel = output.Data;
            }
            else
                AddErrorNotification("Invalid Excel File Uploaded.");
            return View("Index", objModel);
            
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

        private bool ValidateExcelData(DataTable dt)
        {
            if(dt != null)
            {
                if (dt.Columns.Count == 11)
                {
                    if (dt.Columns[0].ColumnName.Replace(" ", string.Empty).ToUpper() == "BRANCHNAME" &&
                        dt.Columns[1].ColumnName.Replace(" ", string.Empty).ToUpper() == "CITY" &&
                        dt.Columns[2].ColumnName.Replace(" ", string.Empty).ToUpper() == "LOCATION" &&
                        dt.Columns[3].ColumnName.Replace(" ", string.Empty).ToUpper() == "SFACODE" &&
                        dt.Columns[4].ColumnName.Replace(" ", string.Empty).ToUpper() == "SFANAME" &&
                        dt.Columns[5].ColumnName.Replace(" ", string.Empty).ToUpper() == "DEALERCODE" &&
                        dt.Columns[6].ColumnName.Replace(" ", string.Empty).ToUpper() == "MASTERCODE" &&
                        dt.Columns[7].ColumnName.Replace(" ", string.Empty).ToUpper() == "DEALERNAME" &&
                        dt.Columns[8].ColumnName.Replace(" ", string.Empty).ToUpper() == "SFACATEGORY" &&
                        dt.Columns[9].ColumnName.Replace(" ", string.Empty).ToUpper() == "INCENTIVECATEGORY" &&
                        dt.Columns[10].ColumnName.Replace(" ", string.Empty).ToUpper() == "FESTIVALINCENTIVECATEGORY")
                        return true;
                    else
                        AddErrorNotification("Unexpected column name found in uploaded Excel file");
                }
                else
                    AddErrorNotification("Uploaded excel file has invalid number of columns.");
            }
            else
                AddErrorNotification("Unable to read data from Excel file.");
            return false;
        }
    }
}