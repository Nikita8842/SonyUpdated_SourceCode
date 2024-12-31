using AmboLibrary.ReportsManagement;
using AmboLibrary.SFAMobileApp;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SonyAmbo.Controllers
{
    /// <summary>
    /// To fetch reports for SID Analytics App.
    /// Dhruv Sharma, ValueFirst, Gurugram
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [ExceptionHandling]
    [Compression]
    public class ReportController : ApiController
    {
        private readonly IReportService _IReportService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="IReportService"></param>
        public ReportController(IReportService IReportService)
        {
            _IReportService = IReportService;
        }

        /// <summary>
        /// To validate user login for SID App.
        /// </summary>
        /// <param name="param">User Cridentials</param>
        /// <returns>User Id, Name, Role Id, Role Name</returns>
        [HttpPost]
        public IHttpActionResult ValidateLogin(Envelope<SFAValidationInput> param)
        {
            var output = _IReportService.ValidateLogin(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Model Wise Trend Report.
        /// </summary>
        /// <param name="param">Branch Id, Product Category Id, Product Sub Category Id, ChannelId</param>
        /// <returns>Model Wise Trend Report.</returns>
        [HttpPost]
        public IHttpActionResult GetModelWiseTrendReport(Envelope<InputParam> param)
        {
            var output = _IReportService.GetModelWiseTrendReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Category Wise Trend Report.
        /// </summary>
        /// <param name="param">Branch Id, ChannelId</param>
        /// <returns>Category Wise Trend Report.</returns>
        [HttpPost]
        public IHttpActionResult GetCategoryWiseTrendReport(Envelope<ReportInputParam> param)
        {
            var output = _IReportService.GetCategoryWiseTrendReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Account Wise Trend Report.
        /// </summary>
        /// <param name="param">Master Code</param>
        /// <returns>Account Wise Trend Report.</returns>
        [HttpPost]
        public IHttpActionResult GetAccountWiseTrendReport(Envelope<AccountWiseTrendInput> param)
        {
            var output = _IReportService.GetAccountWiseTrendReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Month Sell Thru Report.
        /// </summary>
        /// <param name="param">Branch Id, Product Category Id, Product Sub Category Id</param>
        /// <returns>Month Sell Thru Report</returns>
        [HttpPost]
        public IHttpActionResult MonthSellThruReport(Envelope<MonthWiseSellThruReportIds> param)
        {
            var output = _IReportService.MonthSellThruReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Month Plan vs Actual Report.
        /// </summary>
        /// <param name="param">Branch Id, Product Category Id, Product Sub Category Id</param>
        /// <returns>Month Plan vs Actual Report</returns>
        [HttpPost]
        public IHttpActionResult MonthPlanvsActualReport(Envelope<MonthWiseSellThruReportIds> param)
        {
            var output = _IReportService.MonthPlanvsActualReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Last 3 Days Sales Report.
        /// </summary>
        /// <param name="param">Branch Id, Product Category Id, Product Sub Category Id</param>
        /// <returns>Last 3 Days Sales Report</returns>
        [HttpPost]
        public IHttpActionResult Last3DaysSalesReport(Envelope<MonthWiseSellThruReportIds> param)
        {
            var output = _IReportService.Last3DaysSalesReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch MTD Sell Thru Report.
        /// </summary>
        /// <param name="param">Dealer Id, Product Sub Category Id</param>
        /// <returns>MTD Sell Thru Report</returns>
        [HttpPost]
        public IHttpActionResult GetMTDSellThruReport(Envelope<MTDSellThruReportInput> param)
        {
            var output = _IReportService.GetMTDSellThruReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Plan vs Actual vs Stock Report.
        /// </summary>
        /// <param name="Input">DealerId, ProductCategoryId, ProductSubActegoryId</param>
        /// <returns>Plan vs Actual vs Stock Report</returns>
        [HttpPost]
        public IHttpActionResult GetPlanActualStockReport(Envelope<PlanActualStockDataInput> param)
        {
            var output = _IReportService.GetPlanActualStockReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch MTD Sony Vs Comp Sell Report.
        /// </summary>
        /// <param name="param">DealerId, ProductCategoryId, ProductSubActegoryId</param>
        /// <returns>MTD Sony Vs Comp Sell Report</returns>
        [HttpPost]
        public IHttpActionResult GetMTDSonyVsCompSellReport(Envelope<MTDSonyVsCompSellInput> param)
        {
            var output = _IReportService.GetMTDSonyVsCompSellReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch MTD Sony vs Competitor Sell Report.
        /// </summary>
        /// <param name="param">Dealer Id, ProductId, SubProductId</param>
        /// <returns>MTD Sony vs Competitor Sell Report</returns>
        [HttpPost]
        public IHttpActionResult GetSonyVsCompSellReportYrWise(Envelope<PlanActualStockDataInput> param)
        {
            var output = _IReportService.GetSonyVsCompSellReportYrWise(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Account Category Wise Trend Report.
        /// </summary>
        /// <param name="param">MasterCode, ProductId, SubProductId</param>
        /// <returns>Account Category Wise Trend Report</returns>
        [HttpPost]
        public IHttpActionResult GetAccountCategoryWiseTrendReport(Envelope<AccountCategoryWiseTrendInput> param)
        {
            var output = _IReportService.GetAccountCategoryWiseTrendReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Competition Head Count Report.
        /// </summary>
        /// <param name="param">BranchId, TypeId, ChannelId,DealerId</param>
        /// <returns>Competition Head Count Report</returns>
        [HttpPost]
        public IHttpActionResult GetCompetitionHeadCountReport(Envelope<HeadCountReportInput> param)
        {
            var output = _IReportService.GetCompetitionHeadCountReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Last 3 Days Sales Report based on selected date.
        /// </summary>
        /// <param name="param">Branch Id, Product Category Id, Product Sub Category Id,Channel Id, Date</param>
        /// <returns>Last 3 Days Sales Report</returns>
        [HttpPost]
        public IHttpActionResult Last3DaysSalesReport_V2(Envelope<Last3DaysSalesInput> param)
        {
            var output = _IReportService.Last3DaysSalesReport_V2(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Branch Wise Sales Trend Report.
        /// </summary>
        /// <param name="param">Branch Id, Product Category Id, Product Sub Category Id, Channel Id, Dealer Id</param>
        /// <returns>Branch Wise Sales Trend Report.</returns>
        [HttpPost]
        public IHttpActionResult GetBranchWiseSalesTrendReport(Envelope<BranchWiseSalesReportInput> param)
        {
            var output = _IReportService.GetBranchWiseSalesTrendReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get Branch wise sales trend report chart.
        /// </summary>
        /// <param name="param">Branch Id</param>
        /// <returns>Branch wise sales trend report chart.</returns>
        [HttpPost]
        public IHttpActionResult GetBranchWiseSalesTrendChart(Envelope<BranchWiseSalesChartInput> param)
        {
            var output = _IReportService.GetBranchWiseSalesTrendChart(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get SID App Sync Data.
        /// Dhruv Sharma, ValueFirst, Gurugram
        /// May 23, 2018
        /// </summary>
        /// <param name="param">User Id</param>
        /// <returns>SID App Sync Data</returns>
        [HttpPost]
        public IHttpActionResult GetSIDAppSync(Envelope<SIDSyncInput> param)
        {
            var output = _IReportService.GetSIDAppSync(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get SID Display Report.
        /// Dhruv Sharma, VFirst, Gurgaon
        /// </summary>
        /// <param name="param">Input parameters</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSIDDisplayReport(Envelope<SIDDisplayReportInput> param)
        {
            var output = _IReportService.GetSIDDisplayReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get Festival Report data for SID.
        /// Dhruv Sharma, VFirst, Gurgaon
        /// </summary>
        /// <param name="param">Filters</param>
        /// <returns>Festival Report</returns>
        [HttpPost]
        public IHttpActionResult GetFestivalReport(Envelope<FestivalReportInput> param)
        {
            var output = _IReportService.GetFestivalReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get Daily Ranging Graph data for SID.
        /// Dhruv Sharma, VFirst, Gurgaon
        /// </summary>
        /// <param name="param">Filters</param>
        /// <returns>Daily Ranging Graph data</returns>
        [HttpPost]
        public IHttpActionResult GetDailyRangingGraphReport(Envelope<DailyRangingChartReportInputModel> param)
        {
            var output = _IReportService.GetDailyRangingGraphReport(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get Target vs Achievement report data for SID.
        /// Dhruv Sharma, VFirst, Gurgaon
        /// </summary>
        /// <param name="param">Filters</param>
        /// <returns>Target vs Achievement report data</returns>
        [HttpPost]
        public IHttpActionResult GetTargetvsAchievementReport(Envelope<TargetvsAchievementInputModel> param)
        {
            var output = _IReportService.GetTargetvsAchievementReport(param.Data);
            return Ok(output);
        }


        /// <summary>
        /// To fetch Competition Head Count for mobile.
        /// </summary>
        /// <param name="param">BranchId,ChannelId,CompetitionBrand,StoreId</param>
        /// <returns>Competition Head Count for mobile</returns>
        [HttpPost]
        public IHttpActionResult GetComptHeadCount(Envelope<ComptHeadCountParam> param)
        {
            var output = _IReportService.GetComptHeadCount(param.Data);
            return Ok(output);
        }
    }
}
