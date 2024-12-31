using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIAccessLayer.INTERFACE;
using AMBOModels.Modules;
using APIAccessLayer.Helper;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using static AMBOWeb.Classes.Common;
using AMBOWeb.Filters;
using ClosedXML.Excel;
using AMBOModels.GlobalAccessible;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AMBOWeb.Controllers
{
    public class AssetCollectionFromSFAController : SonyBaseController
    {
        private readonly IAPIAssetAssignment _APIAssetAssignment;
        private readonly IAPICommon _Common;
        private string[] ErrorStatus = new string[2];

        public AssetCollectionFromSFAController(IAPIAssetAssignment IAPIAssetAssignment, IAPICommon IAPICommon)
        {
            _APIAssetAssignment = IAPIAssetAssignment;
            _Common = IAPICommon;
        }

        // GET: AssetCollectionFromSFA
        [SessionAuthorize(ModuleId = (Int64)Modules.Asset, SubModuleId = (Int64)SubModules.AssetCollectionfromSFA, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            GetBranch InputParam = new GetBranch();
            AssetCollectionFromSFA view = new AssetCollectionFromSFA();
            InputParam.UserId = objUserSession.UserId;
            string roleName = objUserSession.RoleName;

            if (roleName == "RDI")
            {
                var details = _Common.GetBranchByUserId(InputParam);
                ViewBag.BranchId = details.BranchId;
            }
            else
                ViewBag.BranchId = 0;

            DataTable dt = new DataTable();
            dt = null;
            return View(dt);
        }


        // GET: download template excel for uploading the data
       // [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Asset, SubModuleId = (Int64)SubModules.AssetCollectionfromSFA, Rights = (int)Right.View)]
        public ActionResult CreateFormat(string [] SFACode)
        {
            try
            {
                AssetCollectionFromSFAGet InputParam = new AssetCollectionFromSFAGet();
                InputParam.UserId = objUserSession.UserId;
                InputParam.SFACode = SFACode;

                DataTable dt = new DataTable("AssetCollectionFromSFA");
                dt.Columns.AddRange(new DataColumn[8] { new DataColumn("SFACode"),
                                        new DataColumn("SFAName"),
                                        new DataColumn("ProductName"),
                                        new DataColumn("MaterialCode"),
                                        new DataColumn("IssuedQuantity"),
                                        new DataColumn("IssuedDate"),
                                        new DataColumn("ReturnQuantity"),
                                        new DataColumn("Remarks")});

                IEnumerable<AssetCollectionFromSFAData> assetlist = new List<AssetCollectionFromSFAData>();
                

                
                
                var output = _APIAssetAssignment.GetAssetCollectionFormatFromSFA(InputParam);
                if (output.MessageCode == (int)Acceptable.Found)
                    assetlist = output.Data;

                if (assetlist != null && assetlist.Count() > 0)
                {
                    foreach (var record in assetlist)
                    {
                        dt.Rows.Add(record.SFACode, record.SFAName, record.ProductName, record.MaterialCode, 
                            record.IssuedQuantity, record.IssuedDate, record.ReturnQuantity, record.Remarks);
                    }
                }

                var fileName = "AssetCollectionFromSFA_Data.xlsx";
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
                        return Json(new { fileName = fileName, errorMessage = "" });
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Asset, SubModuleId = (Int64)SubModules.AssetCollectionfromSFA, Rights = (int)Right.Create)]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            AssetCollectionFromSFA InputParam = new AssetCollectionFromSFA();
            InputParam.UserId = objUserSession.UserId;
            DataTable dtResult = new DataTable();
            dtResult = null;
            Envelope<DataTable> output = null;

            var dt = UploadExcel(file);
            if (dt != null)
            {
                InputParam.dtAsset = dt;
                output = _APIAssetAssignment.UploadAssetCollectionFromSFA(InputParam);
                if (output.Data != null)
                {
                    ErrorStatus[0] = "2";
                    ErrorStatus[1] = "Partial/full data uploaded.";
                    dtResult = output.Data;
                }
                else
                {
                    ErrorStatus[0] = "3";
                    ErrorStatus[1] = "Error occured, please validate data and format.";
                }

            }
            ViewBag.Status = ErrorStatus[0];
            ViewBag.Message = ErrorStatus[1];
            GetBranch Input = new GetBranch();
            AssetCollectionFromSFA view = new AssetCollectionFromSFA();
            InputParam.UserId = objUserSession.UserId;
            string roleName = objUserSession.RoleName;

            if (roleName == "RDI")
            {
                var details = _Common.GetBranchByUserId(Input);
                ViewBag.BranchId = details.BranchId;
            }
            else
                ViewBag.BranchId = 0;
            return View("Index", dtResult);
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
                                conString = ConfigurationManager.AppSettings["Excel03ConString"];//provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended properties='Excel 8.0; HDR=yes;'
                                break;
                            case ".xlsx": //Excel 07 or higher
                                conString = ConfigurationManager.AppSettings["Excel07ConString"];// provider=microsoft.ACE.OLEDB.12.0;Data Source={0};Extended properties='Excel 12.0; HDR=yes;'
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
                                if ((dt.Columns[0].ColumnName != "SFACode") ||
                                        (dt.Columns[1].ColumnName != "SFAName") ||
                                        (dt.Columns[2].ColumnName != "ProductName") ||
                                        (dt.Columns[3].ColumnName != "MaterialCode") ||                                        
                                        (dt.Columns[4].ColumnName != "IssuedQuantity") ||
                                        (dt.Columns[5].ColumnName != "IssuedDate") ||
                                        (dt.Columns[6].ColumnName != "ReturnQuantity") ||                                        
                                        (dt.Columns[7].ColumnName != "Remarks"))
                                {
                                    System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                                    ErrorStatus[0] = "1";
                                    ErrorStatus[1] = "Kindly Correct the column names.";
                                    return null;
                                }
                                //foreach (DataColumn column in dt.Columns) //validate null value in any cell
                                //{
                                //    if (dt.Rows.OfType<DataRow>().Any(r => r.IsNull(column)))
                                //    {
                                //        System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                                //        return null;
                                //    }
                                //}
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
                inp.ErrorSource = "AsssetCollectionFromSFAExcelUpload";
                _Common.CreateErrorLogWeb(inp);
                return null;
            }
        }
    }
}