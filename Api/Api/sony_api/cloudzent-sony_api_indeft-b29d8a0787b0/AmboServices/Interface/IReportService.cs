using AmboLibrary.ReportsManagement;
using AmboLibrary.SFAMobileApp;
using AmboLibrary.UserValidation;
using AmboUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboServices.Interface
{
    public interface IReportService
    {
        Envelope<UserDetailsModel> ValidateLogin(SFAValidationInput Input);
        Envelope<ModelWiseTrendList> GetModelWiseTrendReport(InputParam Input);
        Envelope<CategoryWiseTrendList> GetCategoryWiseTrendReport(ReportInputParam Input);
        Envelope<AccountWiseTrendList> GetAccountWiseTrendReport(AccountWiseTrendInput Input);
        Envelope<Sell_Thru_Report> MonthSellThruReport(MonthWiseSellThruReportIds Input);
        Envelope<Plan_vs_Actual_Graph> MonthPlanvsActualReport(MonthWiseSellThruReportIds Input);
        Envelope<LastThreeDaysData> Last3DaysSalesReport(MonthWiseSellThruReportIds Input);
        Envelope<MTDSellThruList> GetMTDSellThruReport(MTDSellThruReportInput Input);
        Envelope<PlanActualStockList> GetPlanActualStockReport(PlanActualStockDataInput Input);
        Envelope<MTDSonyVsCompSellReport> GetMTDSonyVsCompSellReport(MTDSonyVsCompSellInput Input);
        Envelope<SonyVsCompSellReportYrWise> GetSonyVsCompSellReportYrWise(PlanActualStockDataInput Input);
        Envelope<AccountCategoryWiseTrendReport> GetAccountCategoryWiseTrendReport(AccountCategoryWiseTrendInput Input);
        Envelope<CompetitionHeadCountReportList> GetCompetitionHeadCountReport(HeadCountReportInput Input);
        Envelope<LastThreeDaysData> Last3DaysSalesReport_V2(Last3DaysSalesInput Input);
        Envelope<BranchWiseSalesReportData> GetBranchWiseSalesTrendReport(BranchWiseSalesReportInput Input);
        Envelope<BranchWiseSalesChartData> GetBranchWiseSalesTrendChart(BranchWiseSalesChartInput Input);
        Envelope<SIDAppSyncModel> GetSIDAppSync(SIDSyncInput Input);
        Envelope<SIDDisplayList> GetSIDDisplayReport(SIDDisplayReportInput Input);
        Envelope<FestivalReportList> GetFestivalReport(FestivalReportInput Input);
        Envelope<DailyRangingChartReportData> GetDailyRangingGraphReport(DailyRangingChartReportInputModel Input);
        Envelope<TargetvsAchievementReportModel> GetTargetvsAchievementReport(TargetvsAchievementInputModel Input);

        Envelope<ComptHeadCountList> GetComptHeadCount(ComptHeadCountParam Input);
    }
}
