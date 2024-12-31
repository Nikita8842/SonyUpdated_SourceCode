using AmboLibrary.WebReports;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SonyAmbo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Compression]
    public class WebReportsController : ApiController
    {
        private readonly IWebReportsService _IWebReportsService;

        public WebReportsController(IWebReportsService IWebReportsService)
        {
            _IWebReportsService = IWebReportsService;
        }

        [HttpPost]
        public IHttpActionResult GetCompetitionDisplayReport(Envelope<CompetitionDisplayReportFilters> param)
        {
            var output = _IWebReportsService.GetCompetitionDisplayReport(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetCompetitionSFACountReport(Envelope<CompetitionSFACountReportFilters> param)
        {
            var output = _IWebReportsService.GetCompetitionSFACountReport(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult DownloadCompetitionSFACountReportData(Envelope<CompetitionSFACountReportFilters> param)
        {
            var output = _IWebReportsService.DownloadCompetitionSFACountReportData(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetMessageReport(Envelope<MessageReportFilters> param)
        {
            var output = _IWebReportsService.GetMessageReport(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetTargetVsAchievementReport(Envelope<TargetVsAchievementReportFilters> param)
        {
            var output = _IWebReportsService.GetTargetVsAchievementReport(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetFestivalTargetVsAchievementReport(Envelope<FestivalTargetVsAchievementReportFilters> param)
        {
            var output = _IWebReportsService.GetFestivalTargetVsAchievementReport(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetDailySalesWithAttributeReport(Envelope<DailySalesWithAttributesReport> param)
        {
            var output = _IWebReportsService.GetDailySalesWithAttributeReport(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetDailySalesIMEIReport(Envelope<DailySalesReportIMEI> param)
        {
            var output = _IWebReportsService.GetDailySalesIMEIReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Daily Timing Report from Database.
        /// Dhruv Sharma, ValueFirst, Gurgaon
        /// </summary>
        /// <param name="param">Input Parameters</param>
        /// <returns>Daily Timing Report</returns>
        [HttpPost]
        public IHttpActionResult GetDailyTimingReport(Envelope<DailyTimingReportInput> param)
        {
            var output = _IWebReportsService.GetDailyTimingReport(param.Data);
            return Ok(output);
        }
		
		[HttpPost]
        public IHttpActionResult GetStockReport(Envelope<StockReport> param)
        {
            var output = _IWebReportsService.GetStockReport(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetDisplayReport(Envelope<DisplayReport> param)
        {
            var output = _IWebReportsService.GetDisplayReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Monthly Attendance Report from Database.
        /// Dhruv Sharma, ValueFirst, Gurgaon
        /// </summary>
        /// <param name="param">Input Parameters</param>
        /// <returns>Monthly Attendance Report</returns>
        [HttpPost]
        public IHttpActionResult GetMonthlyAttendanceReport(Envelope<MonthlyAttendanceReportInput> param)
        {
            var output = _IWebReportsService.GetMonthlyAttendanceReport(param.Data);
            return Ok(output);
        }
        [HttpPost]
        public IHttpActionResult GetMonthlyAttendanceReportDownload(Envelope<MonthlyAttendanceReportInput> param)
        {
            var output = _IWebReportsService.GetMonthlyAttendanceReportDownload(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Monthly Attendance Report from Database.
        /// Dhruv Sharma, ValueFirst, Gurgaon
        /// </summary>
        /// <param name="param">Input Parameters</param>
        /// <returns>Monthly Attendance Report</returns>
        [HttpPost]
        public IHttpActionResult IsApprovedBranch(Envelope<MonthlyAttendanceReportInput> param)
        {
            var output = _IWebReportsService.IsApprovedBranch(param.Data);
            return Ok(output);
        }



        /// <summary>
        /// To fetch Training Report from Database.
        /// Bela Nalavade, ValueFirst, Gurgaon
        /// </summary>
        /// <param name="param">Input Parameters</param>
        /// <returns>Monthly Attendance Report</returns>
        [HttpPost]
        public IHttpActionResult GetTrainingReport(Envelope<TrainingReport> param)
        {
            var output = _IWebReportsService.GetTrainingReport(param.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetMonthlyAttendanceApprovalReport(Envelope<UpdateMonthlyReport> param)
        {
            var output = _IWebReportsService.GetMonthlyAttendanceApprovalReport(param.Data);
            return Ok(output);
        }
      

        [HttpPost]
        public IHttpActionResult GETApprovalDateStatusWise(Envelope<UpdateMonthlyReport> param)
        {
            var output = _IWebReportsService.GETApprovalDateStatusWise(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch sales data with combo sales Report from Database.
        /// Bela Nalavade, ValueFirst, Gurgaon
        /// </summary>
        /// <param name="param">Input Parameters</param>
        /// <returns>Combo Sales Report</returns>
        [HttpPost]
        public IHttpActionResult GetComboSaleseReport(Envelope<ComboSalesReport> param)
        {
            var output = _IWebReportsService.GetComboSaleseReport(param.Data);
            return Ok(output);
        }

        
    }

}