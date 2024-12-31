using AmboDataServices.Interface;
using AmboLibrary.ReportsManagement;
using AmboLibrary.SFAMobileApp;
using AmboLibrary.UserValidation;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboServices.Implimentation
{
    public class ReportService: IReportService
    {
        private readonly IReportDataService _IReportDataService;
        public ReportService(IReportDataService IReportDataService)
        {
            _IReportDataService = IReportDataService;
        }

        public Envelope<UserDetailsModel> ValidateLogin(SFAValidationInput Input)
        {
            string Message = "";
            var output = _IReportDataService.ValidateLogin(Input,out Message);
            return (output == null || output.Status != 1) ?
                new Envelope<UserDetailsModel> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message } :
                new Envelope<UserDetailsModel> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message };
        }

        public Envelope<ModelWiseTrendList> GetModelWiseTrendReport(InputParam Input)
        {
            var output = _IReportDataService.GetModelWiseTrendReport(Input);
            return (output == null || output.ModelWiseTrendData.Count() == 0) ?
                new Envelope<ModelWiseTrendList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<ModelWiseTrendList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Model Wise Trend Report data fetched successfully." };
        }

        public Envelope<CategoryWiseTrendList> GetCategoryWiseTrendReport(ReportInputParam Input)
        {
            var output = _IReportDataService.GetCategoryWiseTrendReport(Input);
            return (output == null || output.CategoryWiseTrendData.Count() == 0) ?
                new Envelope<CategoryWiseTrendList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<CategoryWiseTrendList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Category Wise Trend Report data fetched successfully." };
        }

        public Envelope<AccountWiseTrendList> GetAccountWiseTrendReport(AccountWiseTrendInput Input)
        {
            var Data = _IReportDataService.GetAccountWiseTrendReport(Input);
            return (Data ==null || Data.AccountWiseTrendData.Count()==0)?
                new Envelope<AccountWiseTrendList> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<AccountWiseTrendList> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Account Wise Trend Report data fetched successfully." };
        }

        public Envelope<Sell_Thru_Report> MonthSellThruReport(MonthWiseSellThruReportIds Input)
        {
            var Data = _IReportDataService.MonthSellThruReport(Input);
            return (Data == null || Data.Report.Count() == 0) ?
                new Envelope<Sell_Thru_Report> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<Sell_Thru_Report> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Month Sell Thru Report data fetched successfully." };
        }

        public Envelope<Plan_vs_Actual_Graph> MonthPlanvsActualReport(MonthWiseSellThruReportIds Input)
        {
            var Data = _IReportDataService.MonthPlanvsActualReport(Input);
            return (Data == null || Data.Report.Count() == 0) ?
                new Envelope<Plan_vs_Actual_Graph> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<Plan_vs_Actual_Graph> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Month Plan vs Actual Report data fetched successfully." };
        }

        public Envelope<LastThreeDaysData> Last3DaysSalesReport(MonthWiseSellThruReportIds Input)
        {
            var Data = _IReportDataService.Last3DaysSalesReport(Input);
            return (Data == null || Data.ModelWiseTrendLastThreeDaysData.Count() == 0) ?
                new Envelope<LastThreeDaysData> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<LastThreeDaysData> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Last 3 Days Sales Report data fetched successfully." };
        }

        public Envelope<MTDSellThruList> GetMTDSellThruReport(MTDSellThruReportInput Input)
        {
            var Data = _IReportDataService.GetMTDSellThruReport(Input);
            return (Data == null || Data.MTDSellThruData.Count() == 0) ?
                new Envelope<MTDSellThruList> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<MTDSellThruList> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "MTD Sell Thru Report data fetched successfully." };
        }

        public Envelope<PlanActualStockList> GetPlanActualStockReport(PlanActualStockDataInput Input)
        {
            var Data = _IReportDataService.GetPlanActualStockReport(Input);
            return (Data == null || Data.PlanActualStockData.Count() == 0) ?
                new Envelope<PlanActualStockList> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<PlanActualStockList> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Plan vs Actual vs Stock Report data fetched successfully." };
        }

        public Envelope<MTDSonyVsCompSellReport> GetMTDSonyVsCompSellReport(MTDSonyVsCompSellInput Input)
        {
            var Data = _IReportDataService.GetMTDSonyVsCompSellReport(Input);
            return (Data == null || Data.Data.Count() == 0) ?
                new Envelope<MTDSonyVsCompSellReport> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<MTDSonyVsCompSellReport> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "MTD Sony Vs Comp Sell Report data fetched successfully." };
        }

        public Envelope<SonyVsCompSellReportYrWise> GetSonyVsCompSellReportYrWise(PlanActualStockDataInput Input)
        {
            var Data = _IReportDataService.GetSonyVsCompSellReportYrWise(Input);
            return (Data == null || Data.SonyVsCompSellReportYrWiseList.Count() == 0) ?
                new Envelope<SonyVsCompSellReportYrWise> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<SonyVsCompSellReportYrWise> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Sony Vs Competitor Sell Report Yr Wise Report data fetched successfully." };
        }

        public Envelope<AccountCategoryWiseTrendReport> GetAccountCategoryWiseTrendReport(AccountCategoryWiseTrendInput Input)
        {
            var Data = _IReportDataService.GetAccountCategoryWiseTrendReport(Input);
            return (Data == null || Data.AccountCategoryWiseTrendData.Count() == 0) ?
                new Envelope<AccountCategoryWiseTrendReport> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<AccountCategoryWiseTrendReport> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Account/Category Wise Trend Report data fetched successfully." };
        }
        public Envelope<CompetitionHeadCountReportList> GetCompetitionHeadCountReport(HeadCountReportInput Input)
        {
            var Data = _IReportDataService.GetCompetitionHeadCountReport(Input);
            return (Data == null || Data.CompetitionHeadCountData.Count() == 0) ?
                new Envelope<CompetitionHeadCountReportList> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<CompetitionHeadCountReportList> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Competition Head Count Report data fetched successfully." };
        }
        public Envelope<LastThreeDaysData> Last3DaysSalesReport_V2(Last3DaysSalesInput Input)
        {
            var Data = _IReportDataService.Last3DaysSalesReport_V2(Input);
            return (Data == null || Data.ModelWiseTrendLastThreeDaysData.Count() == 0) ?
                new Envelope<LastThreeDaysData> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<LastThreeDaysData> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Last 3 days sales report data fetched successfully." };
        }
        public Envelope<BranchWiseSalesReportData> GetBranchWiseSalesTrendReport(BranchWiseSalesReportInput Input)
        {
            var Data = _IReportDataService.GetBranchWiseSalesTrendReport(Input);
            return (Data == null || Data.BranchWiseTrendData.Count() == 0) ?
                new Envelope<BranchWiseSalesReportData> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<BranchWiseSalesReportData> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Branch wise sales trend report data fetched successfully." };
        }
        public Envelope<BranchWiseSalesChartData> GetBranchWiseSalesTrendChart(BranchWiseSalesChartInput Input)
        {
            var Data = _IReportDataService.GetBranchWiseSalesTrendChart(Input);
            return (Data == null || Data.BranchWiseTrendData.Count() == 0) ?
                new Envelope<BranchWiseSalesChartData> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<BranchWiseSalesChartData> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Branch wisee sales trend chart data fetched successfully." };
        }
        public Envelope<SIDAppSyncModel> GetSIDAppSync(SIDSyncInput Input)
        {
            var Data = _IReportDataService.GetSIDAppSync(Input);
            return (Data == null) ?
                new Envelope<SIDAppSyncModel> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<SIDAppSyncModel> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "SID App Sync data fetched successfully." };
        }
        public Envelope<SIDDisplayList> GetSIDDisplayReport(SIDDisplayReportInput Input)
        {
            var Data = _IReportDataService.GetSIDDisplayReport(Input);
            return (Data == null) ?
                new Envelope<SIDDisplayList> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<SIDDisplayList> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "SID Display Report data fetched successfully." };
        }
        public Envelope<FestivalReportList> GetFestivalReport(FestivalReportInput Input)
        {
            var Data = _IReportDataService.GetFestivalReport(Input);
            return (Data == null && Data.FestivalReport.Count() == 0) ?
                new Envelope<FestivalReportList> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<FestivalReportList> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Festival Report data fetched successfully." };
        }
        public Envelope<DailyRangingChartReportData> GetDailyRangingGraphReport(DailyRangingChartReportInputModel Input)
        {
            var Data = _IReportDataService.GetDailyRangingGraphReport(Input);
            return (Data == null && Data.DailyRangingChartReport.Count() == 0) ?
                new Envelope<DailyRangingChartReportData> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<DailyRangingChartReportData> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Daily Ranging Graph data fetched successfully." };
        }
        public Envelope<TargetvsAchievementReportModel> GetTargetvsAchievementReport(TargetvsAchievementInputModel Input)
        {
            var Data = _IReportDataService.GetTargetvsAchievementReport(Input);
            return (Data == null && Data.TargetvsAchievementReport.Count() == 0) ?
                new Envelope<TargetvsAchievementReportModel> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<TargetvsAchievementReportModel> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Target vs Achievement data fetched successfully." };
        }

        public Envelope<ComptHeadCountList> GetComptHeadCount(ComptHeadCountParam Input)
        {
            var Data = _IReportDataService.GetComptHeadCount(Input);
            return (Data == null || Data.ComptHeadCountData.Count() == 0) ?
                new Envelope<ComptHeadCountList> { Data = Data, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<ComptHeadCountList> { Data = Data, MessageCode = (int)Acceptable.Found, Message = "Competition Head Count data fetched successfully." };
        }
    }
}
