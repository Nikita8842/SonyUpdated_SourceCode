using AMBOModels.Modules;
using APIAccessLayer.INTERFACE;
using System;
using AMBOWeb.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
using APIAccessLayer.Helper;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using static AMBOWeb.Classes.Common;
using AMBOWeb.Filters;
using ClosedXML.Excel;
using AMBOModels.GlobalAccessible;

namespace AMBOWeb.Controllers
{
    public class AssetAssignmentToSFAController : SonyBaseController
    {
        private readonly IAPIAssetIssuedToSFA _APIAssetIssuedToSFA;
        private readonly IAPICommon _Common;
        private string[] ErrorStatus = new string[2];
        public AssetAssignmentToSFAController(IAPIAssetIssuedToSFA IAPIAssetIssuedToSFA, IAPICommon IAPICommon)
        {
            _APIAssetIssuedToSFA = IAPIAssetIssuedToSFA;
            _Common = IAPICommon;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Asset, SubModuleId = (Int64)SubModules.AssetIssuetoSFA, Rights = (int)Right.View)]
        public ActionResult IssueAssetToSFA()
        {
            DataTable dt = new DataTable();
            return View(dt);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Asset, SubModuleId = (Int64)SubModules.AssetIssuetoSFA, Rights = (int)Right.Create)]
        public ActionResult IssueAssetToSFA(HttpPostedFileBase file)
        {
            AssetAssignmentToSFA InputParam = new AssetAssignmentToSFA();
            InputParam.UserId = objUserSession.UserId;

            //Envelope<bool> output;
            DataTable dtResult = new DataTable();

            var dt = UploadExcel(file);
            if (dt != null)
            {
                InputParam.assetAssignmentToSFAdt = dt;
                Envelope<DataTable> result = _APIAssetIssuedToSFA.IssueAssetToSFA(InputParam);
                if (result.Data!=null)
                { 
                    ErrorStatus[0] = "2";
                    ErrorStatus[1] = "Partial/full data uploaded.";
                    dtResult = result.Data;
                }
                else
                {
                    ErrorStatus[0] = "3";
                    ErrorStatus[1] = "Error occured, please validate data and format.";
                }
            }
            
            ViewBag.Status = ErrorStatus[0];
            ViewBag.Message = ErrorStatus[1];
            ViewBag.returnTable = JsonConvert.SerializeObject(dtResult);
            return View();
        }

        private DataTable UploadExcel(HttpPostedFileBase postedFile)
        {
            string FileName = postedFile.FileName;
            try
            {
                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(postedFile.FileName))
                {

                    string Extension = Path.GetExtension(postedFile.FileName);
                    if (Extension == ".xls" || Extension == ".xlsx" || Extension == ".csv")
                    {
                        string FilePath = Server.MapPath("~/DownloadTemplate/" + FileName);
                        if (System.IO.File.Exists(Server.MapPath("~/DownloadTemplate/" + FileName)))
                        {
                            // delete file from server 
                            //System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                        }
                        postedFile.SaveAs(FilePath);
                        string extension = Path.GetExtension(postedFile.FileName);
                        string conStr = "";
                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls": //Excel 97-03
                                conString = ConfigurationManager.AppSettings["Excel03ConString"];
                                break;
                            case ".xlsx": //Excel 07 or higher
                                conString = ConfigurationManager.AppSettings["Excel07ConString"];
                                break;
                            case ".csv": //CSV Format
                                conString = ConfigurationManager.AppSettings["CSVConString"];
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
                                if ((dt.Columns[0].ColumnName != "MaterialCode") ||
                                        (dt.Columns[1].ColumnName != "ProductName") ||
                                        (dt.Columns[2].ColumnName != "IssuedQuantity") ||
                                        (dt.Columns[3].ColumnName != "IssuedDate") ||
                                        (dt.Columns[4].ColumnName != "SFAName") ||
                                        (dt.Columns[5].ColumnName != "SFACode") ||
                                        (dt.Columns[6].ColumnName != "Remarks"))

                                {
                                    System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                                    ErrorStatus[0] = "1";
                                    ErrorStatus[1] = "Kindly Correct the column names.";
                                    return null;
                                }

                            }
                        }
                    }
                    else
                    {
                        System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                        ErrorStatus[0] = "1";
                        ErrorStatus[1] = "Sorry! Kindly Select Excel/CSV file only.";
                        return null;
                    }
                    System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                }
                else
                {
                    ErrorStatus[0] = "1";
                    ErrorStatus[1] = "Please Select valid excel file.";
                    return null;
                }

                return dt;
            }
            catch(Exception ex)
            {
                ErrorStatus[0] = "1";
                ErrorStatus[1] = ex.Message;
                ErrorDetailsInput inp = new ErrorDetailsInput();
                inp.ErrorDetails = ex.StackTrace;
                inp.ErrorMessage = ex.Message;
                inp.ErrorSource = "AsssetAssignmentToSFAExcelUpload";
                _Common.CreateErrorLogWeb(inp);
                return null;
            }
        }

        [HttpGet]
        public JsonResult GetAssetsDropdown()
        {
            AssetIssuedToSFAGet Input = new AssetIssuedToSFAGet();
            Input.UserId = objUserSession.UserId;
            var output = _APIAssetIssuedToSFA.GetAssetsDropDown(Input);
            return Json(output.Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Asset, SubModuleId = (Int64)SubModules.AssetIssuetoSFA, Rights = (int)Right.View)]
        public ActionResult DownloadTemplate(AssetIssuedToSFAGet InputParam)
        {
            try
            {
                InputParam.RoleName = objUserSession.RoleName;
                InputParam.UserId = objUserSession.UserId;
                string matcode = "", matname = "";

                if (InputParam.MaterialName != "" && InputParam.MaterialName != null)
                {
                    var res = InputParam.MaterialName.Split('_');
                    matcode = res[0];
                    matname = res[1];
                }

                DataTable dt = new DataTable("AssetIssueToSFA");
                dt.Columns.AddRange(new DataColumn[7] { new DataColumn("MaterialCode"),
                                        new DataColumn("ProductName"),
                                        new DataColumn("IssuedQuantity"),
                                        new DataColumn("IssuedDate"),
                                        new DataColumn("SFAName"),
                                        new DataColumn("SFACode"),
                                        new DataColumn("Remarks")});
                dt.Columns[0].DataType = typeof(string);


                IEnumerable<AssetIssuedToSFAGrid> assetlist = new List<AssetIssuedToSFAGrid>();
                var assetoutput = _APIAssetIssuedToSFA.GetAssetIssuedToSFA(InputParam);
                if (assetoutput.MessageCode == (int)Acceptable.Found)
                    assetlist = assetoutput.Data;

                if (assetlist != null && assetlist.Count() > 0)
                {
                    foreach (var record in assetlist)
                    {
                        if (record.MaterialCode == "" || record.MaterialCode == null)
                            record.MaterialCode = matcode;
                        if (record.ProductName == "" || record.ProductName == null)
                            record.ProductName = matname;
                        dt.Rows.Add(record.MaterialCode, record.ProductName, record.IssuedQuantity, record.IssuedDate, record.SFAName,
                            record.SFACode, record.Remarks);
                    }
                }
                var fileName = "AssetIssuedToSFA_Data.xlsx";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
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
                        return Json((new { fileName = fileName, errorMessage = "" }), JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}