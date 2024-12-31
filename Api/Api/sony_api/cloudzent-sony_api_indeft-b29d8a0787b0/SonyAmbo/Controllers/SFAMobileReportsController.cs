using AmboLibrary.ReportsManagement;
using AmboLibrary.SFAMobileApp;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SonyAmbo.Controllers
{
    /// <summary>
    /// For SFA Mobile App Reports.
    /// Dhruv Sharma, Value First, Gurugram, India
    /// </summary>
    [ExceptionHandling]
    [Compression]
    public class SFAMobileReportsController : ApiController
    {
        private static ISFAMobileReportsService _Reports;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ISFAMobileReportsService"></param>
        public SFAMobileReportsController(ISFAMobileReportsService ISFAMobileReportsService)
        {
            _Reports = ISFAMobileReportsService;
        }

        /// <summary>
        /// To get MDT Model Wise DSR Report.
        /// </summary>
        /// <param name="Input">LoginId</param>
        /// <returns>MDT Model Wise DSR Report Data.</returns>
        [HttpPost]
        public IHttpActionResult GetMTDSalesReport(Envelope<ReportInput> Input)
        {
            var Report = _Reports.GetMTDSalesReport(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get MDT Model Wise Combo Report.
        /// </summary>
        /// <param name="Input">LoginId</param>
        /// <returns>MDT Model Wise combo Report Data.</returns>
        [HttpPost]
        public IHttpActionResult GetMTDModelWiseComboReport(Envelope<ReportInput> Input)
        {
            var Report = _Reports.GetMTDModelWiseComboReport(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get Today Sales Report.
        /// </summary>
        /// <param name="Input">Login Id</param>
        /// <returns>Today Sales Report</returns>
        [HttpPost]
        public IHttpActionResult GetTodaySalesReport(Envelope<ReportInput> Input)
        {
            var Report = _Reports.GetTodaySalesReport(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get Last Day Sales Report.
        /// </summary>
        /// <param name="Input">Login Id</param>
        /// <returns>Last Day Sales Report</returns>
        [HttpPost]
        public IHttpActionResult GetLastDaySalesReport(Envelope<ReportInput> Input)
        {
            var Report = _Reports.GetLastDaySalesReport(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get SFA Target vs Achievement Report data.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 3, 2018
        /// </summary>
        /// <param name="Input">SFA Id</param>
        /// <returns>SFA Target vs Achievement Report data.</returns>
        [HttpPost]
        public IHttpActionResult GetSFATargetvsAchievementReport(Envelope<SFATvsAInput> Input)
        {
            var Report = _Reports.GetSFATargetvsAchievementReport(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get SFA Target vs Achievement Report data.
        /// Bela Nalavade, ValueFirst, Gurugram
        /// May 8, 2019
        /// </summary>
        /// <param name="Input">SFA Id</param>
        /// <returns>SFA Festival Target vs Achievement Report data.</returns>
        [HttpPost]
        public IHttpActionResult GetSFAFestivalTargetVsAchievement(Envelope<SFATvsAInput> Input)
        {
            var Report = _Reports.GetSFAFestivalTargetVsAchievement(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// To get Combo Sales Report data.
        /// Bela Nalavade, ValueFirst, Gurugram
        /// May 8, 2019
        /// </summary>
        /// <param name="Input">SFA Id</param>
        /// <returns>Combo Salesvc Report data.</returns>
        [HttpPost]
        public IHttpActionResult GetMTDComboSalesReport(Envelope<ReportInput> Input)
        {
            var Report = _Reports.GetMTDComboSalesReport(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// This is for total attendance count for RDI
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Envelope<List<SFAAttendanceReportForRDI>>))]
        public IHttpActionResult GetAttendanceCountSFAForRDI(Envelope<SFAAttendanceReportForRDIInput> Input)
        {
            var Report = _Reports.GetAttendanceCountSFAForRDI(Input.Data);
            return Ok(Report);
        }

        /// <summary>
        /// This is for combosales report
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(Envelope<DataTable>))]
        public IHttpActionResult GetComboSalesReportMobile(Envelope<ComboSalesReportInput> Input)
        {
            var Report = _Reports.GetComboSalesReportMobile(Input.Data);
            return Ok(Report);
        }
    }
}
