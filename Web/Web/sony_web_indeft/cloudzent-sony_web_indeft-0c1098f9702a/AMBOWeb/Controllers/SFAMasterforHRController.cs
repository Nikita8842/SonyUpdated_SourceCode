using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMBOModels.Global;
using AMBOModels.UserManagement;
using APIAccessLayer.INTERFACE;
using AMBOModels.GlobalAccessible;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using static AMBOWeb.Classes.Common;
using AMBOWeb.Filters;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using ClosedXML.Excel;
using System.Text;
using AMBOModels.Reports;
using AMBOModels.UserValidation;

namespace AMBOWeb.Controllers
{
    public class SFAMasterforHRController : SonyBaseController
    {
        private readonly IAPIWebReportsData _APIWebReportsData;
        private readonly IAPISFAMasterforHRData _SFAMasterforHRData;
        private readonly IAPICommon _APICommon;

        private string[] ErrorStatus = new string[2];

        public string messageoutput = null;
        public SFAMasterforHRController(IAPIWebReportsData IAPIWebReportsData, IAPISFAMasterforHRData IAPISFAMasterforHRData, IAPICommon IAPICommon)
        {
            _APIWebReportsData = IAPIWebReportsData;
            _SFAMasterforHRData = IAPISFAMasterforHRData;
            _APICommon = IAPICommon;
        }

        // GET: SFAMasterforHR
        [SessionAuthorize(ModuleId = (Int64)Modules.HR, SubModuleId = (Int64)SubModules.SFAMasterforHR, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            GetBranch InputParam = new GetBranch();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;
            SFAMasterforHR view = new SFAMasterforHR();
            if (rolen == "Branch HR")
            {
                var details = _APICommon.GetBranchMappedForHR(InputParam);
                if (details != null && details.BranchIds != null)
                {
                    view.BranchIds = details.BranchIds.ToArray();
                }
                //view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            return View(view);
        }

        [HttpPost]
        [ValidateInput(false)]
        [SessionAuthorize(ModuleId = (Int64)Modules.HR, SubModuleId = (Int64)SubModules.SFAMasterforHR, Rights = (int)Right.Delete)]
        public JsonResult DeleteSFAMasterforHR(SFAMasterforHRDelete objFormData)
        {
            objFormData.UserId = objUserSession.UserId;
            var output = _SFAMasterforHRData.DeleteSFAMasterforHR(objFormData);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.HR, SubModuleId = (Int64)SubModules.SFAMasterforHR, Rights = (int)Right.Edit)]
        public ActionResult UpdateSFAMasterforHR(Int64 LoginId)
        {
            SFAMasterforHR Data = new SFAMasterforHR();
            SFAMasterforHR InputData = new SFAMasterforHR();

            InputData.LoginId = Convert.ToInt64(LoginId);
            Data = _SFAMasterforHRData.GetSFAMasterforHRbyId(InputData);

            return View(Data);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.HR, SubModuleId = (Int64)SubModules.SFAMasterforHR, Rights = (int)Right.Edit)]
        public ActionResult UpdateSFAMasterforHR(SFAMasterforHR InputParam)
        {
            if (ModelState.IsValid)
            {
                InputParam.UserId = objUserSession.UserId;
                Envelope<bool> output;
                if (InputParam.Id == 0)
                    output = _SFAMasterforHRData.CreateSFAMasterforHR(InputParam);

                else
                    output = _SFAMasterforHRData.UpdateSFAMasterforHR(InputParam);
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

        [HttpGet]
        public JsonResult GetAllDetailsSFAMasterforHR()
        {
            var output = _SFAMasterforHRData.GetAllDetailsSFAMasterforHR();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateFormat(SFAMasterforHRDownload InputParam)
        {
            try
            {
                var fileName = "SFAMasterforHR_Format.xlsx";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                DataTable dt = new DataTable("SFAMasterforHR");
                dt.Columns.AddRange(new DataColumn[30] { new DataColumn("SFACode"),
                                        new DataColumn("SFAName"),
                                        new DataColumn("Source"),
                                        new DataColumn("SourceName"),
                                        new DataColumn("SourceCode"),
                                        new DataColumn("DateofLeaving"),
                                        new DataColumn("DateofJoining"),
                                        new DataColumn("MobileIssued"),
                                        new DataColumn("DateofBirth"),
                                        new DataColumn("LevelOfEducation"),
                                        new DataColumn("Education"),
                                        new DataColumn("Experience"),
                                        new DataColumn("BankName"),
                                        new DataColumn("BankAccountNo"),
                                        new DataColumn("Region"),
                                        new DataColumn("Branch"),
                                        new DataColumn("City"),
                                        new DataColumn("Division"),
                                        new DataColumn("SubLevel"),
                                        new DataColumn("ESIAccountNo"),
                                        new DataColumn("PFAccountNo"),
                                        new DataColumn("MedicalInsuranceNo"),
                                        new DataColumn("MICoverageFrom"),
                                        new DataColumn("MICoverageTo"),
                                        new DataColumn("PersonalAccidentInsuranceNo"),
                                        new DataColumn("PICoverageFrom"),
                                        new DataColumn("PICoverageTo"),
                                        new DataColumn("Address"),
                                        new DataColumn("EmailAddress"),
                                        new DataColumn("MobileNumber"),});

                if (InputParam.BranchIds != null || (InputParam.FromDate != null && InputParam.ToDate != null))
                {
                    List<SFAMasterforHR> listOutput = new List<SFAMasterforHR>();
                    listOutput = _SFAMasterforHRData.SFAMasterforHRDataDownload(InputParam);

                    if (listOutput != null && listOutput.Count > 0)
                    {
                        foreach (var record in listOutput)
                        {
                            dt.Rows.Add(record.SFACode, record.SFAName, record.Source, record.SourceName, record.SourceCode, record.DOL,
                                record.DOJ, record.AssetIssued, record.DOB, record.LevelofEducation, record.Education,
                                record.Experience, record.BankName, record.BankAccountNo, record.Region, record.Branch, record.City, record.Division,
                                record.SFALevel, record.ESIAccountNo, record.PFAccountNo, record.MedicalInsuranceNo, record.MICoverageFrom,
                                record.MICoverageTo, record.PersonalInsuranceNo, record.PICoverageFrom, record.PICoverageTo, record.Address, record.EmailId,
                                record.MobileNumber);
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
            GetBranch InputParam = new GetBranch();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;
            if (rolen == "Branch HR")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                ViewBag.BranchId = details.BranchId;
            }
            else
                ViewBag.BranchId = 0;
            return PartialView("PartialViews/SFAMasterforHRUpload", new DataTable());
        }

        public ActionResult UploadData(HttpPostedFileBase ExcelUploadFile)
        {
            List<SFAMasterforHR> listInputData = new List<SFAMasterforHR>();
            List<SFAMasterforHRUpload> listOutputData = new List<SFAMasterforHRUpload>();
            //string filePath = "";
            DataTable dtResult = new DataTable();
            dtResult = null;

            //filePath = Server.MapPath("~/DownloadTemplate/") + Path.GetFileName(ExcelUploadFile.FileName);
            //if (System.IO.File.Exists(filePath))
            //{
            //    // delete file from server 
            //    System.IO.File.Delete(filePath);
            //}
            //ExcelUploadFile.SaveAs(filePath);

            DataTable dt = UploadExcel(ExcelUploadFile);

            Envelope<DataTable> output = null;

            if (dt != null || dt.Rows.Count > 0)
            {
                listInputData = (from DataRow dr in dt.Rows
                                 select new SFAMasterforHR()
                                 {
                                     UserId = objUserSession.UserId,
                                     SFACode = dr["SFACode"].ToString(),
                                     SFAName = dr["SFAName"].ToString(),
                                     Source = dr["Source"].ToString(),
                                     SourceName = dr["SourceName"].ToString(),
                                     SourceCode = dr["SourceCode"].ToString(),
                                     DOL = dr["DateofLeaving"].ToString(),
                                     DOJ = dr["DateofJoining"].ToString(),
                                     AssetIssued = dr["MobileIssued"].ToString(),
                                     DOB = dr["DateofBirth"].ToString(),
                                     LevelofEducation = dr["LevelOfEducation"].ToString(),
                                     Education = dr["Education"].ToString(),
                                     Experience = dr["Experience"].ToString(),
                                     BankName = dr["BankName"].ToString(),
                                     BankAccountNo = dr["BankAccountNo"].ToString(),
                                     Region = dr["Region"].ToString(),
                                     Branch = dr["Branch"].ToString(),
                                     City = dr["City"].ToString(),
                                     Division = dr["Division"].ToString(),
                                     SFALevel = dr["SubLevel"].ToString(),
                                     ESIAccountNo = dr["ESIAccountNo"].ToString(),
                                     PFAccountNo = dr["PFAccountNo"].ToString(),
                                     MedicalInsuranceNo = dr["MedicalInsuranceNo"].ToString(),
                                     MICoverageFrom = dr["MICoverageFrom"].ToString(),
                                     MICoverageTo = dr["MICoverageTo"].ToString(),
                                     PersonalInsuranceNo = dr["PersonalAccidentInsuranceNo"].ToString(),
                                     PICoverageFrom = dr["PICoverageFrom"].ToString(),
                                     PICoverageTo = dr["PICoverageTo"].ToString(),
                                     Address = dr["Address"].ToString(),
                                     EmailId = dr["EmailAddress"].ToString(),
                                     MobileNumber = dr["MobileNumber"].ToString()
                                 }).ToList();

                output = _SFAMasterforHRData.ManageSFAMasterforHRData(listInputData);

                if (output.Data != null && output.MessageCode == (int)Acceptable.Created)
                {
                    dtResult = output.Data;
                    //AddSuccessNotification(output.Message);
                }
                else
                    ViewBag.Message = output.Message;

                //foreach (var input in listInputData)
                //{
                //    var output = _SFAMasterforHRData.CreateSFAMasterforHR(input);
                //    listOutputData.Add(new SFAMasterforHRUpload
                //    {
                //        SFACode = input.SFACode,
                //        SFAName = input.SFAName,
                //        Branch = input.Branch,
                //        City = input.City,
                //        Division = input.Division,
                //        Status = output.MessageCode == (int)Acceptable.Created ? "Successful" : "Unsuccessful",
                //        Reason = output.Message
                //    });
                //}

            }
            else
                ViewBag.Message = ErrorStatus[1];
            return PartialView("PartialViews/SFAMasterforHRUpload", dtResult);

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
                                if (!(dt.Columns[0].ColumnName == "SFACode" &&
                                    dt.Columns[1].ColumnName == "SFAName" &&
                                    dt.Columns[2].ColumnName == "Source" &&
                                    dt.Columns[3].ColumnName == "SourceName" &&
                                    dt.Columns[4].ColumnName == "SourceCode" &&
                                    dt.Columns[5].ColumnName == "DateofLeaving" &&
                                    dt.Columns[6].ColumnName == "DateofJoining" &&
                                    dt.Columns[7].ColumnName == "MobileIssued" &&
                                    dt.Columns[8].ColumnName == "DateofBirth" &&
                                    dt.Columns[9].ColumnName == "LevelOfEducation" &&
                                    dt.Columns[10].ColumnName == "Education" &&
                                    dt.Columns[11].ColumnName == "Experience" &&
                                    dt.Columns[12].ColumnName == "BankName" &&
                                    dt.Columns[13].ColumnName == "BankAccountNo" &&
                                    dt.Columns[14].ColumnName == "Region" &&
                                    dt.Columns[15].ColumnName == "Branch" &&
                                    dt.Columns[16].ColumnName == "City" &&
                                    dt.Columns[17].ColumnName == "Division" &&
                                    dt.Columns[18].ColumnName == "SubLevel" &&
                                    dt.Columns[19].ColumnName == "ESIAccountNo" &&
                                    dt.Columns[20].ColumnName == "PFAccountNo" &&
                                    dt.Columns[21].ColumnName == "MedicalInsuranceNo" &&
                                    dt.Columns[22].ColumnName == "MICoverageFrom" &&
                                    dt.Columns[23].ColumnName == "MICoverageTo" &&
                                    dt.Columns[24].ColumnName == "PersonalAccidentInsuranceNo" &&
                                    dt.Columns[25].ColumnName == "PICoverageFrom" &&
                                    dt.Columns[26].ColumnName == "PICoverageTo" &&
                                    dt.Columns[27].ColumnName == "Address" &&
                                    dt.Columns[28].ColumnName == "EmailAddress" &&
                                    dt.Columns[29].ColumnName == "MobileNumber"))
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
        //        if (dt.Columns.Count == 30)
        //        {
        //            if (dt.Columns[0].ColumnName  == "SFACode" &&
        //                dt.Columns[1].ColumnName  == "SFAName" &&
        //                dt.Columns[2].ColumnName  == "Source" &&
        //                dt.Columns[3].ColumnName  == "SourceName" &&
        //                dt.Columns[4].ColumnName  == "SourceCode" &&
        //                dt.Columns[5].ColumnName  == "DateofLeaving" &&
        //                dt.Columns[6].ColumnName  == "DateofJoining" &&
        //                dt.Columns[7].ColumnName  == "MobileIssued" &&
        //                dt.Columns[8].ColumnName  == "DateofBirth" &&
        //                dt.Columns[9].ColumnName  == "LevelOfEducation" &&
        //                dt.Columns[10].ColumnName  == "Education" &&
        //                dt.Columns[11].ColumnName  == "Experience" &&
        //                dt.Columns[12].ColumnName  == "BankName" &&
        //                dt.Columns[13].ColumnName  == "BankAccountNo" &&
        //                dt.Columns[14].ColumnName  == "Region" &&
        //                dt.Columns[15].ColumnName  == "Branch" &&
        //                dt.Columns[16].ColumnName  == "City" &&
        //                dt.Columns[17].ColumnName  == "Division" &&
        //                dt.Columns[18].ColumnName  == "SubLevel" &&
        //                dt.Columns[19].ColumnName  == "ESIAccountNo" &&
        //                dt.Columns[20].ColumnName  == "PFAccountNo" &&
        //                dt.Columns[21].ColumnName  == "MedicalInsuranceNo" &&
        //                dt.Columns[22].ColumnName  == "MICoverageFrom" &&
        //                dt.Columns[23].ColumnName  == "MICoverageTo" &&
        //                dt.Columns[24].ColumnName  == "PersonalAccidentInsuranceNo" &&
        //                dt.Columns[25].ColumnName  == "PICoverageFrom" &&
        //                dt.Columns[26].ColumnName  == "PICoverageTo" &&
        //                dt.Columns[27].ColumnName  == "Address" &&
        //                dt.Columns[28].ColumnName  == "EmailAddress" &&
        //                dt.Columns[29].ColumnName  == "MobileNumber")
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


        #region AttendanceApprovalReport

        [HttpGet]
        public ActionResult AttendanceApprovalReport()
        {
            GetBranch InputParam = new GetBranch();


            MonthlyAttendanceReportInput view = new MonthlyAttendanceReportInput();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;
            Session["Role"] = rolen.ToUpper();
            if (rolen == "RDI" || rolen == "Sony Admin")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;

                //Commented this line of code because it is fetching the branch of user not its associated mapped branch...
                //var details = _APICommon.GetBranchByUserId(InputParam);
                //  view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else if (rolen == "Branch HR")
            {
                // Commented this line of code because it is fetching the branch of user not its associated mapped branch...
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
        public PartialViewResult GetAttendanceApprovalReport(MonthlyAttendanceReportInput objGridData)
        {

            string Msg = "";
            ApprovalData objApprovalData = new ApprovalData();
            try
            {
                if (objGridData.StartDate == null || objGridData.StartDate == "")
                    objGridData.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
                if (objGridData.EndDate == null || objGridData.EndDate == "")
                    objGridData.EndDate = DateTime.Now.ToString("dd/MM/yyyy");

                objGridData.UserId = objUserSession.UserId;

                // Prior to report we have to check whether any branch attendance is approved or not if yes then only we will fetch the monthly report
                var isApprovedBranch = _APIWebReportsData.IsApprovedBranch(objGridData);

                if (isApprovedBranch.Data == true)
                {

                    #region MyRegion

                    var output = _APIWebReportsData.GetMonthlyAttendanceReport(objGridData);
                    if (output != null || output.Data != null)
                    {
                        UpdateMonthlyReport objUpdateMonthlyReport = new UpdateMonthlyReport();
                        objUpdateMonthlyReport.UserId = objUserSession.UserId;
                        objUpdateMonthlyReport.RoleName = objUserSession.RoleName;
                        objUpdateMonthlyReport.SFAId = objGridData.SFAId;
                        objUpdateMonthlyReport.StartDate = objGridData.StartDate;
                        objUpdateMonthlyReport.EndDate = objGridData.EndDate;

                        List<ApprovalGrid> objlist = new List<ApprovalGrid>();
                        objlist = _APIWebReportsData.GETApprovalDateStatusWise(objUpdateMonthlyReport).Data;

                        if (objlist != null && objlist.Count > 0)
                        {
                            foreach (DataRow dr in output.Data.Rows)
                            {
                                if (objlist.Any(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()))
                                {
                                    var present = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "P").Count();
                                    var absent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "A").Count();
                                    var leave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "L").Count();
                                    var training = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "T/M").Count();
                                    var weeklyoff = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "W").Count();
                                    var leavetoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "L").Count();
                                    var presenttoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "P").Count();
                                    var weeklyofftoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "T/M").Count();
                                    var trainingtoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "W").Count();
                                    var weeklyofftopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "W").Count();
                                    var presenttoweekly = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "W" && a.OldAttendanceType == "P").Count();
                                    var presenttoleave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "L" && a.OldAttendanceType == "P").Count();
                                    var weeklytoleave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "L" && a.OldAttendanceType == "W").Count();
                                    var leavetoweekly = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "W" && a.OldAttendanceType == "L").Count();
                                    var leavetopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[19].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "L").Count();
                                    //  Commented because of extra column division is added in to the report 
                                    //present = (Convert.ToInt32(dr.ItemArray[12]) + present) - (presenttoweekly + presenttoleave);
                                    ////absent = Convert.ToInt32(dr.ItemArray[15]) + absent;                            
                                    //leave = (Convert.ToInt32(dr.ItemArray[14]) + leave) - (leavetoweekly + leavetopresent);
                                    //training = Convert.ToInt32(dr.ItemArray[15]) + training;
                                    //weeklyoff = (Convert.ToInt32(dr.ItemArray[13]) + weeklyoff) - (weeklyofftopresent + weeklytoleave);




                                    present = (Convert.ToInt32(dr.ItemArray[13]) + present) - (presenttoweekly + presenttoleave);
                                    //absent = Convert.ToInt32(dr.ItemArray[15]) + absent;                            
                                    leave = (Convert.ToInt32(dr.ItemArray[15]) + leave) - (leavetoweekly + leavetopresent);
                                    training = Convert.ToInt32(dr.ItemArray[16]) + training;
                                    weeklyoff = (Convert.ToInt32(dr.ItemArray[14]) + weeklyoff) - (weeklyofftopresent + weeklytoleave);


                                    var absentupdate = Convert.ToInt32(output.Data.Columns.Count - 20) - (present + weeklyoff + training + leave - (leavetoabsent + presenttoabsent + trainingtoabsent + weeklyofftoabsent));
                                    dr.SetField("Absent", absentupdate.ToString());

                                    present = present - presenttoabsent;
                                    dr.SetField("Present", present.ToString());
                                    leave = leave - leavetoabsent;
                                    dr.SetField("Leave", leave.ToString());
                                    training = training - trainingtoabsent;
                                    dr.SetField("Training/Meeting", training.ToString());
                                    weeklyoff = weeklyoff - weeklyofftoabsent;
                                    dr.SetField("WeeklyOff", weeklyoff.ToString());
                                    //var total = present + weeklyoff + training + leave + absentupdate;
                                    //var total = Convert.ToInt32(output.Data.Columns.Count - 19);
                                    //dr.SetField("Total", total.ToString());
                                }
                            }
                        }



                        objApprovalData.ApprovalGrid = objlist;
                        objApprovalData.Dt = output.Data;
                    } 
                    #endregion

                    if (output.MessageCode == (int)Acceptable.Found)
                        return PartialView("_HRAttendanceApprovalReport", objApprovalData);

                }
                
            }
            catch (Exception ex)
            {
                Msg = ex.Message.ToString();
            }
            return PartialView("_HRAttendanceApprovalReport", objApprovalData);

        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public JsonResult UpdateAttendance(UpdateMonthlyReport objFormData)
        //{
        //    string Message = "";
        //    Envelope<bool> output = new Envelope<bool>();
        //    try
        //    {
        //        //  objFormData.Remarks = null;
        //        if (objFormData.Remarks == null)
        //        {
        //            output.Message = "Please Enter Remarks";
        //            AddErrorNotification(output.Message);
        //            return Json(output);
        //        }
        //        objFormData.UserId = objUserSession.UserId;
        //        objFormData.EncryptKey = objUserSession.EncryptKey;
        //        objFormData.RoleName = objUserSession.RoleName;
        //        output = _APIWebReportsData.UpdateAttendance(objFormData);
        //        //if(output.MessageCode == (int)Acceptable.Accepted)
        //        //{
        //        //    AddSuccessNotification(output.Message);
        //        //}
        //        return Json(output);
        //    }
        //    catch (Exception ex)
        //    {
        //        Message = ex.Message;
        //        return Json(new Envelope<bool> { Data = false, Message = Message, MessageCode = 0 });
        //    }


        //}

        [HttpPost]
        public ActionResult ExportApprovalRecordExcel(UpdateMonthlyReport objGridData)
        {
            String Msg = "";
            try
            {
                if (objGridData.StartDate == null || objGridData.StartDate == "")
                    objGridData.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
                if (objGridData.EndDate == null || objGridData.EndDate == "")
                    objGridData.EndDate = DateTime.Now.ToString("dd/MM/yyyy");
                var userDetails = (BaseSession)(Session["BaseSession"]);
                //if (userDetails.RoleName == "RDI")
                //    objGridData.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
                //else
                //    objGridData.BranchId = 0;
                objGridData.UserId = objUserSession.UserId;
                objGridData.RoleName = objUserSession.RoleName;
                DataTable output = _APIWebReportsData.GetMonthlyAttendanceApprovalReport(objGridData).Data;
                var fileName = "MonthlyAttendanceApproval_Data.xls";
                string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                //save the file to server temp folder
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                //fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                output.TableName = "Attendance Approval Report";
                if (output.Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(output, output.TableName);
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
                Msg = ex.Message.ToString();
            }
            return null;
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult DownloadFinalReport(string GridHtml)
        {
            // to remove search and index from the inner html .
            GridHtml = GridHtml.Replace("Search:", "");
            int firstindex = GridHtml.IndexOf("Showing");
            int lastindex = GridHtml.IndexOf("entries");
            StringBuilder sb = new StringBuilder();
            sb.Append(GridHtml.Substring(0, firstindex));
            sb.Append(GridHtml.Substring(lastindex + 7));
            GridHtml = string.Empty;
            GridHtml = sb.ToString();

            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "AttendanceApprovalReport.xls");
        }



        [HttpGet]
        [DeleteFileAttribute]
        public ActionResult Download(string file)
        {
            //string fullPath = Path.Combine(Server.MapPath("~/DownloadTemplate"), file);
            string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), file);
            //return the file for download, this is an Excel 
            //so I set the file content type to "application/vnd.ms-excel"
            return File(fullPath, "application/vnd.ms-excel", file);
        }


        #endregion


    }
}