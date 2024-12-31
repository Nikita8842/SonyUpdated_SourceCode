using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIAccessLayer.INTERFACE;
using AMBOModels.MasterMaintenance;
using AMBOModels.Mappings;
using APIAccessLayer.Helper;
using ClosedXML.Excel;
using AMBOWeb.Filters;
using static AMBOWeb.Classes.Common;
using AMBOModels.GlobalAccessible;
using System.Web.Script.Serialization;

namespace AMBOWeb.Controllers
{
    public class ReportQSRQuantityController : SonyBaseController
    {
        private readonly IAPIMappingData _APIMappingData;
        public static IAPICommon _Common;
        public readonly IAPIGridData _APIGridData;

        private string[] ErrorStatus = new string[2];

        public ReportQSRQuantityController(IAPIMappingData IAPIMappingData, IAPICommon IAPICommon, IAPIGridData IAPIGridData)
        {
            _APIMappingData = IAPIMappingData;
            _Common = IAPICommon;
            _APIGridData = IAPIGridData;
        }


        // GET: AssignTargetToSFA
        [SessionAuthorize(ModuleId = (Int64)Modules.Incentive, SubModuleId = (Int64)SubModules.AssignTargettoSFA, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            QSRQuantityGet InputParam = new QSRQuantityGet();
            Int64 UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;
            InputParam.TargetDate = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();

            if (rolen == "RDI")
            {
                var details = _APIMappingData.GetBranchByUserId(UserId);
                InputParam.BranchId = details.BranchId;
            }
            return View(InputParam);
        }
        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Incentive, SubModuleId = (Int64)SubModules.AssignTargettoSFA, Rights = (int)Right.View)]
        public ActionResult Show(string TargetDate, Int64 BranchId)
        {
            QSRQuantityGet Input = new QSRQuantityGet();
            Input.UserId = objUserSession.UserId;
            Input.TargetDate = TargetDate;
            if (BranchId != 0)
                Input.BranchId = BranchId;
            List<QsrReportsGrid> targetList = new List<QsrReportsGrid>();

            targetList = _APIGridData.ShowQuantityReport(Input);

            var jsonresult = Json(targetList, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = Int32.MaxValue;
            return jsonresult;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Incentive, SubModuleId = (Int64)SubModules.AssignTargettoSFA, Rights = (int)Right.View)]
        public ActionResult Download(string TargetDate, Int64 BranchId)
        {
            QSRQuantityGet Input = new QSRQuantityGet();
            Input.UserId = objUserSession.UserId;
            Input.TargetDate = TargetDate.Replace("/", "");

            if (BranchId != 0)
                Input.BranchId = BranchId;

            DataTable dt = new DataTable("TargetToSFA");
            dt.Columns.AddRange(new DataColumn[21] {
new DataColumn("Branch"),
new DataColumn("Date"),
new DataColumn("DealerName"),
new DataColumn("City"),
new DataColumn("Location"),
new DataColumn("PayerName"),
new DataColumn("Channel"),
new DataColumn("DealerState"),
new DataColumn("MasterCode"),
new DataColumn("DealerCode"),
new DataColumn("DealerClassification"),
new DataColumn("SFACode"),
new DataColumn("SFAName"),

new DataColumn("SFALevel"),
new DataColumn("CompanyName"),
new DataColumn("ProductCategory"),
new DataColumn("Material"),
new DataColumn("SFAType"),
new DataColumn("AmboQuantity"),
new DataColumn("QSRQuantity"),
new DataColumn("FinalQuantity"),
});


            List<QsrReportsGrid> targetList = new List<QsrReportsGrid>();
            targetList = _APIGridData.GetQuantityReport(Input);

            if (targetList != null && targetList.Count > 0)
            {
                foreach (var record in targetList)
                {

                    dt.Rows.Add(record.BranchName, record.QSRDate, record.Dealer, record.City, record.Location, record.PayerName, record.Channel,
                    record.DealerState, record.MasterCode, record.DealerCode, record.DealerClassification,
                    record.SFACode, record.SFAName, record.SFALevel, record.CompanyName, record.ProductCategory, record.Material,
                    record.SFAType, record.AmboQuantity, record.QSRQuantity, record.FinalQuantity);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "QSRUpload.xlsx");
                    }
                }
            }
            else
            {
                ViewBag.Status = "404";
                ViewBag.Message = "No data to download.";
                Input.BranchId = 0;
                return View("Index", Input);
            }

        }

        [HttpGet]
        public ActionResult CreateFormat()
        {
            try
            {
                return File("~/DownloadTemplate/AssignTargetToSFA.xlsx", "application/vnd.ms-excel", "AssignTargetToSFA.xlsx");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Incentive, SubModuleId = (Int64)SubModules.AssignTargettoSFA, Rights = (int)Right.Create)]
        public ActionResult UploadData(HttpPostedFileBase file, QSRQuantityUpload Input)
        {
            ErrorDetailsInput inp = new ErrorDetailsInput();
            inp.ErrorDetails = "started upload";
            inp.ErrorMessage = "started upload";
            inp.ErrorSource = "AssignTargetToSFAExcelUpload";
            _Common.CreateErrorLogWeb(inp);
            QSRQuantityUpload InputParam = new QSRQuantityUpload();
            InputParam.UserId = objUserSession.UserId;

            InputParam.TargetDate = Input.TargetDate;

            Envelope<List<QSRQuantityReportUploadOutput>> output = new Envelope<List<QSRQuantityReportUploadOutput>>();
            List<QSRQuantityReportUploadOutput> _AssignTargetToSFAUploadOutput = new List<QSRQuantityReportUploadOutput>();
            inp.ErrorDetails = "checking for file";
            inp.ErrorMessage = "checking for file";
            inp.ErrorSource = "AssignTargetToSFAExcelUpload";
            _Common.CreateErrorLogWeb(inp);
            if (file != null || InputParam.TargetDate != "")
            {
                inp.ErrorDetails = "file is valid";
                inp.ErrorMessage = "file is valid";
                inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                _Common.CreateErrorLogWeb(inp);
                var dt = UploadExcel(file);
                if (dt != null)
                {
                    inp.ErrorDetails = "file data is valid";
                    inp.ErrorMessage = "file data is valid";
                    inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                    _Common.CreateErrorLogWeb(inp);
                    InputParam.dtAsset = dt;
                    var exceloutput = _APIMappingData.UploadQSRQuantity(InputParam);
                    inp.ErrorDetails = "file uploaded";
                    inp.ErrorMessage = "file uploaded";
                    inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                    _Common.CreateErrorLogWeb(inp);
                    if (exceloutput.Data != null && exceloutput.MessageCode == (int)Acceptable.Created)
                    {
                        inp.ErrorDetails = "success";
                        inp.ErrorMessage = "success";
                        inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                        _Common.CreateErrorLogWeb(inp);
                        InputParam.dtAsset = exceloutput.Data;
                        _AssignTargetToSFAUploadOutput = ConvertTo<QSRQuantityReportUploadOutput>(exceloutput.Data);
                        output.Data = _AssignTargetToSFAUploadOutput;
                        output.MessageCode = exceloutput.MessageCode;
                        ViewBag.Status = exceloutput.MessageCode.ToString();
                        output.Message = ViewBag.Message = exceloutput.Message;
                    }

                    else if (output.MessageCode == (int)NotAcceptable.NotModified)
                    {
                        ViewBag.Status = "404";
                        ViewBag.Message = exceloutput.Message;
                        inp.ErrorDetails = "error from api";
                        inp.ErrorMessage = exceloutput.Message;
                        inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                        _Common.CreateErrorLogWeb(inp);
                        output.Data = new List<QSRQuantityReportUploadOutput>();
                        output.MessageCode = exceloutput.MessageCode;
                        output.Message = ViewBag.Message = exceloutput.Message;
                    }
                }
                else
                {
                    ViewBag.Status = ErrorStatus[0];
                    ViewBag.Message = ErrorStatus[1];
                    inp.ErrorDetails = "error in dt";
                    inp.ErrorMessage = ErrorStatus[1];
                    inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                    _Common.CreateErrorLogWeb(inp);
                    output.Data = new List<QSRQuantityReportUploadOutput>();
                    output.MessageCode = 404;
                    output.Message = ErrorStatus[1];
                }
            }
            else
            {
                ViewBag.Status = ErrorStatus[0];
                ViewBag.Message = ErrorStatus[1];
                inp.ErrorDetails = "error in file";
                inp.ErrorMessage = ErrorStatus[1];
                inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                _Common.CreateErrorLogWeb(inp);
                output.Data = new List<QSRQuantityReportUploadOutput>();
                output.MessageCode = 404;
                output.Message = ErrorStatus[1];
            }

            var jsonresult = Json(output, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = Int32.MaxValue;
            return jsonresult;
        }
        private DataTable UploadExcel(HttpPostedFileBase postedFile)
        {
            ErrorDetailsInput inp = new ErrorDetailsInput();
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
                        inp.ErrorDetails = "extension is valid";
                        inp.ErrorMessage = "extension is valid";
                        inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                        _Common.CreateErrorLogWeb(inp);
                        string FilePath = Server.MapPath("~/DownloadTemplate/" + FileName);
                        if (System.IO.File.Exists(Server.MapPath("~/DownloadTemplate/" + FileName)))
                        {
                            // delete file from server 
                            System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                            inp.ErrorDetails = "existing file deleted";
                            inp.ErrorMessage = "existing file deleted";
                            inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                            _Common.CreateErrorLogWeb(inp);
                        }
                        postedFile.SaveAs(FilePath);
                        string conString = string.Empty;
                        switch (Extension)
                        {
                            case ".xls": //Excel 97-03
                                conString = ConfigurationManager.AppSettings["Excel03ConString"];//provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended properties='Excel 8.0; HDR=yes;'
                                break;
                            case ".xlsx": //Excel 07 or higher
                                conString = ConfigurationManager.AppSettings["Excel07ConString"];
                                //conString = ConfigurationManager.AppSettings["provider =microsoft.ACE.OLEDB.12.0;Data Source={0};Extended properties='Excel 12.0; HDR=yes;'"];
                                break;

                        }
                        inp.ErrorDetails = "provider is valid";
                        inp.ErrorMessage = "provider is valid";
                        inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                        _Common.CreateErrorLogWeb(inp);
                        conString = string.Format(conString, FilePath);
                        OleDbConnection oledbConn = new OleDbConnection(conString);
                        if (oledbConn.State == ConnectionState.Closed)
                            oledbConn.Open();
                        inp.ErrorDetails = "connection opened";
                        inp.ErrorMessage = "connection opened";
                        inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                        _Common.CreateErrorLogWeb(inp);
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
                                if ((dt.Columns[0].ColumnName != "Branch") ||
                                (dt.Columns[1].ColumnName != "Date") ||
                                (dt.Columns[2].ColumnName != "DealerName") ||
                                (dt.Columns[3].ColumnName != "City") ||
                                (dt.Columns[4].ColumnName != "Location") ||
                                (dt.Columns[5].ColumnName != "PayerName") ||
                                (dt.Columns[6].ColumnName != "Channel") ||
                                (dt.Columns[7].ColumnName != "DealerState") ||
                                (dt.Columns[8].ColumnName != "MasterCode") ||
                                (dt.Columns[9].ColumnName != "DealerCode") ||
                                (dt.Columns[10].ColumnName != "DealerClassification") ||
                                (dt.Columns[11].ColumnName != "SFACode") ||
                                (dt.Columns[12].ColumnName != "SFAName") ||
                                (dt.Columns[13].ColumnName != "SFALevel") ||
                                (dt.Columns[14].ColumnName != "CompanyName") ||
                                (dt.Columns[15].ColumnName != "ProductCategory") ||
                                (dt.Columns[16].ColumnName != "Material") ||
                                (dt.Columns[17].ColumnName != "SFAType") ||
                                (dt.Columns[18].ColumnName != "AmboQuantity") ||
                                (dt.Columns[19].ColumnName != "QSRQuantity") ||
                                (dt.Columns[20].ColumnName != "FinalQuantity"))
                                {
                                    System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                                    ErrorStatus[0] = "404";
                                    ErrorStatus[1] = "Kindly Correct the column names.";
                                    inp.ErrorDetails = "Kindly Correct the column names.";
                                    inp.ErrorMessage = "Kindly Correct the column names.";
                                    inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                                    _Common.CreateErrorLogWeb(inp);
                                    return null;
                                }
                                //foreach (DataColumn column in dt.Columns) //validate null value in any cell
                                //{
                                // if (dt.Rows.OfType<DataRow>().Any(r => r.IsNull(column)))
                                // {
                                // System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                                // return null;
                                // }
                                //}
                            }
                        }
                    }

                    else
                    {
                        System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                        ErrorStatus[0] = "404";
                        ErrorStatus[1] = "Sorry! Kindly Select Excel file only.";
                        inp.ErrorDetails = "Kindly Correct the file.";
                        inp.ErrorMessage = "Kindly Correct the file.";
                        inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                        _Common.CreateErrorLogWeb(inp);
                        return null;
                    }
                    if (System.IO.File.Exists(Server.MapPath("~/DownloadTemplate/" + FileName)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                    }
                }

                else
                {
                    ErrorStatus[0] = "404";
                    ErrorStatus[1] = "Please Select valid excel file.";
                    inp.ErrorDetails = "no file";
                    inp.ErrorMessage = "no file";
                    inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                    _Common.CreateErrorLogWeb(inp);
                    return null;
                }

                return dt;
            }
            catch (Exception ex)
            {
                ErrorStatus[0] = "404";
                ErrorStatus[1] = ex.Message;
                //ErrorDetailsInput inp = new ErrorDetailsInput();
                inp.ErrorDetails = ex.StackTrace;
                inp.ErrorMessage = ex.Message;
                inp.ErrorSource = "AssignTargetToSFAExcelUpload";
                _Common.CreateErrorLogWeb(inp);
                return null;
            }
        }
        #region Assign Festival Target to SFA
        [HttpGet]
        public ActionResult AssignFestivalTarget()
        {

            return View();

        }

        [HttpPost]
        public ActionResult ShowFestivalTarget(int SchemeId)
        {
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            AssignFestivalTargetGet input = new AssignFestivalTargetGet();
            input.UserId = objUserSession.UserId;
            input.FestivalSchemeId = SchemeId;
            List<AssignFestivalTargetGrid> targetList = new List<AssignFestivalTargetGrid>();

            targetList = _APIMappingData.ShowFestivalTargetToSFABySchemeId(input);
            //var result = new ContentResult
            //{
            // Content = serializer.Serialize(targetList),
            // ContentType = "application/json"
            //};
            //return result; 
            var jsonresult = Json(targetList, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = Int32.MaxValue;
            return jsonresult;
        }

        [HttpPost]
        public ActionResult ExportFestivalTargetToSFAData(int SchemeId)
        {
            try
            {
                AssignFestivalTargetGet Input = new AssignFestivalTargetGet();
                Input.UserId = objUserSession.UserId;
                Input.FestivalSchemeId = SchemeId;

                DataTable dt = new DataTable("FestivalTargetToSFA");
                dt.Columns.AddRange(new DataColumn[15] { new DataColumn("FestivalScheme"),
new DataColumn("Branch"),
new DataColumn("City"),
new DataColumn("Location"),
new DataColumn("SFACode"),
new DataColumn("SFAName"),
new DataColumn("DealerCode"),
new DataColumn("MasterCode"),
new DataColumn("DealerName"),
new DataColumn("SFACategory"),
new DataColumn("FestivalIncentiveCategory"),
new DataColumn("TargetCategory"),
new DataColumn("ProductCategory"),
new DataColumn("QtyTarget"),
new DataColumn("ValueTarget"),});


                List<AssignFestivalTargetGrid> targetList = new List<AssignFestivalTargetGrid>();
                targetList = _APIMappingData.GetFestivalTargetToSFABySchemeId(Input);

                if (targetList != null && targetList.Count > 0)
                {
                    foreach (var record in targetList)
                    {
                        dt.Rows.Add(record.FestivalScheme, record.Branch, record.City, record.Location, record.SFACode, record.SFAName,
                        record.DealerCode, record.MasterCode, record.DealerName, record.SFACategory, record.FestivalIncentiveCategory,
                        record.TargetCategory, record.ProductCategory, record.QtyTarget, record.ValueTarget);
                    }

                    var fileName = "FestivalTargetToSFA_Data.xlsx";
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
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult DownloadFormat()
        {
            try
            {
                return File("~/DownloadTemplate/AssignFestivalTargetToSFA.xlsx", "application/vnd.ms-excel", "AssignFestivalTargetToSFA.xlsx");
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult UploadDataFestivalTargetToSFA(HttpPostedFileBase file, Int64 SchemeId)
        {
            AssignFestivalTargetUpload InputParam = new AssignFestivalTargetUpload();

            InputParam.UserId = objUserSession.UserId;
            InputParam.FestivalSchemeId = SchemeId;

            Envelope<List<AssignFestivalTargetUploadOutput>> output = new Envelope<List<AssignFestivalTargetUploadOutput>>();
            List<AssignFestivalTargetUploadOutput> _AssignFestivalTargetToSFAUploadOutput = new List<AssignFestivalTargetUploadOutput>();

            if (file != null)
            {
                var dt = UploadExcelFestivalTargetToSFA(file);
                if (dt != null)
                {
                    InputParam.dtAsset = dt;
                    var exceloutput = _APIMappingData.UploadFestivalTargetToSFAByScheme(InputParam);
                    if (exceloutput.Data != null && exceloutput.MessageCode == (int)Acceptable.Created)
                    {
                        InputParam.dtAsset = exceloutput.Data;
                        _AssignFestivalTargetToSFAUploadOutput = ConvertTo<AssignFestivalTargetUploadOutput>(exceloutput.Data);
                        output.Data = _AssignFestivalTargetToSFAUploadOutput;
                        output.MessageCode = exceloutput.MessageCode;
                        ViewBag.Status = exceloutput.MessageCode.ToString();
                        output.Message = ViewBag.Message = exceloutput.Message;
                    }
                    else if (output.MessageCode == (int)NotAcceptable.NotModified)
                    {
                        ViewBag.Status = "404";
                        ViewBag.Message = exceloutput.Message;
                    }
                }
                else
                {
                    ViewBag.Status = ErrorStatus[0];
                    ViewBag.Message = ErrorStatus[1];
                }
            }
            else
            {
                ViewBag.Status = ErrorStatus[0];
                ViewBag.Message = ErrorStatus[1];
            }

            var jsonresult = Json(output, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = Int32.MaxValue;
            return jsonresult;

        }
        private DataTable UploadExcelFestivalTargetToSFA(HttpPostedFileBase postedFile)
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
                                if (
                                (dt.Columns[0].ColumnName != "FestivalScheme") ||
                                (dt.Columns[1].ColumnName != "Branch") ||
                                (dt.Columns[2].ColumnName != "City") ||
                                (dt.Columns[3].ColumnName != "Location") ||
                                (dt.Columns[4].ColumnName != "SFACode") ||
                                (dt.Columns[5].ColumnName != "SFAName") ||
                                (dt.Columns[6].ColumnName != "DealerCode") ||
                                (dt.Columns[7].ColumnName != "MasterCode") ||
                                (dt.Columns[8].ColumnName != "DealerName") ||
                                (dt.Columns[9].ColumnName != "SFACategory") ||
                                (dt.Columns[10].ColumnName != "FestivalIncentiveCategory") ||
                                (dt.Columns[11].ColumnName != "TargetCategory") ||
                                (dt.Columns[12].ColumnName != "ProductCategory") ||
                                (dt.Columns[13].ColumnName != "QtyTarget") ||
                                (dt.Columns[14].ColumnName != "ValueTarget"))
                                {
                                    System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                                    ErrorStatus[0] = "404";
                                    ErrorStatus[1] = "Kindly Correct the column names.";
                                    return null;
                                }
                                //foreach (DataColumn column in dt.Columns) //validate null value in any cell
                                //{
                                // if (dt.Rows.OfType<DataRow>().Any(r => r.IsNull(column)))
                                // {
                                // System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                                // return null;
                                // }
                                //}
                            }
                        }
                    }
                    else
                    {
                        System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                        ErrorStatus[0] = "404";
                        ErrorStatus[1] = "Sorry! Kindly Select Excel file only.";
                        return null;
                    }
                    System.IO.File.Delete(Server.MapPath("~/DownloadTemplate/" + FileName));
                }

                else
                {
                    ErrorStatus[0] = "404";
                    ErrorStatus[1] = "Please Select valid excel file.";
                    return null;
                }

                return dt;
            }
            catch (Exception ex)
            {
                ErrorStatus[0] = "404";
                ErrorStatus[1] = ex.Message;
                ErrorDetailsInput inp = new ErrorDetailsInput();
                inp.ErrorDetails = ex.StackTrace;
                inp.ErrorMessage = ex.Message;
                inp.ErrorSource = "AssignFestivalTargetToSFAExcelUpload";
                _Common.CreateErrorLogWeb(inp);
                return null;
            }
        }
        #endregion

    }

}