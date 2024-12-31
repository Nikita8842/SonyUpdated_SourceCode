using AmboDataServices.Interface;
using AmboLibrary.ReportsManagement;
using AmboLibrary.SFAMobileApp;
using AmboLibrary.UserValidation;
using AmboProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboDataServices.Implimentation
{
    public class ReportDataService: IReportDataService
    {
        private readonly IReportProvider _IReportProvider;

        public ReportDataService(IReportProvider IReportProvider)
        {
            _IReportProvider = IReportProvider;
        }

        public UserDetailsModel ValidateLogin(SFAValidationInput UserDetails, out string Message)
        {
            return _IReportProvider.ValidateLogin(UserDetails, out Message);
        }

        public ModelWiseTrendList GetModelWiseTrendReport(InputParam Input)
        {
            return _IReportProvider.GetModelWiseTrendReport(Input);
        }

        public CategoryWiseTrendList GetCategoryWiseTrendReport(ReportInputParam Input)
        {
            return _IReportProvider.GetCategoryWiseTrendReport(Input);
        }

        public AccountWiseTrendList GetAccountWiseTrendReport(AccountWiseTrendInput Input)
        {
            return _IReportProvider.GetAccountWiseTrendReport(Input);
        }

        public Sell_Thru_Report MonthSellThruReport(MonthWiseSellThruReportIds Input)
        {
            return _IReportProvider.MonthSellThruReport(Input);
        }
        public Plan_vs_Actual_Graph MonthPlanvsActualReport(MonthWiseSellThruReportIds Input)
        {
            return _IReportProvider.MonthPlanvsActualReport(Input);
        }
        public LastThreeDaysData Last3DaysSalesReport(MonthWiseSellThruReportIds Input)
        {
            return _IReportProvider.Last3DaysSalesReport(Input);
        }

        public MTDSellThruList GetMTDSellThruReport(MTDSellThruReportInput Input)
        {
            return _IReportProvider.GetMTDSellThruReport(Input);
        }

        public PlanActualStockList GetPlanActualStockReport(PlanActualStockDataInput Input)
        {
            return _IReportProvider.GetPlanActualStockReport(Input);
        }
        public MTDSonyVsCompSellReport GetMTDSonyVsCompSellReport(MTDSonyVsCompSellInput Input)
        {
            return _IReportProvider.GetMTDSonyVsCompSellReport(Input);
        }

        public SonyVsCompSellReportYrWise GetSonyVsCompSellReportYrWise(PlanActualStockDataInput Input)
        {
            return _IReportProvider.GetSonyVsCompSellReportYrWise(Input);
        }
        public AccountCategoryWiseTrendReport GetAccountCategoryWiseTrendReport(AccountCategoryWiseTrendInput Input)
        {
            return _IReportProvider.GetAccountCategoryWiseTrendReport(Input);
        }
        public CompetitionHeadCountReportList GetCompetitionHeadCountReport(HeadCountReportInput Input)
        {
            return _IReportProvider.GetCompetitionHeadCountReport(Input);
        }
        public LastThreeDaysData Last3DaysSalesReport_V2(Last3DaysSalesInput Input)
        {
            return _IReportProvider.Last3DaysSalesReport_V2(Input);
        }
        public BranchWiseSalesReportData GetBranchWiseSalesTrendReport(BranchWiseSalesReportInput Input)
        {
            return _IReportProvider.GetBranchWiseSalesTrendReport(Input);
        }
        public BranchWiseSalesChartData GetBranchWiseSalesTrendChart(BranchWiseSalesChartInput Input)
        {
            return _IReportProvider.GetBranchWiseSalesTrendChart(Input);
        }
        public SIDAppSyncModel GetSIDAppSync(SIDSyncInput Input)
        {
            return _IReportProvider.GetSIDAppSync(Input);
        }
        public SIDDisplayList GetSIDDisplayReport(SIDDisplayReportInput Input)
        {
            return _IReportProvider.GetSIDDisplayReport(Input);
        }
        public FestivalReportList GetFestivalReport(FestivalReportInput Input)
        {
            return _IReportProvider.GetFestivalReport(Input);
        }
        public DailyRangingChartReportData GetDailyRangingGraphReport(DailyRangingChartReportInputModel Input)
        {
            return _IReportProvider.GetDailyRangingGraphReport(Input);
        }
        public TargetvsAchievementReportModel GetTargetvsAchievementReport(TargetvsAchievementInputModel Input)
        {
            return _IReportProvider.GetTargetvsAchievementReport(Input);
        }

        public ComptHeadCountList GetComptHeadCount(ComptHeadCountParam Input)
        {
            return _IReportProvider.GetComptHeadCount(Input);
        }
        
    }
}
