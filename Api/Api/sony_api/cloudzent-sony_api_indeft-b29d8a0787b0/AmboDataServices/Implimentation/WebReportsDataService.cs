using AmboDataServices.Interface;
using AmboProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.WebReports;
using System.Data;

namespace AmboDataServices.Implimentation
{
    public class WebReportsDataService : IWebReportsDataService
    {
        private readonly IWebReportsProvider _IWebReportsProvider;

        public WebReportsDataService(IWebReportsProvider IWebReportsProvider)
        {
            _IWebReportsProvider = IWebReportsProvider;
        }

        public GridOutput<IEnumerable<CompetitionDisplayReportData>> GetCompetitionDisplayReport(CompetitionDisplayReportFilters objGridVariables)
        {
            return _IWebReportsProvider.GetCompetitionDisplayReport(objGridVariables);
        }

        public GridOutput<IEnumerable<CompetitionSFACountReportData>> GetCompetitionSFACountReport(CompetitionSFACountReportFilters objGridVariables)
        {
            return _IWebReportsProvider.GetCompetitionSFACountReport(objGridVariables);
        }

        public DownloadCompetitionSFACountReportData DownloadCompetitionSFACountReportData(CompetitionSFACountReportFilters objGridVariables)
        {
            return _IWebReportsProvider.DownloadCompetitionSFACountReportData(objGridVariables);
        }

        public GridOutput<IEnumerable<MessageReportData>> GetMessageReport(MessageReportFilters objGridVariables)
        {
            return _IWebReportsProvider.GetMessageReport(objGridVariables);
        }

        public GridOutput<IEnumerable<TargetVsAchievementReportData>> GetTargetVsAchievementReport(TargetVsAchievementReportFilters objGridVariables)
        {
            return _IWebReportsProvider.GetTargetVsAchievementReport(objGridVariables);
        }

        #region Daily Sales With Attribute
        public GridOutput<IEnumerable<DailySalesWithAttributeReportGrid>> GetDailySalesWithAttributeReport(DailySalesWithAttributesReport objGridVariables)
        {
            return _IWebReportsProvider.GetDailySalesWithAttributeReport(objGridVariables);
        }
        #endregion

        #region Daily Sales IMEI
        public GridOutput<IEnumerable<DailySalesReportIMEIGrid>> GetDailySalesIMEIReport(DailySalesReportIMEI objGridVariables)
        {
            return _IWebReportsProvider.GetDailySalesIMEIReport(objGridVariables);
        }
        #endregion

        public GridOutput<IEnumerable<DailyTimingReport>> GetDailyTimingReport(DailyTimingReportInput Input)
        {
            return _IWebReportsProvider.GetDailyTimingReport(Input);
        }
		
		#region Stock Report
        public GridOutput<IEnumerable<StockReportGrid>> GetStockReport(StockReport objGridVariables)
        {
            return _IWebReportsProvider.GetStockReport(objGridVariables);
        }
        #endregion

        #region Display Report
        public GridOutput<IEnumerable<DisplayReportGrid>> GetDisplayReport(DisplayReport objGridVariables)
        {
            return _IWebReportsProvider.GetDisplayReport(objGridVariables);
        }
        #endregion

        public DataTable GetMonthlyAttendanceReport(MonthlyAttendanceReportInput Input)
        {
            return _IWebReportsProvider.GetMonthlyAttendanceReport(Input);
        }
        public DataTable GetMonthlyAttendanceReportDownload(MonthlyAttendanceReportInput Input)
        {
            return _IWebReportsProvider.GetMonthlyAttendanceReportDownload(Input);
        }
        public bool IsApprovedBranch(MonthlyAttendanceReportInput Input, out string Message)
        {
            return Convert.ToBoolean(_IWebReportsProvider.IsApprovedBranch(Input, out Message));
        }
        
        public DataTable GetTrainingReport(TrainingReport objGridVariables)
        {
            return _IWebReportsProvider.GetTrainingReport(objGridVariables);
        }

        public DataTable GetMonthlyAttendanceApprovalReport(UpdateMonthlyReport Input)
        {
            return _IWebReportsProvider.GetMonthlyAttendanceApprovalReport(Input);
        }

        public List<ApprovalGrid> GETApprovalDateStatusWise(UpdateMonthlyReport Input)
        {
            return _IWebReportsProvider.GETApprovalDateStatusWise(Input);
        }

        #region ComboSales Report
        public GridOutput<IEnumerable<ComboSalesReportGrid>> GetComboSaleseReport(ComboSalesReport objGridVariables)
        {
            return _IWebReportsProvider.GetComboSaleseReport(objGridVariables);
        }

        public GridOutput<IEnumerable<FestivalTargetVsAchievementReportData>> GetFestivalTargetVsAchievementReport(FestivalTargetVsAchievementReportFilters objGridVariables)
        {
            return _IWebReportsProvider.GetFestivalTargetVsAchievementReport(objGridVariables);
        }
        #endregion

        
    }
}
