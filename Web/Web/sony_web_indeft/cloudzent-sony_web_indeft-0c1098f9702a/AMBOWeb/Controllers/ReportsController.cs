using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.INTERFACE;
using AMBOModels.Reports;
using APIAccessLayer.Helper;
using AMBOModels.UserValidation;
using System.Net.Http;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using ClosedXML;
//using OfficeOpenXml;
using System.Web.UI.WebControls;
using System.Web.UI;
using AMBOWeb.Filters;
using AMBOModels.GlobalAccessible;
using System.Threading.Tasks;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using Newtonsoft.Json;
using System.ComponentModel;
using AMBOWeb.Classes;
using APIAccessLayer.IMPLEMENTATION;

namespace AMBOWeb.Controllers
{
    public class ReportsController : SonyBaseController
    {
        private readonly IAPIWebReportsData _APIWebReportsData;
        private readonly IAPICommon _APICommon;
        private readonly IAPIMappingData _APIMappingData;
        private readonly IAPIIncentiveData _APIIncentiveData;
       

        public ReportsController(IAPIWebReportsData IAPIWebReportsData, IAPIMappingData IAPIMappingData, IAPICommon IAPICommon, IAPIIncentiveData IAPIIncentiveData)
        {
            _APIWebReportsData = IAPIWebReportsData;
            _APIMappingData = IAPIMappingData;
            _APICommon = IAPICommon;
            _APIIncentiveData = IAPIIncentiveData;
        }

        #region CompetitionDisplay
        [HttpGet]
        public ActionResult CompetitionDisplayReport()
        {
            GetBranch InputParam = new GetBranch();
            CompetitionDisplayReportFilters view = new CompetitionDisplayReportFilters();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            if (rolen == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            if (Session["CompetitionDisplayNoData"] == null)
                Session["CompetitionDisplayNoData"] = 0;

            return View(view);
        }

        [HttpPost]
        public ActionResult CompetitionDisplayReport(CompetitionDisplayReportFilters objGridData)
        {
            var output = _APIWebReportsData.GetCompetitionDisplayReport(objGridData);
            return Json(output.Data);
        }

        [HttpPost]
        public void DownloadCompetitionDisplayReport(CompetitionDisplayReportFilters objGridData)
        {
            objGridData.start = 0;
            objGridData.length = int.MaxValue;
            var output = _APIWebReportsData.GetCompetitionDisplayReport(objGridData);
            DataTable dt = new DataTable("CompetitionDisplayReport");

            if (output.MessageCode == (int)Acceptable.Found)
            {
                var listdata = output.Data.data.ToList();
                dt.Columns.AddRange(new DataColumn[17] {
                    new DataColumn("ActualDate"),
                    new DataColumn("BranchName"),
                    new DataColumn("OutletName"),
                    new DataColumn("City"),
                    new DataColumn("Location"),
                    new DataColumn("Channel"),
                    new DataColumn("DealerState"),
                    new DataColumn("MasterCode"),
                    new DataColumn("DealerCode"),
                    new DataColumn("DealerClass"),
                    new DataColumn("Division"),
                    new DataColumn("SFACode"),
                    new DataColumn("SFAName"),
                    new DataColumn("Brand"),
                    new DataColumn("ProdCat"),
                    new DataColumn("Model"),
                    new DataColumn("Quantity")
                });
                foreach (var record in listdata)
                {
                    dt.Rows.Add(record.ActualDate, record.BranchName, record.OutletName, record.City, record.Location,
                        record.Channel, record.DealerState, record.MasterCode, record.DealerCode, record.DealerClass,
                        record.Division, record.SFACode, record.SFAName, record.Brand, record.ProdCat, record.Model, record.Quantity);
                }

            }

            if (dt.Rows.Count > 0)
            {
                string attachment = "attachment; filename=CompetitionDisplayReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();

            }
            else
            {
                Session["CompetitionDisplayNoData"] = 1;
                Response.Redirect(Url.Action("CompetitionDisplayReport", "Reports", new
                {

                    exceptionType = this.GetType().Name
                }));
            }
        }
        #endregion CompetitionDisplay

        #region CompetitionSFACount
        [HttpGet]
        public ActionResult CompetitionSFACountReport()
        {
            GetBranch InputParam = new GetBranch();
            CompetitionSFACountReportFilters view = new CompetitionSFACountReportFilters();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            if (rolen == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            if (Session["CompetitionSFACountNoData"] == null)
                Session["CompetitionSFACountNoData"] = 0;

            return View(view);
        }

        [HttpPost]
        public ActionResult CompetitionSFACountReport(CompetitionSFACountReportFilters objGridData)
        {
            var output = _APIWebReportsData.GetCompetitionSFACountReport(objGridData);
            return Json(output.Data);
        }

        [HttpPost]
        public void DownloadCompetitionSFACountReport(CompetitionSFACountReportFilters objGridData)
        {
            objGridData.start = 0;
            objGridData.length = int.MaxValue;
            var output = _APIWebReportsData.GetCompetitionSFACountReport(objGridData);
            DataTable dt = new DataTable("CompetitionSFACountReport");

            if (output.MessageCode == (int)Acceptable.Found)
            {
                var listdata = output.Data.data.ToList();
                dt.Columns.AddRange(new DataColumn[17] {
                    new DataColumn("BranchName"),
                    new DataColumn("DealerName"),
                    new DataColumn("City"),
                    new DataColumn("Location"),
                    new DataColumn("Channel"),
                    new DataColumn("DealerState"),
                    new DataColumn("MasterCode"),
                    new DataColumn("DealerCode"),
                    new DataColumn("DealerClass"),
                    new DataColumn("SFACode"),
                    new DataColumn("SFAName"),
                    new DataColumn("SFALevel"),
                    new DataColumn("IncentiveCategory"),
                    new DataColumn("Division"),
                    new DataColumn("Brand"),
                    new DataColumn("Type"),
                    new DataColumn("Count")
                });
                foreach (var record in listdata)
                {
                    dt.Rows.Add(record.BranchName, record.DealerName, record.City, record.Location, record.Channel, record.DealerState,
                        record.MasterCode, record.DealerCode, record.DealerClass, record.SFACode, record.SFAName, record.SFALevel,
                        record.IncentiveCategory, record.Division, record.Brand, record.Type, record.Count);
                }

            }

            if (dt.Rows.Count > 0)
            {
                string attachment = "attachment; filename=CompetitionSFACountReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();

            }
            else
            {
                Session["CompetitionSFACountNoData"] = 1;
                Response.Redirect(Url.Action("CompetitionSFACountReport", "Reports", new
                {

                    exceptionType = this.GetType().Name
                }));
            }
        }
        #endregion CompetitionSFACount

        #region Message
        [HttpGet]
        public ActionResult MessageReport()
        {
            GetBranch InputParam = new GetBranch();
            MessageReportFilters view = new MessageReportFilters();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            if (rolen == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            if (Session["MessageReportNoData"] == null)
                Session["MessageReportNoData"] = 0;
            return View(view);
        }

        [HttpPost]
        public ActionResult MessageReport(MessageReportFilters objGridData)
        {
            var output = _APIWebReportsData.GetMessageReport(objGridData);

            //CODE TO CHECK File is Existing or not on Desired Directory
            for (int i = 0; i < output.Data.data.Count(); i++)
            {
                if (!System.IO.File.Exists(Server.MapPath("~/Uploads/Broadcast/" + output.Data.data[i].MessageFile)))
                {
                    output.Data.data[i].MessageFile = "";
                }
            }

            return Json(output.Data);
        }

        [HttpPost]
        public void DownloadMessageReport(MessageReportFilters objGridData)
        {
            objGridData.start = 0;
            objGridData.length = int.MaxValue;
            var output = _APIWebReportsData.GetMessageReport(objGridData);
            DataTable dt = new DataTable("MessageReport");

            if (output.MessageCode == (int)Acceptable.Found)
            {
                var listdata = output.Data.data.ToList();
                dt.Columns.AddRange(new DataColumn[22] {
                    new DataColumn("SendDate"),
                    new DataColumn("ReplyDate"),
                    new DataColumn("Branch"),
                    new DataColumn("Dealer"),
                    new DataColumn("MasterCode"),
                    new DataColumn("DealerCode"),
                    new DataColumn("State"),
                    new DataColumn("City"),
                    new DataColumn("Location"),
                    new DataColumn("Channel"),
                    new DataColumn("SFACode"),
                    new DataColumn("SFA"),
                    new DataColumn("SFALevel"),
                    new DataColumn("SFADivision"),
                    new DataColumn("IncentiveCategory"),
                    new DataColumn("Message"),
                    new DataColumn("MessageFile"),
                    new DataColumn("Reply"),
                    new DataColumn("ReplyFile"),
                    new DataColumn("ValidFrom"),
                    new DataColumn("ValidTo"),
                    new DataColumn("MessageType")
                });
                foreach (var record in listdata)
                {
                    dt.Rows.Add(record.SendDate, record.ReplyDate, record.Branch, record.Dealer, record.MasterCode, record.DealerCode,
                        record.State, record.City, record.Location, record.Channel, record.SFACode, record.SFA,
                        record.SFALevel, record.SFADivision, record.IncentiveCategory, record.Message, record.MessageFile,
                        record.Reply, record.ReplyFile, record.ValidFrom, record.ValidTo, record.MessageType);
                }

            }

            if (dt.Rows.Count > 0)
            {
                string attachment = "attachment; filename=MessageReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + Convert.ToString(dr[i]));
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
            else
            {
                Session["MessageReportNoData"] = 1;
                Response.Redirect(Url.Action("MessageReport", "Reports", new
                {

                    exceptionType = this.GetType().Name
                }));
            }
        }
        #endregion Message

        #region TargetVsAchievement
        [HttpGet]
        public ActionResult TargetVsAchievementReport()
        {
            GetBranch InputParam = new GetBranch();
            TargetVsAchievementReportFilters view = new TargetVsAchievementReportFilters();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            if (rolen == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            if (Session["TargetVsAchievementReportNoData"] == null)
                Session["TargetVsAchievementReportNoData"] = 0;

            return View(view);
        }

        [HttpPost]
        public ActionResult TargetVsAchievementReport(TargetVsAchievementReportFilters objGridData)
        {
            var output = _APIWebReportsData.GetTargetVsAchievementReport(objGridData);
            return Json(output.Data);
        }

        [HttpPost]
        public void DownloadTargetVsAchievementReport(TargetVsAchievementReportFilters objGridData)
        {
            objGridData.start = 0;
            objGridData.length = int.MaxValue;
            string selectedMonth = objGridData.Month; // This should be in 'mYYYY' or 'mmYYYY' format   (Shivani Mali)
            int monthPart = int.Parse(selectedMonth.Substring(0, selectedMonth.Length - 4));
            string yearPart = selectedMonth.Substring(selectedMonth.Length - 4);
            string formattedMonth = new DateTime(int.Parse(yearPart), monthPart, 1).ToString("MMM yyyy");
            var output = _APIWebReportsData.GetTargetVsAchievementReport(objGridData);
            DataTable dt = new DataTable("TargetVsAchievementReport");

            if (output.MessageCode == (int)Acceptable.Found)
            {
                var listdata = output.Data.data.ToList();
                dt.Columns.AddRange(new DataColumn[18] {
                    new DataColumn("Month"),
                    new DataColumn("BranchName"),
                    new DataColumn("SFAName"),
                    new DataColumn("SFACode"),
                    new DataColumn("SFALevel"),
                    new DataColumn("DealerName"),
                    new DataColumn("DealerCode"),
                    new DataColumn("Channel"),
                    new DataColumn("City"),
                    new DataColumn("Location"),
                    new DataColumn("ProductCategory"),
                    new DataColumn("TargetCategory"),
                    new DataColumn("IncentiveCategory"),
                    new DataColumn("Division"),
                    new DataColumn("TargetQty"),
                    new DataColumn("AchQty"),
                    new DataColumn("TargetValue"),
                    new DataColumn("AchValue")
                });
                foreach (var record in listdata)
                {
                    dt.Rows.Add(formattedMonth,record.BranchName, record.SFAName, record.SFACode, record.SFALevel, record.DealerName,
                        record.DealerCode, record.Channel, record.City, record.Location, record.ProductCategory,
                        record.TargetCategory, record.IncentiveCategory, record.Division, record.TargetQty,
                        record.AchQty, record.TargetValue, record.AchValue);
                }

            }

            if (dt.Rows.Count > 0)
            {
                string attachment = "attachment; filename=TargetVsAchievementReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + Convert.ToString(dr[i]));
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
            else
            {
                Session["TargetVsAchievementReportNoData"] = 1;
                Response.Redirect(Url.Action("TargetVsAchievementReport", "Reports", new
                {

                    exceptionType = this.GetType().Name
                }));
            }
        }
        #endregion TargetVsAchievement

        #region DailySalesWithAttribute
        [HttpGet]
        public ActionResult DailySalesWithAttributeIndex()
        {
            GetBranch InputParam = new GetBranch();
            DailySalesWithAttributesReport view = new DailySalesWithAttributesReport();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            if (rolen == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            if (Session["DailySalesWithAttributeIndexNoData"] == null)
                Session["DailySalesWithAttributeIndexNoData"] = 0;

            return View(view);
        }

        [HttpPost]
        public ActionResult DailySalesWithAttributeDisplayReport(DailySalesWithAttributesReport objGridData)
        {

            var output = _APIWebReportsData.GetDailySalesWithAttributeReport(objGridData);
            //if (output.MessageCode == (int)Acceptable.Found)
            return Json(output.Data, JsonRequestBehavior.AllowGet);
            //else
            //return Json(new Envelope<IEnumerable<DailySalesReportIMEIGrid>> { Data = null, Message = "No data found", MessageCode = 0 });

        }


        [HttpPost]
        public void DailySalesWithAttributeExcel(DailySalesWithAttributesReport objGridData)
        {
            try
            {
                objGridData.start = 0;
                objGridData.length = 10000000;
                objGridData.draw = 1;
                DataTable dt = new DataTable("DailySalesReportWithAttribute");


                var model = _APIWebReportsData.GetDailySalesWithAttributeReport(objGridData);
                if (model.MessageCode == (int)Acceptable.Found)
                {
                    var listdata = model.Data.data.ToList();
                    dt.Columns.AddRange(new DataColumn[32] { new DataColumn("Branch"),
                                            new DataColumn("Date"),
                                            new DataColumn("Month"),
                                            new DataColumn("Dealer Name"),
                                            new DataColumn("City"),
                                            new DataColumn("Location"),
                                            new DataColumn("Payer Name"),
                                            new DataColumn("Channel"),
                                            new DataColumn("Dealer State"),
                                            new DataColumn("Master Code"),
                                            new DataColumn("Dealer Code"),
                                            new DataColumn("Dealer Classification"),
                                            new DataColumn("SFA Code"),
                                            new DataColumn("SFA Name"),
                                            new DataColumn("SFA Level"),
                                            new DataColumn("SFA Type"),
                                            new DataColumn("Incentive Category"),
                                            new DataColumn("Company Name"),
                                            new DataColumn("Product Category"),
                                            //new DataColumn("Product Sub Category"),
                                            //new DataColumn("Product Description"),
                                            new DataColumn("Material"),
                                            new DataColumn("Division"),
                                            new DataColumn("MOP"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("Total MOP"),
                                            new DataColumn("Attribute 1"),
                                            new DataColumn("Attribute 2"),
                                            new DataColumn("Attribute 3"),
                                            new DataColumn("Attribute 4"),
                                            new DataColumn("Attribute 5"),
                                            new DataColumn("Attribute 6"),
                                            new DataColumn("QSRStatus"),
                                            new DataColumn("QSRQuantity"),});
                    //new DataColumn("Core Category"),});
                    foreach (var record in listdata)
                    {
                        dt.Rows.Add(record.Branch, record.Date, record.Month, record.DealerName, record.City, record.Location, record.PayerName, record.Channel, record.State,
                            record.SAPCode, record.DealerCode, record.DealerClassification, record.SFACode, record.SFAName, record.SFALevel, record.SFAType,
                            record.IncentiveCate, record.CompanyName, record.ProCat, /*record.ProSubCat, record.ProSubCatDescription,*/ record.SonyMaterial, record.Division,
                            record.MOP, record.Quantity, record.TotalMOP, record.SubCatGroup, record.Segment, record.Resolution, record.Internet,
                            record.S3D, record.TVType,record.QSRStatus,record.QSRQuantity);
                    }

                    var fileName = "DailySalesReportWithAttribute_Data.xls";
                    string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        string attachment = "attachment; filename=" + fileName;
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/vnd.ms-excel";
                        string tab = "";
                        foreach (DataColumn dc in dt.Columns)
                        {
                            Response.Write(tab + dc.ColumnName);
                            tab = "\t";
                        }
                        Response.Write("\n");
                        int i;
                        foreach (DataRow dr in dt.Rows)
                        {
                            tab = "";
                            for (i = 0; i < dt.Columns.Count; i++)
                            {
                                Response.Write(tab + dr[i].ToString());
                                tab = "\t";
                            }
                            Response.Write("\n");
                        }
                        //AddSuccessNotification(model.Message);

                        Response.End();

                        //return File(Response.OutputStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

                    }
                }
                else
                {
                    Session["DailySalesWithAttributeIndexNoData"] = 1;
                    Response.Redirect(Url.Action("DailySalesWithAttributeIndex", "Reports", new
                    {

                        exceptionType = this.GetType().Name
                    }));
                }
            }
            catch (Exception ex)
            {

            }


        }
        #endregion

        #region DailySalesIMEI
        [HttpGet]
        public ActionResult DailySalesIMEI()
        {
            GetBranch InputParam = new GetBranch();
            DailySalesReportIMEI view = new DailySalesReportIMEI();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            if (rolen == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            if (Session["DailySalesIMEINoData"] == null)
                Session["DailySalesIMEINoData"] = 0;

            return View(view);
        }

        [HttpPost]
        public ActionResult DailySalesIMEIReport(DailySalesReportIMEI objGridData)
        {
            var output = _APIWebReportsData.GetDailySalesIMEIReport(objGridData);
            //if (output.MessageCode == (int)Acceptable.Found)
            return Json(output.Data, JsonRequestBehavior.AllowGet);

            //else

            //return Json(new Envelope<IEnumerable<DailySalesReportIMEIGrid>> { Data = null, Message = "No data found", MessageCode = 0 });
        }

        [HttpPost]
        //[ActionName("DailySalesIMEI")]
        public void DailySalesIMEIExcel(DailySalesReportIMEI objGridData)
        {
            try
            {
                objGridData.start = 0;
                objGridData.length = 10000000;
                objGridData.draw = 1;
                DataTable dt = new DataTable("DailySalesIMEIReport");


                var model = _APIWebReportsData.GetDailySalesIMEIReport(objGridData);
                if (model.MessageCode == (int)Acceptable.Found)
                {
                    var listdata = model.Data.data.ToList();
                    dt.Columns.AddRange(new DataColumn[22] { new DataColumn("Branch"),
                                            new DataColumn("Date"),
                                            new DataColumn("Dealer Name"),
                                            new DataColumn("City"),
                                            new DataColumn("Location"),
                                            new DataColumn("Channel"),
                                            new DataColumn("Dealer State"),
                                            new DataColumn("Master Code"),
                                            new DataColumn("Dealer Code"),
                                            new DataColumn("Dealer Classification"),
                                            new DataColumn("SFA Code"),
                                            new DataColumn("SFA Name"),
                                            new DataColumn("SFA Level"),
                                            new DataColumn("Incentive Category"),
                                            new DataColumn("Product Category"),
                                            new DataColumn("Product Sub Category"),
                                            new DataColumn("Product Description"),
                                            new DataColumn("Material"),
                                            new DataColumn("Division"),
                                            new DataColumn("Quantity"),
                                            new DataColumn("IMEI No."),
                                            new DataColumn("Type"), });
                    //new DataColumn("Core Category"),});
                    foreach (var record in listdata)
                    {
                        dt.Rows.Add(record.Branch, record.Date, record.DealerName, record.City, record.Location, record.Channel, record.State,
                            record.SAPCode, record.DealerCode, record.DealerClassification, record.SFACode, record.SFAName, record.SFALevel,
                            record.IncentiveCate, record.ProCat, record.ProSubCat, record.ProSubCatDescription, record.SonyMaterial, record.Division,
                            record.Quantity, record.IMEI, record.Type);
                    }

                    var fileName = "DailySalesIMEIReport_Data.xls";
                    string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }


                    if (dt.Rows.Count > 0)
                    {
                        string attachment = "attachment; filename=" + fileName;
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/vnd.ms-excel";
                        string tab = "";
                        foreach (DataColumn dc in dt.Columns)
                        {
                            Response.Write(tab + dc.ColumnName);
                            tab = "\t";
                        }
                        Response.Write("\n");
                        int i;
                        foreach (DataRow dr in dt.Rows)
                        {
                            tab = "";
                            for (i = 0; i < dt.Columns.Count; i++)
                            {
                                Response.Write(tab + dr[i].ToString());
                                tab = "\t";
                            }
                            Response.Write("\n");
                        }
                        //AddSuccessNotification(model.Message);

                        Response.End();

                        //return File(Response.OutputStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

                    }
                }
                else
                {
                    Session["DailySalesIMEINoData"] = 1;
                    Response.Redirect(Url.Action("DailySalesIMEI", "Reports", new { exceptionType = this.GetType().Name }));
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        #region TodayTiming
        [HttpGet]
        public ActionResult TodayTimingReport(string ErrorMsg = null)
        {
            GetBranch InputParam = new GetBranch();
            DailyTimingReportInput view = new DailyTimingReportInput();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            if (rolen == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            if (ErrorMsg != null)
            {
                AddErrorNotification("No Data Found");

            }
            return View(view);
        }



        [HttpPost]
        public ActionResult TodayTimingReport(DailyTimingReportInput objGridData)
        {
            if (objGridData.StartDate == null || objGridData.StartDate == "")
                objGridData.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
            if (objGridData.EndDate == null || objGridData.EndDate == "")
                objGridData.EndDate = DateTime.Now.ToString("dd/MM/yyyy");
            var userDetails = (BaseSession)(Session["BaseSession"]);
            //if (userDetails.RoleName == "RDI")
            //    objGridData.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
            var output = _APIWebReportsData.GetDailyTimingReport(objGridData);
            //if (output.MessageCode == (int)Acceptable.Found)


            //foreach (var item in output.Data.data)
            //{
            //    if (item.Attendance == "A")
            //    {
            //        item.Attendance = "Forgot To Punch";
            //    }
            //}


            return Json(output.Data, JsonRequestBehavior.AllowGet);
            //else
            //    return Json(new GridOutput<IEnumerable<DailyTimingReport>> { data = null, recordsFiltered=0 });
        }
        [HttpPost]
        public void ExportDailyTimingReport(DailyTimingReportInput objGridData)
        {
            objGridData.start = 1;
            objGridData.length = 98989898;
            if (objGridData.StartDate == null || objGridData.StartDate == "")
                objGridData.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
            if (objGridData.EndDate == null || objGridData.EndDate == "")
                objGridData.EndDate = DateTime.Now.ToString("dd/MM/yyyy");
            var userDetails = (BaseSession)(Session["BaseSession"]);
            if (userDetails.RoleName == "RDI")
                objGridData.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
            else
                objGridData.BranchId = 0;
            var output = _APIWebReportsData.GetDailyTimingReport(objGridData);
            DataTable dt = new DataTable();
            string[] headers = new string[] { "Region", "BranchName", "MasterCode", "DealerCode", "DealerName", "DealerLocation", "DealerCity", "SFACode", "SFALevel", "SFACategory", "SFALocation", "SFAName", "IncentiveCategory", "StoreLocation", "Attendance", "ShiftName", "TimeIn", "TimeOut", "TotalWorkingHrs", "SalesStatus", "CoreSalesStatus", "CreationDate", "IMEI1" };
            foreach (var name in headers)
                dt.Columns.Add(name);
            foreach (var record in output.Data.data)
            {
                dt.Rows.Add(record.Region, record.BranchName, record.MasterCode, record.DealerCode, record.DealerName, record.DealerLocation, record.DealerCity, record.SFACode, record.SFALevel, record.SFACategory, record.SFALocation, record.SFAName, record.IncentiveCategory, record.StoreLocation, record.Attendance, record.ShiftName, record.TimeIn, record.TimeOut, record.TotalWorkingHrs, record.SalesStatus, record.CoreSalesStatus, record.CreationDate, record.IMEI1);
            }


            var fileName = "DailyTiming_Data.xls";
            string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
            //save the file to server temp folder
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            //fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
            dt.TableName = "Daily Timing Report";
            if (dt.Rows.Count > 0)
            {
                string attachment = "attachment; filename=" + fileName;
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                //AddSuccessNotification(model.Message);

                Response.End();

                //return File(Response.OutputStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

            }
            else
            {
                // Session["DailySalesWithAttributeIndexNoData"] = 1;
                Response.Redirect(Url.Action("TodayTimingReport", "Reports", new
                {
                    ErrorMsg = "No Data Found",
                    exceptionType = this.GetType().Name
                }));
            }
        }
        #endregion



        #region Stock
        [HttpGet]
        public ActionResult Stock()
        {
            GetBranch InputParam = new GetBranch();
            StockReport view = new StockReport();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            if (rolen == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            if (Session["StockNoData"] == null)
                Session["StockNoData"] = 0;

            return View(view);
        }

        [HttpPost]
        public ActionResult StockReport(StockReport objGridData)
        {
            var output = _APIWebReportsData.GetStockReport(objGridData);
            //if (output.MessageCode == (int)Acceptable.Found)
            return Json(output.Data, JsonRequestBehavior.AllowGet);

            //else

            //    return Json(new IEnumerable<StockReportGrid> { Data = null });
        }


        [HttpPost]
        public void StockExcel(StockReport objGridData)
        {
            try
            {
                objGridData.start = 0;
                objGridData.length = 10000000;
                objGridData.draw = 1;
                DataTable dt = new DataTable("StockReport");

                var model = _APIWebReportsData.GetStockReport(objGridData);
                if (model.MessageCode == (int)Acceptable.Found)
                {
                    var listdata = model.Data.data.ToList();

                    dt.Columns.AddRange(new DataColumn[21] { new DataColumn("Branch"),
                                            new DataColumn("Date"),
                                            new DataColumn("Dealer Name"),
                                            new DataColumn("City"),
                                            new DataColumn("Location"),
                                            new DataColumn("Channel"),
                                            new DataColumn("Dealer State"),
                                            new DataColumn("Master Code"),
                                            new DataColumn("Party Name"),
                                            new DataColumn("Dealer Code"),
                                            new DataColumn("Dealer Classification"),
                                            new DataColumn("SFA Code"),
                                            new DataColumn("SFA Name"),
                                            new DataColumn("SFA Level"),
                                            new DataColumn("SFA Type"),
                                            new DataColumn("Incentive Category"),
                                            new DataColumn("Product Category"),
                                            new DataColumn("Product Sub Category"),
                                            new DataColumn("Product Description"),
                                            new DataColumn("Material"),
                                            new DataColumn("Quantity"),});
                    //new DataColumn("Core Category"),});
                    foreach (var record in listdata)
                    {
                        dt.Rows.Add(record.Branch, record.Date, record.DealerName, record.City, record.Location, record.Channel, record.State,
                            record.SAPCode, record.PartyName, record.DealerCode, record.DealerClassification, record.SFACode, record.SFAName, record.SFALevel,
                            record.SFAType, record.IncentiveCate, record.ProCat, record.ProSubCat, record.ProSubCatDescription, record.SonyMaterial,
                            record.Quantity);
                    }

                    var fileName = "StockReport_Data.xls";
                    string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        string attachment = "attachment; filename=" + fileName;
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/vnd.ms-excel";
                        string tab = "";
                        foreach (DataColumn dc in dt.Columns)
                        {
                            Response.Write(tab + dc.ColumnName);
                            tab = "\t";
                        }
                        Response.Write("\n");
                        int i;
                        foreach (DataRow dr in dt.Rows)
                        {
                            tab = "";
                            for (i = 0; i < dt.Columns.Count; i++)
                            {
                                Response.Write(tab + dr[i].ToString());
                                tab = "\t";
                            }
                            Response.Write("\n");
                        }
                        //AddSuccessNotification(model.Message);

                        Response.End();

                    }
                }
                else
                {
                    Session["StockNoData"] = 1;
                    Response.Redirect(Url.Action("Stock", "Reports", new
                    {

                        exceptionType = this.GetType().Name
                    }));
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        #region DemoStock (Display)
        [HttpGet]
        public ActionResult Display(string ErrorMsg = null)
        {
            GetBranch InputParam = new GetBranch();
            DisplayReport view = new DisplayReport();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            if (rolen == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            //if (Session["DisplayNoData"] == null)
            //    Session["DisplayNoData"] = 0;

            if (ErrorMsg != null)
            {
                AddErrorNotification("No Data Found");

            }

            return View(view);
        }

        [HttpPost]
        public ActionResult DisplayReport(DisplayReport objGridData)
        {
            var output = _APIWebReportsData.GetDisplayReport(objGridData);
            //if (output.MessageCode == (int)Acceptable.Found)
            return Json(output.Data);

            //else

            //return Json(new Envelope<IEnumerable<StockReportGrid>> { Data = null, Message = "No data found", MessageCode = 0 });
        }

        //[HttpPost]
        //public void DisplayExcel(DisplayReport objGridData)
        //{
        //    try
        //    {
        //        objGridData.start = 0;
        //        objGridData.length = 10000000;
        //        objGridData.draw = 1;
        //        DataTable dt = new DataTable("DisplayReport");


        //        var model = _APIWebReportsData.GetDisplayReport(objGridData);
        //        if (model.MessageCode == (int)Acceptable.Found)
        //        {
        //            var listdata = model.Data.data.ToList();
        //            dt.Columns.AddRange(new DataColumn[31] { new DataColumn("Date"),
        //                                    new DataColumn("Branch"),
        //                                    new DataColumn("Dealer Name"),
        //                                    new DataColumn("City"),
        //                                    new DataColumn("Location"),
        //                                    new DataColumn("Channel"),
        //                                    new DataColumn("Dealer State"),
        //                                    new DataColumn("Master Code"),
        //                                    new DataColumn("Party Name"),
        //                                    new DataColumn("Dealer Code"),
        //                                    new DataColumn("Dealer Classification"),
        //                                    new DataColumn("Material Name"),
        //                                    new DataColumn("SFA Code"),
        //                                    new DataColumn("SFA Name"),
        //                                    new DataColumn("SFA Level"),
        //                                    new DataColumn("SFA Type"),
        //                                    new DataColumn("Incentive Category"),
        //                                    new DataColumn("Product Category"),
        //                                    //new DataColumn("product Vijay"),
        //                                    new DataColumn("Division"),
        //                                     new DataColumn("FY"),
        //                                    new DataColumn("Planned Quantity"),
        //                                    new DataColumn("Display Quantity"),
        //                                    new DataColumn("Gap"),
        //                                     new DataColumn("Attribute 1"),
        //                                    new DataColumn("Attribute 2"),
        //                                    new DataColumn("Attribute 3"),
        //                                    new DataColumn("Attribute 4"),
        //                                    new DataColumn("Attribute 5"),
        //                                    new DataColumn("Attribute 6"),
        //                                    new DataColumn("Brand"),
        //                                     new DataColumn("Quantity")

        //                    });
        //            //new DataColumn("Core Category"),});

        //            foreach (var record in listdata)
        //            {
        //                dt.Rows.Add(record.Date, record.Branch, record.DealerName, record.City, record.Location, record.Channel, record.State,
        //                    record.SAPCode, record.PartyName, record.DealerCode, record.DealerClassification, record.SonyMaterial,
        //                    record.SFACode, record.SFAName, record.SFALevel, record.SFAType, record.IncentiveCate, record.ProCat,/* record.ProSubCat,*/
        //                    record.Division, record.FY, record.PlannedQuantity, record.DisplayQuantity, record.Gap, record.SubCatGroup, record.Segment, record.Resolution, record.Internet,
        //                    record.S3D, record.TVType, record.Brand, record.Quantity);
        //                    //,record.Model,record.Brand,record.Quantity);
        //            }

        //            var fileName = "DisplayReport_Data.xls";
        //            string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
        //            if (System.IO.File.Exists(fullPath))
        //            {
        //                System.IO.File.Delete(fullPath);
        //            }

        //            if (dt.Rows.Count > 0)
        //            {
        //                string attachment = "attachment; filename=" + fileName;
        //                Response.ClearContent();
        //                Response.AddHeader("content-disposition", attachment);
        //                Response.ContentType = "application/vnd.ms-excel";
        //                string tab = "";
        //                foreach (DataColumn dc in dt.Columns)
        //                {
        //                    Response.Write(tab + dc.ColumnName);
        //                    tab = "\t";
        //                }
        //                Response.Write("\n");
        //                int i;
        //                foreach (DataRow dr in dt.Rows)
        //                {
        //                    tab = "";
        //                    for (i = 0; i < dt.Columns.Count; i++)
        //                    {
        //                        Response.Write(tab + dr[i].ToString());
        //                        tab = "\t";
        //                    }
        //                    Response.Write("\n");
        //                }
        //                //AddSuccessNotification(model.Message);

        //                Response.End();

        //                //return File(Response.OutputStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        //            }
        //        }
        //        else
        //        {
        //            //Session["DisplayNoData"] = 1;
        //            Response.Redirect(Url.Action("Display", "Reports", new
        //            {
        //                ErrorMsg = "No Data Found",
        //                exceptionType = this.GetType().Name
        //            }));
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }


        //}

        [HttpPost]
        public void DisplayExcel(DisplayReport objGridData)
        {
            try
            {
                objGridData.start = 0;
                objGridData.length = 10000000;
                objGridData.draw = 1;
                DataTable dt = new DataTable("DisplayReport");


                var model = _APIWebReportsData.GetDisplayReport(objGridData);
                if (model.MessageCode == (int)Acceptable.Found)
                {
                    var listdata = model.Data.data.ToList();
                    dt.Columns.AddRange(new DataColumn[31] { new DataColumn("Date"),
                                            new DataColumn("Branch"),
                                            new DataColumn("Dealer Name"),
                                            new DataColumn("City"),
                                            new DataColumn("Location"),
                                            new DataColumn("Channel"),
                                            new DataColumn("Dealer State"),
                                            new DataColumn("Master Code"),
                                            new DataColumn("Party Name"),
                                            new DataColumn("Dealer Code"),
                                            new DataColumn("Dealer Classification"),
                                            new DataColumn("Material Name"),
                                            new DataColumn("SFA Code"),
                                            new DataColumn("SFA Name"),
                                            new DataColumn("SFA Level"),
                                            new DataColumn("SFA Type"),
                                            new DataColumn("Incentive Category"),
                                            new DataColumn("Product Category"),
                                            //new DataColumn("product Vijay"),
                                            new DataColumn("Division"),
                                             new DataColumn("FY"),
                                            new DataColumn("Planned Quantity"),
                                            new DataColumn("Display Quantity"),
                                            new DataColumn("Gap"),
                                             new DataColumn("Attribute 1"),
                                            new DataColumn("Attribute 2"),
                                            new DataColumn("Attribute 3"),
                                            new DataColumn("Attribute 4"),
                                            new DataColumn("Attribute 5"),
                                            new DataColumn("Attribute 6"),
                                            //new DataColumn("Model"),
                                            //new DataColumn("Brand"),
                                            // new DataColumn("AmboQuantity")
                                              new DataColumn("CompanyName"),
                                            new DataColumn("QSRStatus"),

                            });
                    //new DataColumn("Core Category"),});

                    foreach (var record in listdata)
                    {
                        dt.Rows.Add(record.Date, record.Branch, record.DealerName, record.City, record.Location, record.Channel, record.State,
                            record.SAPCode, record.PartyName, record.DealerCode, record.DealerClassification, record.SonyMaterial,
                            record.SFACode, record.SFAName, record.SFALevel, record.SFAType,
                             record.IncentiveCate, record.ProCat, /* record.ProSubCat,*/
                            record.Division, record.FY, record.PlannedQuantity, record.DisplayQuantity, record.Gap, record.SubCatGroup, record.Segment, record.Resolution, record.Internet,
                            record.S3D, record.TVType, record.CompanyName,record.QSRStatus);
                        //,record.Model,record.Brand,record.Quantity);
                    }

                    var fileName = "DisplayReport_Data.xls";
                    string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        string attachment = "attachment; filename=" + fileName;
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/vnd.ms-excel";
                        string tab = "";
                        foreach (DataColumn dc in dt.Columns)
                        {
                            Response.Write(tab + dc.ColumnName);
                            tab = "\t";
                        }
                        Response.Write("\n");
                        int i;
                        foreach (DataRow dr in dt.Rows)
                        {
                            tab = "";
                            for (i = 0; i < dt.Columns.Count; i++)
                            {
                                Response.Write(tab + dr[i].ToString());
                                tab = "\t";
                            }
                            Response.Write("\n");
                        }
                        //AddSuccessNotification(model.Message);

                        Response.End();

                        //return File(Response.OutputStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

                    }
                }
                else
                {
                    //Session["DisplayNoData"] = 1;
                    Response.Redirect(Url.Action("Display", "Reports", new
                    {
                        ErrorMsg = "No Data Found",
                        exceptionType = this.GetType().Name
                    }));
                }
            }
            catch (Exception ex)
            {

            }


        }
        #endregion DemoStock (Display)

        #region MonthlyAttedanceReport
        [HttpGet]
        public ActionResult MonthlyAttendanceReport()
        {
            GetBranch InputParam = new GetBranch();
            MonthlyAttendanceReportInput view = new MonthlyAttendanceReportInput();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            if (rolen == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            return View(view);
        }



        [HttpPost]
        public PartialViewResult GetMonthlyAttendanceReport(MonthlyAttendanceReportInput objGridData)
        {
            if (objGridData.StartDate == null || objGridData.StartDate == "")
                objGridData.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
            if (objGridData.EndDate == null || objGridData.EndDate == "")
                objGridData.EndDate = DateTime.Now.ToString("dd/MM/yyyy");

            var output = _APIWebReportsData.GetMonthlyAttendanceReport(objGridData);


            //foreach (var item in output.Data.Data)
            //{
            //    if (item.Attendance == "A")
            //    {
            //        item.Attendance = "P";
            //    }
            //}

            if (output.MessageCode == (int)Acceptable.Found)
                return PartialView("_MonthlyAttendanceReport", output.Data);

            else

                return PartialView("_MonthlyAttendanceReport", new Envelope<DataTable> { Data = null, Message = "No data found", MessageCode = 0 });
        }

        [HttpPost]
        public ActionResult ExportExcel(MonthlyAttendanceReportInput objGridData)
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
            DataTable output = _APIWebReportsData.GetMonthlyAttendanceReport(objGridData).Data;
            var fileName = "MonthlyAttendance_Data.xls";
            string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
            //save the file to server temp folder
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            //fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
            output.TableName = "Monthly Attendance Report";
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
        #endregion

        #region IncentiveReports
        [HttpGet]
        public ActionResult BaseIncentiveReport()
        {
            var userDetails = (BaseSession)(Session["BaseSession"]);
            if (userDetails.RoleName == "RDI")
                ViewBag.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
            else
                ViewBag.BranchId = 0;
            return View();
        }
        [HttpPost]
        public ActionResult GetBasePerPieceIncentiveReport(BasePerPieceIncentiveReportInputParam objGridData)
        {
            var output = _APIIncentiveData.GetBasePerPieceIncentiveReport(objGridData);
            return Json(output.Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BasePerPieceIncentiveExport(string BranchId = "0", string SFAId = "0", string DivisionId = "0", string Month = "0", string ProductCategoryId = "0")
        {
            BasePerPieceIncentiveReportInputParam objGridData = new BasePerPieceIncentiveReportInputParam();
            objGridData.BranchId = String.IsNullOrEmpty(BranchId) ? 0 : Convert.ToInt32(BranchId);
            objGridData.SFAId = String.IsNullOrEmpty(SFAId) ? 0 : Convert.ToInt32(SFAId);
            objGridData.DivisionId = String.IsNullOrEmpty(DivisionId) ? 0 : Convert.ToInt32(DivisionId);
            objGridData.Month = Month;
            objGridData.ProductCategoryId = String.IsNullOrEmpty(ProductCategoryId) ? 0 : Convert.ToInt32(ProductCategoryId);

            BasePerPieceIncentiveDisplayReportList List = new BasePerPieceIncentiveDisplayReportList();
            List = _APIIncentiveData.GetBasePerPieceIncentiveReport(objGridData).Data;


            GridView gv = new GridView();
            gv.DataSource = List.BasePerPieceIncentiveData;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=BasePerPieceIncentiveReport.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.End();
            return View();
        }
        [HttpPost]
        public ActionResult GetFestivalIncentiveReport(FestivalIncentiveReportInputParam objGridData)
        {
            var output = _APIIncentiveData.GetFestivalIncentiveReport(objGridData);
            return Json(output.Data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FestivalIncentiveExport(string BranchId = "0", string SFAId = "0", string DivisionId = "0", string FestivalSchemeId = "0", string ProductCategoryId = "0")
        {
            FestivalIncentiveReportInputParam objGridData = new FestivalIncentiveReportInputParam();
            objGridData.BranchId = String.IsNullOrEmpty(BranchId) ? 0 : Convert.ToInt32(BranchId);
            objGridData.SFAId = String.IsNullOrEmpty(SFAId) ? 0 : Convert.ToInt32(SFAId);
            objGridData.DivisionId = String.IsNullOrEmpty(DivisionId) ? 0 : Convert.ToInt32(DivisionId);
            objGridData.FestivalSchemeId = Convert.ToInt32(FestivalSchemeId);
            objGridData.ProductCategoryId = String.IsNullOrEmpty(ProductCategoryId) ? 0 : Convert.ToInt32(ProductCategoryId);

            FestivalIncentiveDisplayReportList List = new FestivalIncentiveDisplayReportList();
            List = _APIIncentiveData.GetFestivalIncentiveReport(objGridData).Data;


            GridView gv = new GridView();
            gv.DataSource = List.FestivalIncentiveData;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=FestivalIncentiveReport.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.End();
            return View();
        }
        [HttpGet]
        public ActionResult FestivalIncentiveDetailReport()
        {
            var userDetails = (BaseSession)(Session["BaseSession"]);
            if (userDetails.RoleName == "RDI")
                ViewBag.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
            else
                ViewBag.BranchId = 0;
            return View();
        }
        [HttpPost]
        public ActionResult GetFestivalIncentiveDetailReport(FestivalIncentiveReportInputParam objGridData)
        {
            var output = _APIIncentiveData.GetFestivalIncentiveDetailReport(objGridData);

            var jsonResult = Json(output.Data, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            //return Json(output.Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FestivalIncentiveDetailExport(string BranchId = "0", string SFAId = "0", string DivisionId = "0", string FestivalSchemeId = "0", string ProductCategoryId = "0")
        {
            FestivalIncentiveReportInputParam objGridData = new FestivalIncentiveReportInputParam();
            objGridData.BranchId = String.IsNullOrEmpty(BranchId) ? 0 : Convert.ToInt32(BranchId);
            objGridData.SFAId = String.IsNullOrEmpty(SFAId) ? 0 : Convert.ToInt32(SFAId);
            objGridData.DivisionId = String.IsNullOrEmpty(DivisionId) ? 0 : Convert.ToInt32(DivisionId);
            objGridData.FestivalSchemeId = Convert.ToInt32(FestivalSchemeId);
            objGridData.ProductCategoryId = String.IsNullOrEmpty(ProductCategoryId) ? 0 : Convert.ToInt32(ProductCategoryId);

            FestivalIncentiveDetailReportList List = new FestivalIncentiveDetailReportList();
            List = _APIIncentiveData.GetFestivalIncentiveDetailReport(objGridData).Data;


            GridView gv = new GridView();
            gv.DataSource = List.FestivalIncentiveDetailData;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=FestivalIncentiveDetailReport.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.End();
            return View();
        }


        [HttpGet]
        public ActionResult BasePerPieceIncentiveDetailReport()
        {
            var userDetails = (BaseSession)(Session["BaseSession"]);
            if (userDetails.RoleName == "RDI")
                ViewBag.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
            else
                ViewBag.BranchId = 0;
            return View();
        }
        [HttpPost]
        public ActionResult GetBasePerPieceIncentiveDetailReport(BasePerPieceIncentiveReportInputParam objGridData)
        {
            var output = _APIIncentiveData.GetBasePerPieceIncentiveDetailReport(objGridData);

            var jsonResult = Json(output.Data, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

            //return Json(output.Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BasePerPieceIncentiveDetailExport(string BranchId = "0", string SFAId = "0", string DivisionId = "0", string Month = "0", string ProductCategoryId = "0")
        {
            BasePerPieceIncentiveReportInputParam objGridData = new BasePerPieceIncentiveReportInputParam();
            objGridData.BranchId = String.IsNullOrEmpty(BranchId) ? 0 : Convert.ToInt32(BranchId);
            objGridData.SFAId = String.IsNullOrEmpty(SFAId) ? 0 : Convert.ToInt32(SFAId);
            objGridData.DivisionId = String.IsNullOrEmpty(DivisionId) ? 0 : Convert.ToInt32(DivisionId);
            objGridData.Month = Month;
            objGridData.ProductCategoryId = String.IsNullOrEmpty(ProductCategoryId) ? 0 : Convert.ToInt32(ProductCategoryId);

            BasePerPieceIncentiveDetailReportList List = new BasePerPieceIncentiveDetailReportList();
            List = _APIIncentiveData.GetBasePerPieceIncentiveDetailReport(objGridData).Data;


            GridView gv = new GridView();
            gv.DataSource = List.BasePerPieceIncentiveDetailData;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=BasePerPieceIncentiveDetailReport.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.End();
            return View();
        }


        [HttpGet]
        public ActionResult PerPieceIncentiveReport()
        {
            var userDetails = (BaseSession)(Session["BaseSession"]);
            if (userDetails.RoleName == "RDI")
                ViewBag.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
            else
                ViewBag.BranchId = 0;
            return View();
        }
        [HttpGet]
        public ActionResult FestivalIncentiveReport()
        {
            var userDetails = (BaseSession)(Session["BaseSession"]);
            if (userDetails.RoleName == "RDI")
                ViewBag.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
            else
                ViewBag.BranchId = 0;
            return View();
        }
        #endregion

        #region Training
        [HttpGet]
        public ActionResult TrainingReportIndex()
        {
            GetBranch InputParam = new GetBranch();
            TrainingReport view = new TrainingReport();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            if (rolen == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            if (Session["TrainingNoData"] == null)
                Session["TrainingNoData"] = 0;

            return View(view);
        }

        [HttpPost]
        public ActionResult TrainingReport(TrainingReport objGridData)
        {
            var output = _APIWebReportsData.GetTrainingReport(objGridData);
            if (output.MessageCode == (int)Acceptable.Found)
                return PartialView("_TrainingReport", output.Data);
            else
                return PartialView("_TrainingReport", new Envelope<DataTable> { Data = null, Message = "No data found", MessageCode = 0 });
        }

        [HttpPost]
        public ActionResult TrainingReportExcel(TrainingReport objGridData)
        {

            DataTable output = _APIWebReportsData.GetTrainingReport(objGridData).Data;
            var x = output.Rows[0][0].ToString();
            var fileName = "TrainingReport.xls";
            string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
            //save the file to server temp folder
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            //fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
            output.TableName = "Training Report";
            if (output.Rows.Count > 0 && x != "No data found")
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
        #endregion Training


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
                var output = _APIWebReportsData.GetMonthlyAttendanceReport(objGridData);

                if (output != null || output.Data != null)
                {
                    UpdateMonthlyReport objUpdateMonthlyReport = new UpdateMonthlyReport();



                    objUpdateMonthlyReport.UserId = objUserSession.UserId;
                    objUpdateMonthlyReport.RoleName = objUserSession.RoleName;
                    objUpdateMonthlyReport.SFAId = objGridData.SFAId; //replace SFAId to system id nikita 29/12/2024
                    objUpdateMonthlyReport.Month = objGridData.Month;
                    objUpdateMonthlyReport.StartDate = objGridData.StartDate;
                    objUpdateMonthlyReport.EndDate = objGridData.EndDate;

                    List<ApprovalGrid> objlist = new List<ApprovalGrid>();

                    objlist = _APIWebReportsData.GETApprovalDateStatusWise(objUpdateMonthlyReport).Data;

                    if (objlist != null && objlist.Count > 0)
                    {
                        foreach (DataRow dr in output.Data.Rows)
                        {
                            if (objlist.Any(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()))
                            {
                                var present = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "P").Count();
                                var absent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "A").Count();
                                var leave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "L").Count();
                                var training = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "T/M").Count();
                                var weeklyoff = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "W").Count();

                                 var halfday = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "HD").Count();


                                var leavetoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "L").Count();
                                var presenttoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "P").Count();
                                //var trainingtoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "T/M").Count();
                                //var weeklyofftoabsent  = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "W").Count();
                                //var weeklyofftopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "W").Count();

                                var trainingtoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "T/M").Count();

                                var presenttotraining = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "T/M" && a.OldAttendanceType == "P").Count();

                                var weeklyofftoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "W").Count();
                                var weeklyofftopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "W").Count();
                                var presenttoweekly = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "W" && a.OldAttendanceType == "P").Count();
                                var presenttoleave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "L" && a.OldAttendanceType == "P").Count();
                                var weeklytoleave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "L" && a.OldAttendanceType == "W").Count();
                                var weeklytoTraining = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "T/M" && a.OldAttendanceType == "W").Count();
                                var leavetoweekly = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "W" && a.OldAttendanceType == "L").Count();
                                var leavetopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "L").Count();
                                //  Commented because of extra column division is added in to the report 
                                //present = (Convert.ToInt32(dr.ItemArray[12]) + present) - (presenttoweekly + presenttoleave);
                                ////absent = Convert.ToInt32(dr.ItemArray[15]) + absent;                            
                                //leave = (Convert.ToInt32(dr.ItemArray[14]) + leave) - (leavetoweekly + leavetopresent);
                                //training = Convert.ToInt32(dr.ItemArray[15]) + training;
                                //weeklyoff = (Convert.ToInt32(dr.ItemArray[13]) + weeklyoff) - (weeklyofftopresent + weeklytoleave);




                                present = (Convert.ToInt32(dr.ItemArray[15]) + present) - (presenttoweekly + presenttoleave + presenttotraining);
                                //absent = Convert.ToInt32(dr.ItemArray[15]) + absent;                            
                                leave = (Convert.ToInt32(dr.ItemArray[17]) + leave) - (leavetoweekly + leavetopresent);
                                training = Convert.ToInt32(dr.ItemArray[18]) + training;
                                weeklyoff = (Convert.ToInt32(dr.ItemArray[16]) + weeklyoff) -
                                     (weeklyofftopresent + weeklytoleave + weeklyofftoabsent + weeklytoTraining);


                                var absentupdate = Convert.ToInt32(output.Data.Columns.Count - 21) -
                                    (present + weeklyoff + training + leave -
                                    (leavetoabsent + presenttoabsent + trainingtoabsent));
                                dr.SetField("Absent", absentupdate.ToString());

                                present = present - presenttoabsent;
                                dr.SetField("Present", present.ToString());
                                leave = leave - leavetoabsent;
                                dr.SetField("Leave", leave.ToString());
                                training = training - trainingtoabsent;
                                dr.SetField("Training/Meeting", training.ToString());
                                //weeklyoff = weeklyoff - weeklyofftoabsent;
                                dr.SetField("WeeklyOff", weeklyoff.ToString());
                                // dr.SetField("HalfDay", halfday.ToString());

                                //var total = present + weeklyoff + training + leave + absentupdate;
                                //var total = Convert.ToInt32(output.Data.Columns.Count - 19);
                                //dr.SetField("Total", total.ToString());
                            }
                        }
                    }



                    objApprovalData.ApprovalGrid = objlist;
                    objApprovalData.Dt = output.Data;

                    //SendMail();
                }

                if (output.MessageCode == (int)Acceptable.Found)
                    return PartialView("_AttendanceApprovalReport", objApprovalData);


            }
            catch (Exception ex)
            {
                Msg = ex.Message.ToString();
            }
            return PartialView("_AttendanceApprovalReport", objApprovalData);

        }
        [HttpPost]
        public ActionResult GetAttendanceApprovalReportDownLoad(MonthlyAttendanceReportInput objGridData)
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
                var output = _APIWebReportsData.GetMonthlyAttendanceReportDownload(objGridData);

                if (output != null || output.Data != null)
                {
                    UpdateMonthlyReport objUpdateMonthlyReport = new UpdateMonthlyReport();



                    objUpdateMonthlyReport.UserId = objUserSession.UserId;
                    objUpdateMonthlyReport.RoleName = objUserSession.RoleName;
                    objUpdateMonthlyReport.SFAId = objGridData.SFAId;
                    objUpdateMonthlyReport.Month = objGridData.Month;
                    objUpdateMonthlyReport.StartDate = objGridData.StartDate;
                    objUpdateMonthlyReport.EndDate = objGridData.EndDate;

                    List<ApprovalGrid> objlist = new List<ApprovalGrid>();

                    objlist = _APIWebReportsData.GETApprovalDateStatusWise(objUpdateMonthlyReport).Data;

                    if (objlist != null && objlist.Count > 0)
                    {
                        foreach (DataRow dr in output.Data.Rows)
                        {
                            if (objlist.Any(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() &&  x.DealerCode.ToString() == dr.ItemArray[3].ToString()))
                            {
                                //var present = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "P").Count();
                                //var absent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A").Count();
                                //var leave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "L").Count();
                                //var training = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "T/M").Count();
                                //var weeklyoff = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "W").Count();
                                //var leavetoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "L").Count();
                                //var presenttoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "P").Count();
                                //var trainingtoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "T/M").Count();

                                //var presenttotraining = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "T/M" && a.OldAttendanceType == "P").Count();

                                //var weeklyofftoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "W").Count();
                                //var weeklyofftopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "W").Count();
                                //var presenttoweekly = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "W" && a.OldAttendanceType == "P").Count();
                                //var presenttoleave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "L" && a.OldAttendanceType == "P").Count();
                                //var weeklytoleave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "L" && a.OldAttendanceType == "W").Count();
                                //var leavetoweekly = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "W" && a.OldAttendanceType == "L").Count();
                                //var leavetopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "L").Count();
                                //var weeklytoTraining = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "T/M" && a.OldAttendanceType == "W").Count();
                                ////  Commented because of extra column division is added in to the report 
                                ////present = (Convert.ToInt32(dr.ItemArray[12]) + present) - (presenttoweekly + presenttoleave);
                                //////absent = Convert.ToInt32(dr.ItemArray[15]) + absent;                            
                                ////leave = (Convert.ToInt32(dr.ItemArray[14]) + leave) - (leavetoweekly + leavetopresent);
                                ////training = Convert.ToInt32(dr.ItemArray[15]) + training;
                                ////weeklyoff = (Convert.ToInt32(dr.ItemArray[13]) + weeklyoff) - (weeklyofftopresent + weeklytoleave);




                                //present = (Convert.ToInt32(dr.ItemArray[14]) + present) - (presenttoweekly + presenttoleave + presenttotraining);
                                ////absent = Convert.ToInt32(dr.ItemArray[15]) + absent;                            
                                //leave = (Convert.ToInt32(dr.ItemArray[16]) + leave) - (leavetoweekly + leavetopresent);
                                //training = Convert.ToInt32(dr.ItemArray[17]) + training;
                                //weeklyoff = (Convert.ToInt32(dr.ItemArray[15]) + weeklyoff) -
                                //    (weeklyofftopresent + weeklytoleave + weeklyofftoabsent + weeklytoTraining);


                                //var absentupdate = Convert.ToInt32(output.Data.Columns.Count - 21) -
                                //    (present + weeklyoff + training + leave -
                                //    (leavetoabsent + presenttoabsent + trainingtoabsent));
                                //dr.SetField("Absent", absentupdate.ToString());

                                //present = present - presenttoabsent;
                                //dr.SetField("Present", present.ToString());
                                //leave = leave - leavetoabsent;
                                //dr.SetField("Leave", leave.ToString());
                                //training = training - trainingtoabsent;
                                //dr.SetField("Training/Meeting", training.ToString());
                                ////weeklyoff = weeklyoff - weeklyofftoabsent;
                                //dr.SetField("WeeklyOff", weeklyoff.ToString());
                                ////var total = present + weeklyoff + training + leave + absentupdate;
                                ////var total = Convert.ToInt32(output.Data.Columns.Count - 19);
                                ////dr.SetField("Total", total.ToString());


                                ///nikita 30/12/2024
                                ///
                                var present = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "P").Count();
                                var absent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "A").Count();
                                var leave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "L").Count();
                                var training = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "T/M").Count();
                                var weeklyoff = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "W").Count();

                                var halfday = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "HD").Count();


                                var leavetoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "L").Count();
                                var presenttoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "P").Count();
                                //var trainingtoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "T/M").Count();
                                //var weeklyofftoabsent  = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "W").Count();
                                //var weeklyofftopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "W").Count();

                                var trainingtoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "T/M").Count();

                                var presenttotraining = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "T/M" && a.OldAttendanceType == "P").Count();

                                var weeklyofftoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "W").Count();
                                var weeklyofftopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "W").Count();
                                var presenttoweekly = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "W" && a.OldAttendanceType == "P").Count();
                                var presenttoleave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "L" && a.OldAttendanceType == "P").Count();
                                var weeklytoleave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "L" && a.OldAttendanceType == "W").Count();
                                var weeklytoTraining = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "T/M" && a.OldAttendanceType == "W").Count();
                                var leavetoweekly = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "W" && a.OldAttendanceType == "L").Count();
                                var leavetopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[21].ToString() && x.DealerCode.ToString() == dr.ItemArray[3].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "L").Count();
                                //  Commented because of extra column division is added in to the report 
                                //present = (Convert.ToInt32(dr.ItemArray[12]) + present) - (presenttoweekly + presenttoleave);
                                ////absent = Convert.ToInt32(dr.ItemArray[15]) + absent;                            
                                //leave = (Convert.ToInt32(dr.ItemArray[14]) + leave) - (leavetoweekly + leavetopresent);
                                //training = Convert.ToInt32(dr.ItemArray[15]) + training;
                                //weeklyoff = (Convert.ToInt32(dr.ItemArray[13]) + weeklyoff) - (weeklyofftopresent + weeklytoleave);




                                present = (Convert.ToInt32(dr.ItemArray[15]) + present) - (presenttoweekly + presenttoleave + presenttotraining);
                                //absent = Convert.ToInt32(dr.ItemArray[15]) + absent;                            
                                leave = (Convert.ToInt32(dr.ItemArray[17]) + leave) - (leavetoweekly + leavetopresent);
                                training = Convert.ToInt32(dr.ItemArray[18]) + training;
                                weeklyoff = (Convert.ToInt32(dr.ItemArray[16]) + weeklyoff) -
                                     (weeklyofftopresent + weeklytoleave + weeklyofftoabsent + weeklytoTraining);


                                var absentupdate = Convert.ToInt32(output.Data.Columns.Count - 21) -
                                    (present + weeklyoff + training + leave -
                                    (leavetoabsent + presenttoabsent + trainingtoabsent));
                                dr.SetField("Absent", absentupdate.ToString());

                                present = present - presenttoabsent;
                                dr.SetField("Present", present.ToString());
                                leave = leave - leavetoabsent;
                                dr.SetField("Leave", leave.ToString());
                                training = training - trainingtoabsent;
                                dr.SetField("Training/Meeting", training.ToString());
                                //weeklyoff = weeklyoff - weeklyofftoabsent;
                                dr.SetField("WeeklyOff", weeklyoff.ToString());
                                // dr.SetField("HalfDay", halfday.ToString());

                            }
                        }
                    }



                    objApprovalData.ApprovalGrid = objlist;
                    objApprovalData.Dt = output.Data;
                    //ListToDataTable list = new ListToDataTable();
                    //objApprovalData.Dt = list.ToDataTable(objApprovalData.ApprovalGrid);
                    //ConvertListToDataTable(objApprovalData.ApprovalGrid);
                    // GenericToDataTable(objApprovalData.ApprovalGrid);
                    var fileName = "AttendanceApproval_Data.xls";
                    string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                    //save the file to server temp folder
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    //fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                    objApprovalData.Dt.TableName = "Attendance Approval Report";

                    // var ds = GetDataSetFromDB();
                    // var excelApp = OfficeOpenXML.GetInstance();
                    // var file = excelApp.GetExcelStream(ds, false);
                    //   return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Guid.NewGuid().ToString() + ".xlsx");

                    if (objApprovalData.Dt.Rows.Count > 0)
                    {

                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(objApprovalData.Dt, objApprovalData.Dt.TableName);
                            using (MemoryStream stream = new MemoryStream())
                            {
                                wb.SaveAs(stream);
                                stream.Position = 0;
                                FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                                stream.WriteTo(file);
                                file.Close();

                                //         //Build the File Path.
                                //         // string path = Server.MapPath("~/Files/") + fileName;
                                //         // return File(fullPath, "application/vnd.ms-excel");
                                //         // return File(path, "application/pdf");

                                //      //   ////Read the File data into Byte Array.
                                //         byte[] bytes = System.IO.File.ReadAllBytes(fullPath);

                                //         ////Send the File to Download.
                                //         ////  return File(bytes, "application/octet-stream", fileName);
                                //         //return File(bytes, "application/vnd.ms-excel", fileName);

                                //         // MyMemoryStream.WriteTo(Response.OutputStream);
                                //         //Response.Flush();
                                //         //Response.End();
                            }

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
            // return File("","","");
            // return PartialView("_AttendanceApprovalReport", objApprovalData);

        }
        //public DataTable GenericToDataTable(IList<T> list)
        //{
        //    var json = JsonConvert.SerializeObject(list);
        //    DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
        //    return dt;
        //}

        public void SendMail(string branch)
        {
            DataSet ds = new DataSet();
            // ds = objBLL.GetDealerApprovalMailData(Session["UserName"].ToString(), ddlBranch.SelectedValue);
            MailMessage mailMessage = new MailMessage();
            string MailCC1 = "", MailCC2 = "", SMTPSrver = "", FromEmailid = "";
            string Toemail = "";
            string CCMailId = ""; string BCCMailId = "";
            string userName = "", subject = "";
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    Toemail = ds.Tables[0].Rows[0]["TomailID"].ToString();
            //    CCMailId = ds.Tables[0].Rows[0]["CCmailID"].ToString();
            //    BCCMailId = "Sidconswebdev@ap.sony.com"; //ds.Tables[0].Rows[0]["BCCmailID"].ToString();
            //}
            Toemail = "Sidconswebdev@ap.sony.com";
            CCMailId = "Sidconswebdev@ap.sony.com";
            SMTPSrver = ConfigurationManager.AppSettings.Get("MailHost").ToString();
            FromEmailid = ConfigurationManager.AppSettings.Get("FromEmailid").ToString();
            MailCC1 = CCMailId;
            MailCC2 = BCCMailId;
            //Attachment attachment = new Attachment(message.Attachments());

            string HostAdd = SMTPSrver;
            subject = "Ambo Changes ";

            string Pass = ConfigurationManager.AppSettings["MailPassword"].ToString();
            string status = "";

            //creating the object of MailMessage 
            string table = "";// "</br>DealerDetails </br> <table border=1 width='550'>" +
            //    "<tr><td> Financier Retailer Code / POS ID</td><td> RetailerName </td><td> Branch </td><td> State </td>" +
            //    "<td> City </td> <td> PinCode </td><td>StartDate </td><td>Status</td></tr> " +
            //    "<tr><td>" + txtDelearCode.Text + "</td><td>" + txtRetailerName.Text + "</td><td>" + ddlBranch.SelectedItem + "</td>" +
            //    "<td>" + ddlState.SelectedItem + "</td> <td>" + txtCity.Text + "</td><td>" + txtPincode.Text + "</td><td>" + DateTime.Now.ToString("dd/MM/yyyy") + "</td><td>" + status + "</td></tr> </table>";

            string Msg = "<html>" + "<body>Hi Sir / Mam,<br/><br/>" +
                                       " <H3>Below request is pending for your approval</H3> <br/><br/> " +
                                       "These are the below information<br/> " +
                                       "<br/><br/>Best Regards,<br/>Team Sony</body> " +
                                        "</html>";

            //MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(FromEmailid); //From Email Id  
            mailMessage.Subject = subject; //Subject of Email  
                                           // mailMessage.Body = mailMessage.Body; ; //body or message of Email  
            mailMessage.IsBodyHtml = true;
            //mailMessage.Attachments.Add(attachment);
            if (Toemail != "")
            {
                mailMessage.To.Add(Toemail);
                mailMessage.Body = Msg;
                if (CCMailId != "")
                {
                    mailMessage.CC.Add(CCMailId);
                }
                if (BCCMailId != "")
                {
                    mailMessage.Bcc.Add(BCCMailId);
                }


                SmtpClient smtp = new SmtpClient();  // creating object of smptpclient  

                smtp.Host = HostAdd;                 //host of emailaddress for example smtp.gmail.com etc  

                //network and security related credentials  
                smtp.EnableSsl = false;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = mailMessage.From.Address;
                NetworkCred.Password = Pass;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 25;


                smtp.Send(mailMessage);
            }
            mailMessage.Dispose();


        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdateAttendance(UpdateMonthlyReport objFormData)
        {
            string Message = "";
            Envelope<bool> output = new Envelope<bool>();
            try
            {
                //  objFormData.Remarks = null;
                if (objFormData.Remarks == null)
                {
                    output.Message = "Please Enter Remarks";
                    AddErrorNotification(output.Message);
                    return Json(output);
                }
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;

                output = _APIWebReportsData.UpdateAttendance(objFormData);
                //SendMail(objFormData.BranchId.ToString());
                //if(output.MessageCode == (int)Acceptable.Accepted)
                //{
                //    AddSuccessNotification(output.Message);
                //}
                return Json(output);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Json(new Envelope<bool> { Data = false, Message = Message, MessageCode = 0 });
            }


        }
        //public JsonResult UpdateApproveAttendance(UpdateMonthlyReport objFormData)
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

        //        output = _APIWebReportsData.UpdateApproveAttendance(objFormData);
        //        //SendMail(objFormData.BranchId.ToString());
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
                //objGridData.BranchId=
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
            //y6 sb.Remove()
            // sb.Remove(0,1000);
            // StringBuilder builder = new StringBuilder();
            //string str = sb.ToString();
            // sb.Replace(")", "");
            // sb.Replace("'", "");
            // sb.Replace("///", "");
            ////sb.Replace(",", "");
            // sb.Replace("/", "");
            // sb.Replace("javascript:updatePresent(", "");

            GridHtml = string.Empty;
            GridHtml = sb.ToString();
            // sb.Remove(1, GridHtml.IndexOf("\n") + 1);
            // GridHtml = sb.ToString();

            // GridHtml.Remove(0, GridHtml.IndexOf("\n") + 1);
            //GridHtml.

            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "AttendanceApprovalReport.xls");
        }
        #endregion

        //#region AttendanceApprovalReport

        //[HttpGet]
        //public ActionResult AttendanceApprovalReport()
        //{
        //    GetBranch InputParam = new GetBranch();


        //    MonthlyAttendanceReportInput view = new MonthlyAttendanceReportInput();
        //    InputParam.UserId = objUserSession.UserId;
        //    string rolen = objUserSession.RoleName;
        //    Session["Role"] = rolen.ToUpper();
        //    if (rolen == "RDI" || rolen == "Sony Admin")
        //    {
        //        var details = _APICommon.GetBranchByUserId(InputParam);
        //        view.BranchId = InputParam.BranchId = details.BranchId;

        //        //Commented this line of code because it is fetching the branch of user not its associated mapped branch...
        //        //var details = _APICommon.GetBranchByUserId(InputParam);
        //        //  view.BranchId = InputParam.BranchId = details.BranchId;
        //    }
        //    else if (rolen == "Branch HR")
        //    {
        //        // Commented this line of code because it is fetching the branch of user not its associated mapped branch...
        //        var details = _APICommon.GetBranchMappedForHR(InputParam);
        //        if (details != null && details.BranchIds != null)
        //        {
        //            view.BranchIds = details.BranchIds.ToArray();
        //        }
        //    }
        //    else
        //        view.BranchId = 0;

        //    return View(view);
        //}

        //[HttpPost]
        //public PartialViewResult GetAttendanceApprovalReport(MonthlyAttendanceReportInput objGridData)
        //{

        //    string Msg = "";
        //    ApprovalData objApprovalData = new ApprovalData();
        //    try
        //    {
        //        if (objGridData.StartDate == null || objGridData.StartDate == "")
        //            objGridData.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
        //        if (objGridData.EndDate == null || objGridData.EndDate == "")
        //            objGridData.EndDate = DateTime.Now.ToString("dd/MM/yyyy");

        //        objGridData.UserId = objUserSession.UserId;
        //        var output = _APIWebReportsData.GetMonthlyAttendanceReport(objGridData);

        //        if (output != null || output.Data != null)
        //        {
        //            UpdateMonthlyReport objUpdateMonthlyReport = new UpdateMonthlyReport();



        //            objUpdateMonthlyReport.UserId = objUserSession.UserId;
        //            objUpdateMonthlyReport.RoleName = objUserSession.RoleName;
        //            objUpdateMonthlyReport.SFAId = objGridData.SFAId;
        //            objUpdateMonthlyReport.Month = objGridData.Month;
        //            objUpdateMonthlyReport.StartDate = objGridData.StartDate;
        //            objUpdateMonthlyReport.EndDate = objGridData.EndDate;

        //            List<ApprovalGrid> objlist = new List<ApprovalGrid>();

        //            objlist = _APIWebReportsData.GETApprovalDateStatusWise(objUpdateMonthlyReport).Data;

        //            if (objlist != null && objlist.Count > 0)
        //            {
        //                foreach (DataRow dr in output.Data.Rows)
        //                {
        //                    if (objlist.Any(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()))
        //                    {
        //                        var present = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "P").Count();
        //                        var absent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A").Count();
        //                        var leave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "L").Count();
        //                        var training = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "T/M").Count();
        //                        var weeklyoff = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "W").Count();

        //                        var halfday = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "HD").Count();


        //                        var leavetoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "L").Count();
        //                        var presenttoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "P").Count();
        //                        //var trainingtoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "T/M").Count();
        //                        //var weeklyofftoabsent  = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "W").Count();
        //                        //var weeklyofftopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "W").Count();

        //                        var trainingtoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "T/M").Count();

        //                        var presenttotraining = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "T/M" && a.OldAttendanceType == "P").Count();

        //                        var weeklyofftoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "W").Count();
        //                        var weeklyofftopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "W").Count();
        //                        var weeklyofftohalfday = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "HD" && a.OldAttendanceType == "W").Count();

        //                        var presenttoweekly = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "W" && a.OldAttendanceType == "P").Count();
        //                        var presenttoleave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "L" && a.OldAttendanceType == "P").Count();
        //                        var presenttohalfday = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "HD" && a.OldAttendanceType == "P").Count();

        //                        var weeklytoleave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "L" && a.OldAttendanceType == "W").Count();
        //                        var weeklytoTraining = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "T/M" && a.OldAttendanceType == "W").Count();
        //                        var leavetoweekly = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "W" && a.OldAttendanceType == "L").Count();
        //                        var leavetopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "L").Count();
        //                        var leavetohalfday = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "HD" && a.OldAttendanceType == "L").Count();
        //                        //  Commented because of extra column division is added in to the report 
        //                        //present = (Convert.ToInt32(dr.ItemArray[12]) + present) - (presenttoweekly + presenttoleave);
        //                        ////absent = Convert.ToInt32(dr.ItemArray[15]) + absent;                            
        //                        //leave = (Convert.ToInt32(dr.ItemArray[14]) + leave) - (leavetoweekly + leavetopresent);
        //                        //training = Convert.ToInt32(dr.ItemArray[15]) + training;
        //                        //weeklyoff = (Convert.ToInt32(dr.ItemArray[13]) + weeklyoff) - (weeklyofftopresent + weeklytoleave);

        //                        var halfdaytoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "HD").Count();
        //                        var halfdaytotraining = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "T/M" && a.OldAttendanceType == "HD").Count();
        //                        var halfdaytoweekly = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "W" && a.OldAttendanceType == "HD").Count();
        //                        var halfdaytoleave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "L" && a.OldAttendanceType == "HD").Count();
        //                        var halfdaytopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "HD").Count();


        //                        present = (Convert.ToInt32(dr.ItemArray[14]) + present) - (presenttoweekly + presenttoleave + presenttotraining);
        //                        //absent = Convert.ToInt32(dr.ItemArray[15]) + absent;                            
        //                        leave = (Convert.ToInt32(dr.ItemArray[16]) + leave) - (leavetoweekly + leavetopresent);
        //                        training = Convert.ToInt32(dr.ItemArray[17]) + training;
        //                        weeklyoff = (Convert.ToInt32(dr.ItemArray[15]) + weeklyoff) -
        //                             (weeklyofftopresent + weeklytoleave + weeklyofftoabsent + weeklytoTraining);

        //                        halfday = (Convert.ToInt32(dr.ItemArray[23]) + halfday) - (halfdaytopresent + halfdaytoabsent + halfdaytoleave + halfdaytoweekly);

        //                        var absentupdate = Convert.ToInt32(output.Data.Columns.Count - 21) -
        //                            (present + weeklyoff + training + leave -
        //                            (leavetoabsent + presenttoabsent + trainingtoabsent));
        //                        dr.SetField("Absent", absentupdate.ToString());

        //                        present = present - presenttoabsent;
        //                        dr.SetField("Present", present.ToString());
        //                        leave = leave - leavetoabsent;
        //                        dr.SetField("Leave", leave.ToString());
        //                        training = training - trainingtoabsent;
        //                        dr.SetField("Training/Meeting", training.ToString());
        //                        //weeklyoff = weeklyoff - weeklyofftoabsent;
        //                        dr.SetField("WeeklyOff", weeklyoff.ToString());

        //                        halfday = halfday - halfdaytopresent;
        //                        dr.SetField("halfday", halfday.ToString());

        //                        //var total = present + weeklyoff + training + leave + absentupdate;
        //                        //var total = Convert.ToInt32(output.Data.Columns.Count - 19);
        //                        //dr.SetField("Total", total.ToString());
        //                    }
        //                }
        //            }



        //            objApprovalData.ApprovalGrid = objlist;
        //            objApprovalData.Dt = output.Data;

        //            //SendMail();
        //        }

        //        if (output.MessageCode == (int)Acceptable.Found)
        //            return PartialView("_AttendanceApprovalReport", objApprovalData);


        //    }
        //    catch (Exception ex)
        //    {
        //        Msg = ex.Message.ToString();
        //    }
        //    return PartialView("_AttendanceApprovalReport", objApprovalData);

        //}
        //[HttpPost]
        //public ActionResult GetAttendanceApprovalReportDownLoad(MonthlyAttendanceReportInput objGridData)
        //{

        //    string Msg = "";
        //    ApprovalData objApprovalData = new ApprovalData();
        //    try
        //    {
        //        if (objGridData.StartDate == null || objGridData.StartDate == "")
        //            objGridData.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
        //        if (objGridData.EndDate == null || objGridData.EndDate == "")
        //            objGridData.EndDate = DateTime.Now.ToString("dd/MM/yyyy");

        //        objGridData.UserId = objUserSession.UserId;
        //        var output = _APIWebReportsData.GetMonthlyAttendanceReportDownload(objGridData);

        //        if (output != null || output.Data != null)
        //        {
        //            UpdateMonthlyReport objUpdateMonthlyReport = new UpdateMonthlyReport();



        //            objUpdateMonthlyReport.UserId = objUserSession.UserId;
        //            objUpdateMonthlyReport.RoleName = objUserSession.RoleName;
        //            objUpdateMonthlyReport.SFAId = objGridData.SFAId;
        //            objUpdateMonthlyReport.Month = objGridData.Month;
        //            objUpdateMonthlyReport.StartDate = objGridData.StartDate;
        //            objUpdateMonthlyReport.EndDate = objGridData.EndDate;

        //            List<ApprovalGrid> objlist = new List<ApprovalGrid>();

        //            objlist = _APIWebReportsData.GETApprovalDateStatusWise(objUpdateMonthlyReport).Data;

        //            if (objlist != null && objlist.Count > 0)
        //            {
        //                foreach (DataRow dr in output.Data.Rows)
        //                {
        //                    if (objlist.Any(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()))
        //                    {
        //                        var present = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "P").Count();
        //                        var absent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A").Count();
        //                        var leave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "L").Count();
        //                        var training = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "T/M").Count();
        //                        var weeklyoff = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "W").Count();
        //                        var leavetoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "L").Count();
        //                        var presenttoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "P").Count();
        //                        var trainingtoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "T/M").Count();

        //                        var presenttotraining = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "T/M" && a.OldAttendanceType == "P").Count();

        //                        var weeklyofftoabsent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "A" && a.OldAttendanceType == "W").Count();
        //                        var weeklyofftopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "W").Count();
        //                        var presenttoweekly = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "W" && a.OldAttendanceType == "P").Count();
        //                        var presenttoleave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "L" && a.OldAttendanceType == "P").Count();
        //                        var weeklytoleave = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "L" && a.OldAttendanceType == "W").Count();
        //                        var leavetoweekly = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "W" && a.OldAttendanceType == "L").Count();
        //                        var leavetopresent = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "P" && a.OldAttendanceType == "L").Count();
        //                        var weeklytoTraining = objlist.Where(x => x.SFAIds.ToString() == dr.ItemArray[20].ToString()).Where(a => a.NewAttendanceType == "T/M" && a.OldAttendanceType == "W").Count();
        //                        //  Commented because of extra column division is added in to the report 
        //                        //present = (Convert.ToInt32(dr.ItemArray[12]) + present) - (presenttoweekly + presenttoleave);
        //                        ////absent = Convert.ToInt32(dr.ItemArray[15]) + absent;                            
        //                        //leave = (Convert.ToInt32(dr.ItemArray[14]) + leave) - (leavetoweekly + leavetopresent);
        //                        //training = Convert.ToInt32(dr.ItemArray[15]) + training;
        //                        //weeklyoff = (Convert.ToInt32(dr.ItemArray[13]) + weeklyoff) - (weeklyofftopresent + weeklytoleave);




        //                        present = (Convert.ToInt32(dr.ItemArray[14]) + present) - (presenttoweekly + presenttoleave + presenttotraining);
        //                        //absent = Convert.ToInt32(dr.ItemArray[15]) + absent;                            
        //                        leave = (Convert.ToInt32(dr.ItemArray[16]) + leave) - (leavetoweekly + leavetopresent);
        //                        training = Convert.ToInt32(dr.ItemArray[17]) + training;
        //                        weeklyoff = (Convert.ToInt32(dr.ItemArray[15]) + weeklyoff) -
        //                            (weeklyofftopresent + weeklytoleave + weeklyofftoabsent + weeklytoTraining);


        //                        var absentupdate = Convert.ToInt32(output.Data.Columns.Count - 21) -
        //                            (present + weeklyoff + training + leave -
        //                            (leavetoabsent + presenttoabsent + trainingtoabsent));
        //                        dr.SetField("Absent", absentupdate.ToString());

        //                        present = present - presenttoabsent;
        //                        dr.SetField("Present", present.ToString());
        //                        leave = leave - leavetoabsent;
        //                        dr.SetField("Leave", leave.ToString());
        //                        training = training - trainingtoabsent;
        //                        dr.SetField("Training/Meeting", training.ToString());
        //                        //weeklyoff = weeklyoff - weeklyofftoabsent;
        //                        dr.SetField("WeeklyOff", weeklyoff.ToString());
        //                        //var total = present + weeklyoff + training + leave + absentupdate;
        //                        //var total = Convert.ToInt32(output.Data.Columns.Count - 19);
        //                        //dr.SetField("Total", total.ToString());
        //                    }
        //                }
        //            }



        //            objApprovalData.ApprovalGrid = objlist;
        //            objApprovalData.Dt = output.Data;
        //            //ListToDataTable list = new ListToDataTable();
        //            //objApprovalData.Dt = list.ToDataTable(objApprovalData.ApprovalGrid);
        //            //ConvertListToDataTable(objApprovalData.ApprovalGrid);
        //            // GenericToDataTable(objApprovalData.ApprovalGrid);
        //            var fileName = "AttendanceApproval_Data.xls";
        //            string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
        //            //save the file to server temp folder
        //            if (System.IO.File.Exists(fullPath))
        //            {
        //                System.IO.File.Delete(fullPath);
        //            }
        //            //fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
        //            objApprovalData.Dt.TableName = "Attendance Approval Report";

        //            // var ds = GetDataSetFromDB();
        //            // var excelApp = OfficeOpenXML.GetInstance();
        //            // var file = excelApp.GetExcelStream(ds, false);
        //            //   return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Guid.NewGuid().ToString() + ".xlsx");

        //            if (objApprovalData.Dt.Rows.Count > 0)
        //            {

        //                using (XLWorkbook wb = new XLWorkbook())
        //                {
        //                    wb.Worksheets.Add(objApprovalData.Dt, objApprovalData.Dt.TableName);
        //                    using (MemoryStream stream = new MemoryStream())
        //                    {
        //                        wb.SaveAs(stream);
        //                        stream.Position = 0;
        //                        FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        //                        stream.WriteTo(file);
        //                        file.Close();

        //                        //         //Build the File Path.
        //                        //         // string path = Server.MapPath("~/Files/") + fileName;
        //                        //         // return File(fullPath, "application/vnd.ms-excel");
        //                        //         // return File(path, "application/pdf");

        //                        //      //   ////Read the File data into Byte Array.
        //                        //         byte[] bytes = System.IO.File.ReadAllBytes(fullPath);

        //                        //         ////Send the File to Download.
        //                        //         ////  return File(bytes, "application/octet-stream", fileName);
        //                        //         //return File(bytes, "application/vnd.ms-excel", fileName);

        //                        //         // MyMemoryStream.WriteTo(Response.OutputStream);
        //                        //         //Response.Flush();
        //                        //         //Response.End();
        //                    }

        //                    return Json(new { fileName = fileName, errorMessage = "" });
        //                }
        //            }
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Msg = ex.Message.ToString();
        //    }
        //    return null;
        //    // return File("","","");
        //    // return PartialView("_AttendanceApprovalReport", objApprovalData);

        //}
        ////public DataTable GenericToDataTable(IList<T> list)
        ////{
        ////    var json = JsonConvert.SerializeObject(list);
        ////    DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
        ////    return dt;
        ////}

        //public void SendMail(string branch)
        //{
        //    DataSet ds = new DataSet();
        //    // ds = objBLL.GetDealerApprovalMailData(Session["UserName"].ToString(), ddlBranch.SelectedValue);
        //    MailMessage mailMessage = new MailMessage();
        //    string MailCC1 = "", MailCC2 = "", SMTPSrver = "", FromEmailid = "";
        //    string Toemail = "";
        //    string CCMailId = ""; string BCCMailId = "";
        //    string userName = "", subject = "";
        //    //if (ds.Tables[0].Rows.Count > 0)
        //    //{
        //    //    Toemail = ds.Tables[0].Rows[0]["TomailID"].ToString();
        //    //    CCMailId = ds.Tables[0].Rows[0]["CCmailID"].ToString();
        //    //    BCCMailId = "Sidconswebdev@ap.sony.com"; //ds.Tables[0].Rows[0]["BCCmailID"].ToString();
        //    //}
        //    Toemail = "Sidconswebdev@ap.sony.com";
        //    CCMailId = "Sidconswebdev@ap.sony.com";
        //    SMTPSrver = ConfigurationManager.AppSettings.Get("MailHost").ToString();
        //    FromEmailid = ConfigurationManager.AppSettings.Get("FromEmailid").ToString();
        //    MailCC1 = CCMailId;
        //    MailCC2 = BCCMailId;
        //    //Attachment attachment = new Attachment(message.Attachments());

        //    string HostAdd = SMTPSrver;
        //    subject = "Ambo Changes ";

        //    string Pass = ConfigurationManager.AppSettings["MailPassword"].ToString();
        //    string status = "";

        //    //creating the object of MailMessage 
        //    string table = "";// "</br>DealerDetails </br> <table border=1 width='550'>" +
        //    //    "<tr><td> Financier Retailer Code / POS ID</td><td> RetailerName </td><td> Branch </td><td> State </td>" +
        //    //    "<td> City </td> <td> PinCode </td><td>StartDate </td><td>Status</td></tr> " +
        //    //    "<tr><td>" + txtDelearCode.Text + "</td><td>" + txtRetailerName.Text + "</td><td>" + ddlBranch.SelectedItem + "</td>" +
        //    //    "<td>" + ddlState.SelectedItem + "</td> <td>" + txtCity.Text + "</td><td>" + txtPincode.Text + "</td><td>" + DateTime.Now.ToString("dd/MM/yyyy") + "</td><td>" + status + "</td></tr> </table>";

        //    string Msg = "<html>" + "<body>Hi Sir / Mam,<br/><br/>" +
        //                               " <H3>Below request is pending for your approval</H3> <br/><br/> " +
        //                               "These are the below information<br/> " +
        //                               "<br/><br/>Best Regards,<br/>Team Sony</body> " +
        //                                "</html>";

        //    //MailMessage mailMessage = new MailMessage();

        //    mailMessage.From = new MailAddress(FromEmailid); //From Email Id  
        //    mailMessage.Subject = subject; //Subject of Email  
        //                                   // mailMessage.Body = mailMessage.Body; ; //body or message of Email  
        //    mailMessage.IsBodyHtml = true;
        //    //mailMessage.Attachments.Add(attachment);
        //    if (Toemail != "")
        //    {
        //        mailMessage.To.Add(Toemail);
        //        mailMessage.Body = Msg;
        //        if (CCMailId != "")
        //        {
        //            mailMessage.CC.Add(CCMailId);
        //        }
        //        if (BCCMailId != "")
        //        {
        //            mailMessage.Bcc.Add(BCCMailId);
        //        }


        //        SmtpClient smtp = new SmtpClient();  // creating object of smptpclient  

        //        smtp.Host = HostAdd;                 //host of emailaddress for example smtp.gmail.com etc  

        //        //network and security related credentials  
        //        smtp.EnableSsl = false;
        //        NetworkCredential NetworkCred = new NetworkCredential();
        //        NetworkCred.UserName = mailMessage.From.Address;
        //        NetworkCred.Password = Pass;
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = NetworkCred;
        //        smtp.Port = 25;


        //        smtp.Send(mailMessage);
        //    }
        //    mailMessage.Dispose();


        //}

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
        //        //SendMail(objFormData.BranchId.ToString());
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
        ////public JsonResult UpdateApproveAttendance(UpdateMonthlyReport objFormData)
        ////{
        ////    string Message = "";
        ////    Envelope<bool> output = new Envelope<bool>();
        ////    try
        ////    {
        ////        //  objFormData.Remarks = null;
        ////        if (objFormData.Remarks == null)
        ////        {
        ////            output.Message = "Please Enter Remarks";
        ////            AddErrorNotification(output.Message);
        ////            return Json(output);
        ////        }
        ////        objFormData.UserId = objUserSession.UserId;
        ////        objFormData.EncryptKey = objUserSession.EncryptKey;
        ////        objFormData.RoleName = objUserSession.RoleName;

        ////        output = _APIWebReportsData.UpdateApproveAttendance(objFormData);
        ////        //SendMail(objFormData.BranchId.ToString());
        ////        //if(output.MessageCode == (int)Acceptable.Accepted)
        ////        //{
        ////        //    AddSuccessNotification(output.Message);
        ////        //}
        ////        return Json(output);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Message = ex.Message;
        ////        return Json(new Envelope<bool> { Data = false, Message = Message, MessageCode = 0 });
        ////    }


        ////}



        //[HttpPost]
        //public ActionResult ExportApprovalRecordExcel(UpdateMonthlyReport objGridData)
        //{
        //    String Msg = "";
        //    try
        //    {
        //        if (objGridData.StartDate == null || objGridData.StartDate == "")
        //            objGridData.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
        //        if (objGridData.EndDate == null || objGridData.EndDate == "")
        //            objGridData.EndDate = DateTime.Now.ToString("dd/MM/yyyy");
        //        var userDetails = (BaseSession)(Session["BaseSession"]);
        //        //if (userDetails.RoleName == "RDI")
        //        //    objGridData.BranchId = _APIMappingData.GetBranchByUserId(Convert.ToInt64(userDetails.UserId)).BranchId;
        //        //else
        //        //    objGridData.BranchId = 0;
        //        objGridData.UserId = objUserSession.UserId;
        //        //objGridData.BranchId=
        //        objGridData.RoleName = objUserSession.RoleName;
        //        DataTable output = _APIWebReportsData.GetMonthlyAttendanceApprovalReport(objGridData).Data;
        //        var fileName = "MonthlyAttendanceApproval_Data.xls";
        //        string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
        //        //save the file to server temp folder
        //        if (System.IO.File.Exists(fullPath))
        //        {
        //            System.IO.File.Delete(fullPath);
        //        }
        //        //fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
        //        output.TableName = "Attendance Approval Report";
        //        if (output.Rows.Count > 0)
        //        {
        //            using (XLWorkbook wb = new XLWorkbook())
        //            {
        //                wb.Worksheets.Add(output, output.TableName);
        //                using (MemoryStream stream = new MemoryStream())
        //                {
        //                    wb.SaveAs(stream);
        //                    stream.Position = 0;
        //                    FileStream file = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        //                    stream.WriteTo(file);
        //                    file.Close();
        //                    return Json(new { fileName = fileName, errorMessage = "" });
        //                }
        //            }
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Msg = ex.Message.ToString();
        //    }
        //    return null;
        //}

        //[HttpPost]
        //[ValidateInput(false)]
        //public FileResult DownloadFinalReport(string GridHtml)
        //{
        //    // to remove search and index from the inner html .
        //    GridHtml = GridHtml.Replace("Search:", "");
        //    int firstindex = GridHtml.IndexOf("Showing");
        //    int lastindex = GridHtml.IndexOf("entries");
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append(GridHtml.Substring(0, firstindex));
        //    sb.Append(GridHtml.Substring(lastindex + 7));
        //    //y6 sb.Remove()
        //    // sb.Remove(0,1000);
        //    // StringBuilder builder = new StringBuilder();
        //    //string str = sb.ToString();
        //    // sb.Replace(")", "");
        //    // sb.Replace("'", "");
        //    // sb.Replace("///", "");
        //    ////sb.Replace(",", "");
        //    // sb.Replace("/", "");
        //    // sb.Replace("javascript:updatePresent(", "");

        //    GridHtml = string.Empty;
        //    GridHtml = sb.ToString();
        //    // sb.Remove(1, GridHtml.IndexOf("\n") + 1);
        //    // GridHtml = sb.ToString();

        //    // GridHtml.Remove(0, GridHtml.IndexOf("\n") + 1);
        //    //GridHtml.

        //    return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "AttendanceApprovalReport.xls");
        //}
        //#endregion

        #region ComboSales
        [HttpGet]
        public ActionResult ComboSales()
        {
            GetBranch InputParam = new GetBranch();
            ComboSalesReport view = new ComboSalesReport();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            if (rolen == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            if (Session["ComboSalesReportNoData"] == null)
                Session["ComboSalesReportNoData"] = 0;

            return View(view);
        }

        [HttpPost]
        public ActionResult ComboSalesReport(ComboSalesReport objGridData)
        {

            var output = _APIWebReportsData.GetComboSaleseReport(objGridData);
            //if (output.MessageCode == (int)Acceptable.Found)
            return Json(output.Data, JsonRequestBehavior.AllowGet);
            //else
            //return Json(new Envelope<IEnumerable<DailySalesReportIMEIGrid>> { Data = null, Message = "No data found", MessageCode = 0 });

        }

        [HttpPost]
        public void ComboSalesReportExcel(ComboSalesReport objGridData)
        {
            try
            {
                objGridData.start = 0;
                objGridData.length = 10000000;
                objGridData.draw = 1;
                DataTable dt = new DataTable("ComboSales");


                var model = _APIWebReportsData.GetComboSaleseReport(objGridData);
                if (model.MessageCode == (int)Acceptable.Found)
                {
                    var listdata = model.Data.data.ToList();
                    dt.Columns.AddRange(new DataColumn[26] { new DataColumn("Branch"),
                                            new DataColumn("Date"),
                                            new DataColumn("Month"),
                                            new DataColumn("Dealer Name"),
                                            new DataColumn("City"),
                                            new DataColumn("Location"),
                                            new DataColumn("Payer Name"),
                                            new DataColumn("Channel"),
                                            new DataColumn("Dealer State"),
                                            new DataColumn("Master Code"),
                                            new DataColumn("Dealer Code"),
                                            new DataColumn("Dealer Classification"),
                                            new DataColumn("SFA Code"),
                                            new DataColumn("SFA Name"),
                                            new DataColumn("SFA Level"),
                                            new DataColumn("Incentive Category"),
                                            new DataColumn("Company Name"),
                                            new DataColumn("Division"),
                                            new DataColumn("Product Category 1"),
                                            new DataColumn("Combo Material 1"),
                                            new DataColumn("Sale Date 1"),
                                            new DataColumn("Invoice No 1"),
                                            new DataColumn("Product Category 2"),
                                            new DataColumn("Combo Material 2"),
                                            new DataColumn("Sale Date 2"),
                                            new DataColumn("Invoice No 2") });
                    //new DataColumn("Core Category"),});
                    foreach (var record in listdata)
                    {
                        dt.Rows.Add(record.Branch, record.Date, record.Month, record.DealerName, record.City, record.Location, record.PayerName, record.Channel, record.State,
                            record.SAPCode, record.DealerCode, record.DealerClassification, record.SFACode, record.SFAName, record.SFALevel,
                            record.IncentiveCate, record.CompanyName, record.Division, record.ProCat1, record.ComboMaterial1,
                            record.SaleDate1, record.InvoiceNo1, record.ProCat2, record.ComboMaterial2, record.SaleDate2,
                            record.InvoiceNo2);
                    }

                    var fileName = "ComboSales_Data.xls";
                    string fullPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/DownloadTemplate"), fileName);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        string attachment = "attachment; filename=" + fileName;
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "application/vnd.ms-excel";
                        string tab = "";
                        foreach (DataColumn dc in dt.Columns)
                        {
                            Response.Write(tab + dc.ColumnName);
                            tab = "\t";
                        }
                        Response.Write("\n");
                        int i;
                        foreach (DataRow dr in dt.Rows)
                        {
                            tab = "";
                            for (i = 0; i < dt.Columns.Count; i++)
                            {
                                Response.Write(tab + dr[i].ToString());
                                tab = "\t";
                            }
                            Response.Write("\n");
                        }
                        //AddSuccessNotification(model.Message);

                        Response.End();

                        //return File(Response.OutputStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

                    }
                }
                else
                {
                    Session["ComboSalesReportNoData"] = 1;
                    Response.Redirect(Url.Action("ComboSales", "Reports", new
                    {

                        exceptionType = this.GetType().Name
                    }));
                }
            }
            catch (Exception ex)
            {

            }


        }
        #endregion


        #region FestivalTargetVsAchievement
        [HttpGet]
        public ActionResult FestivalTargetVsAchievementReport()
        {
            GetBranch InputParam = new GetBranch();
            FestivalTargetVsAchievementReportFilters view = new FestivalTargetVsAchievementReportFilters();
            InputParam.UserId = objUserSession.UserId;
            string rolen = objUserSession.RoleName;

            if (rolen == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            if (Session["FestivalTargetVsAchievementReportNoData"] == null)
                Session["FestivalTargetVsAchievementReportNoData"] = 0;

            return View(view);
        }

        [HttpPost]
        public ActionResult FestivalTargetVsAchievementReport(FestivalTargetVsAchievementReportFilters objGridData)
        {
            var output = _APIWebReportsData.GetFestivalTargetVsAchievementReport(objGridData);
            return Json(output.Data);
        }


        [HttpPost]
        public void DownloadFestivalTargetVsAchievementReport(FestivalTargetVsAchievementReportFilters objGridData)
        {
            objGridData.start = 0;
            objGridData.length = int.MaxValue;
            var output = _APIWebReportsData.GetFestivalTargetVsAchievementReport(objGridData);
            DataTable dt = new DataTable("FestivalTargetVsAchievementReport");

            if (output.MessageCode == (int)Acceptable.Found)
            {
                var listdata = output.Data.data.ToList();
                dt.Columns.AddRange(new DataColumn[18] {
                    new DataColumn("SchemeName"),
                    new DataColumn("BranchName"),
                    new DataColumn("SFAName"),
                    new DataColumn("SFACode"),
                    new DataColumn("SFALevel"),
                    new DataColumn("DealerName"),
                    new DataColumn("DealerCode"),
                    new DataColumn("Channel"),
                    new DataColumn("City"),
                    new DataColumn("Location"),
                    new DataColumn("ProductCategory"),
                    new DataColumn("TargetCategory"),
                    new DataColumn("IncentiveCategory"),
                    new DataColumn("Division"),
                    new DataColumn("TargetQty"),
                    new DataColumn("AchQty"),
                    new DataColumn("TargetValue"),
                    new DataColumn("AchValue")
                });
                foreach (var record in listdata)
                {
                    dt.Rows.Add(record.SchemeName, record.BranchName, record.SFAName, record.SFACode, record.SFALevel, record.DealerName,
                        record.DealerCode, record.Channel, record.City, record.Location, record.ProductCategory,
                        record.TargetCategory, record.IncentiveCategory, record.Division, record.TargetQty,
                        record.AchQty, record.TargetValue, record.AchValue);
                }

            }

            if (dt.Rows.Count > 0)
            {
                string attachment = "attachment; filename=FestivalTargetVsAchievementReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in dt.Rows)
                {
                    tab = "";
                    for (i = 0; i < dt.Columns.Count; i++)
                    {
                        Response.Write(tab + Convert.ToString(dr[i]));
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
            else
            {
                Session["FestivalTargetVsAchievementReportNoData"] = 1;
                Response.Redirect(Url.Action("FestivalTargetVsAchievementReport", "Reports", new
                {

                    exceptionType = this.GetType().Name
                }));
            }
        }
        #endregion TargetVsAchievement



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


     
    }


}





