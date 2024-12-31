using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIAccessLayer.INTERFACE;
using System.IO;
using AMBOModels.Modules;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using APIAccessLayer.Helper;
using Newtonsoft.Json;
using static AMBOWeb.Classes.Common;
using AMBOWeb.Filters;
using ClosedXML.Excel;
using AMBOModels.GlobalAccessible;

namespace AMBOWeb.Controllers
{
    public class AssetAssignmentToRDIController : SonyBaseController
    {
        private readonly IAPIAssetAssignment _APIAssetAssignment;
        public static IAPICommon _Common;
        public static IAPIGridData _APIGridData;

        private string[] ErrorStatus = new string[2];

        public AssetAssignmentToRDIController(IAPIAssetAssignment IAPIAssetAssignment, IAPICommon IAPICommon, IAPIGridData IAPIGridData)
        {
            _APIAssetAssignment = IAPIAssetAssignment;
            _Common = IAPICommon;
            _APIGridData = IAPIGridData;
        }

        // GET: AssetAssignmentToRDI
        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Asset, SubModuleId = (Int64)SubModules.AssetAssignmenttoRDI, Rights = (int)Right.View)]
        public ActionResult Index()
        {            
            return View();
        }

        
        public ActionResult CreateFormat(string ReferenceNumber)
        {
            try
            {
                AssetAssignmentToRDIGet InputParam = new AssetAssignmentToRDIGet();
                InputParam.Reference = ReferenceNumber;

                DataTable dt = new DataTable("AssetAssignmenttoRDI");
                dt.Columns.AddRange(new DataColumn[10] { new DataColumn("MaterialCode"),
                                        new DataColumn("ProductName"),
                                        new DataColumn("IssuedQty"),
                                        new DataColumn("IssuedDate"),
                                        new DataColumn("RDIName"),
                                        new DataColumn("RDICode"),
                                        new DataColumn("Place"),
                                        new DataColumn("Reason"),
                                        new DataColumn("ReturnDate"),
                                        new DataColumn("Reference")});


                IEnumerable<AssetAssignmentToRDIGrid> assetlist = new List<AssetAssignmentToRDIGrid>();
                assetlist = _APIGridData.GetAssetAssignmentToRDIByReference(InputParam);

                if (assetlist != null && assetlist.Count() > 0)
                {
                    foreach (var record in assetlist)
                    {
                        dt.Rows.Add(record.MaterialCode, record.ProductName, record.IssuedQty, record.IssuedDate, record.RDIName,
                            record.RDICode, record.Place, record.Reason,null, record.Reference);
                    }
                }
                var fileName = "AssetAssignmentToRDI_Data.xlsx";
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
            catch(Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Asset, SubModuleId = (Int64)SubModules.AssetAssignmenttoRDI, Rights = (int)Right.Edit)]
        public ActionResult UpdateAssetAssignmentToRDI(AssetAssignmentToRDIUpdate assetAssignmentToRDI)
        {
            var output = _APIAssetAssignment.UpdateAssetAssignmentToRDI(assetAssignmentToRDI);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Asset, SubModuleId = (Int64)SubModules.AssetAssignmenttoRDI, Rights = (int)Right.Create)]
        public ActionResult UploadFilePartial(HttpPostedFileBase file)
        {
            AssetAssignmentToRDIUpload InputParam = new AssetAssignmentToRDIUpload();
            InputParam.UserId = objUserSession.UserId;
            DataTable dtResult = new DataTable();
            dtResult = null;
            List<AssetAssignmentToRDUploadOutput> outputgrid = new List<AssetAssignmentToRDUploadOutput>();
            Envelope<List<AssetAssignmentToRDUploadOutput>> output = new Envelope<List<AssetAssignmentToRDUploadOutput>>(); ;

            if (file != null)
            {
                var dt = UploadExcel(file);
                if (dt != null)
                {
                    InputParam.dtAsset = dt;
                    var apioutput = _APIAssetAssignment.UploadAssetAssignmentToRDI(InputParam);
                    if (apioutput.Data != null && apioutput.MessageCode == (int)Acceptable.Created)
                    {
                        dtResult = apioutput.Data;
                        outputgrid = ConvertTo<AssetAssignmentToRDUploadOutput>(apioutput.Data);
                        output.Data = outputgrid;
                        output.MessageCode = apioutput.MessageCode;
                        output.Message = apioutput.Message;
                    }
                    else if (apioutput.MessageCode == (int)NotAcceptable.NotModified)
                    {
                        output.Data = null;
                        output.MessageCode = apioutput.MessageCode;
                        output.Message = apioutput.Message;
                    }
                }
                else
                {
                    output.Data = null;
                    output.MessageCode = Convert.ToInt32(ErrorStatus[0]); 
                    output.Message = ErrorStatus[1];
                }
            }
            else
            {
                output.Data = null;
                output.MessageCode = (int)NotAcceptable.BadRequest;
                output.Message = "Please Select file to upload and target month";
            }

            return Json(output, JsonRequestBehavior.AllowGet);
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
                                if ((dt.Columns[0].ColumnName != "MaterialCode") ||
                                        (dt.Columns[1].ColumnName != "ProductName") ||
                                        (dt.Columns[2].ColumnName != "IssuedQty") ||
                                        (dt.Columns[3].ColumnName != "IssuedDate") ||
                                        (dt.Columns[4].ColumnName != "RDIName") ||
                                        (dt.Columns[5].ColumnName != "RDICode") ||
                                        (dt.Columns[6].ColumnName != "Place") ||
                                        (dt.Columns[7].ColumnName != "Reason") ||
                                        (dt.Columns[8].ColumnName != "ReturnDate") ||
                                        (dt.Columns[9].ColumnName != "Reference"))
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
                
                #region comment
                //string filePath = string.Empty;
                //DataTable dt = new DataTable();
                //if (postedFile != null)
                //{
                //    string path = Server.MapPath("~/Uploads/");
                //    if (!Directory.Exists(path))
                //    {
                //        Directory.CreateDirectory(path);
                //    }

                //    filePath = path + Path.GetFileName(postedFile.FileName);
                //    string extension = Path.GetExtension(postedFile.FileName);
                //    postedFile.SaveAs(filePath);

                //    string conString = string.Empty;
                //    switch (extension)
                //    {
                //        case ".xls": //Excel 97-03.
                //            conString = ConfigurationManager.AppSettings["Excel03ConString"]; ;
                //            break;
                //        case ".xlsx": //Excel 07 and above.
                //            conString = ConfigurationManager.AppSettings["Excel07ConString"];
                //            break;
                //    }


                //    conString = string.Format(conString, filePath);

                //    using (OleDbConnection connExcel = new OleDbConnection(conString))
                //    {
                //        using (OleDbCommand cmdExcel = new OleDbCommand())
                //        {
                //            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                //            {
                //                cmdExcel.Connection = connExcel;

                //                //Get the name of First Sheet.
                //                connExcel.Open();
                //                DataTable dtExcelSchema;
                //                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //                string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                //                connExcel.Close();

                //                //Read Data from First Sheet.
                //                connExcel.Open();
                //                cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                //                odaExcel.SelectCommand = cmdExcel;
                //                odaExcel.Fill(dt);
                //                connExcel.Close();

                //                DataTable dtNewCheck = new DataTable();
                //                dtNewCheck = dt;
                //                if (dtNewCheck.Rows.Count > 0)
                //                {
                //                    if ((dtNewCheck.Columns[0].ColumnName != "MaterialCode") ||
                //                        (dtNewCheck.Columns[1].ColumnName != "ProductName") ||
                //                        (dtNewCheck.Columns[2].ColumnName != "IssuedQty") ||
                //                        (dtNewCheck.Columns[3].ColumnName != "IssuedDate") ||
                //                        (dtNewCheck.Columns[4].ColumnName != "RDIName") ||
                //                        (dtNewCheck.Columns[5].ColumnName != "RDICode") ||
                //                        (dtNewCheck.Columns[6].ColumnName != "Place") ||
                //                        (dtNewCheck.Columns[7].ColumnName != "Reason") ||
                //                        (dtNewCheck.Columns[8].ColumnName != "ReturnDate") ||
                //                        (dtNewCheck.Columns[9].ColumnName != "Reference"))
                //                    {
                //                        //lblErrorMessage.Text = "Please correct your file column names.";
                //                        return null;
                //                    }

                //                    foreach (DataColumn column in dtNewCheck.Columns) //validate null value in any cell
                //                    {
                //                        if (dtNewCheck.Rows.OfType<DataRow>().Any(r => r.IsNull(column)))
                //                        {
                //                            //lblErrorMessage.Text = "Blank or invalid values in " + column + " .";
                //                            return null;
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }               
                //}
                #endregion comment
                return dt;
            }
            catch(Exception ex)
            {
                ErrorStatus[0] = "0";
                ErrorStatus[1] = ex.Message;
                ErrorDetailsInput inp = new ErrorDetailsInput();
                inp.ErrorDetails = ex.StackTrace;
                inp.ErrorMessage = ex.Message;
                inp.ErrorSource = "AsssetAssignmentToRDIExcelUpload";
                _Common.CreateErrorLogWeb(inp);
                return null;
            }
        }


    }
}