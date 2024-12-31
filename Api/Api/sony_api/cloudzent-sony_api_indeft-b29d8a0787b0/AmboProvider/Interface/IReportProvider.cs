using AmboLibrary.ReportsManagement;
using AmboLibrary.SFAMobileApp;
using AmboLibrary.UserValidation;

namespace AmboProvider.Interface
{
    public interface IReportProvider
    {
        UserDetailsModel ValidateLogin(SFAValidationInput UserDetails, out string Message);
        ModelWiseTrendList GetModelWiseTrendReport(InputParam Input);
        CategoryWiseTrendList GetCategoryWiseTrendReport(ReportInputParam Input);
        AccountWiseTrendList GetAccountWiseTrendReport(AccountWiseTrendInput Input);
        Sell_Thru_Report MonthSellThruReport(MonthWiseSellThruReportIds Input);
        Plan_vs_Actual_Graph MonthPlanvsActualReport(MonthWiseSellThruReportIds Input);
        LastThreeDaysData Last3DaysSalesReport(MonthWiseSellThruReportIds Input);
        MTDSellThruList GetMTDSellThruReport(MTDSellThruReportInput Input);
        PlanActualStockList GetPlanActualStockReport(PlanActualStockDataInput Input);
        MTDSonyVsCompSellReport GetMTDSonyVsCompSellReport(MTDSonyVsCompSellInput Input);
        SonyVsCompSellReportYrWise GetSonyVsCompSellReportYrWise(PlanActualStockDataInput Input);
        AccountCategoryWiseTrendReport GetAccountCategoryWiseTrendReport(AccountCategoryWiseTrendInput Input);
        CompetitionHeadCountReportList GetCompetitionHeadCountReport(HeadCountReportInput Input);
        LastThreeDaysData Last3DaysSalesReport_V2(Last3DaysSalesInput Input);
        BranchWiseSalesReportData GetBranchWiseSalesTrendReport(BranchWiseSalesReportInput Input);
        BranchWiseSalesChartData GetBranchWiseSalesTrendChart(BranchWiseSalesChartInput Input);
        SIDAppSyncModel GetSIDAppSync(SIDSyncInput Input);
        SIDDisplayList GetSIDDisplayReport(SIDDisplayReportInput Input);
        FestivalReportList GetFestivalReport(FestivalReportInput Input);
        DailyRangingChartReportData GetDailyRangingGraphReport(DailyRangingChartReportInputModel Input);
        TargetvsAchievementReportModel GetTargetvsAchievementReport(TargetvsAchievementInputModel Input);

        ComptHeadCountList GetComptHeadCount(ComptHeadCountParam Input);
    }
}
