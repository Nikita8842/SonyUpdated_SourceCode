using AmboLibrary.MasterMaintenance;
using AmboLibrary.WebReports;
using AmboUtilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboServices.Interface
{
    public interface IWebReportsService
    {
        Envelope<GridOutput<IEnumerable<CompetitionDisplayReportData>>> GetCompetitionDisplayReport(CompetitionDisplayReportFilters objGridVariables);
		
        Envelope<GridOutput<IEnumerable<CompetitionSFACountReportData>>> GetCompetitionSFACountReport(CompetitionSFACountReportFilters objGridVariables);

        Envelope<DownloadCompetitionSFACountReportData> DownloadCompetitionSFACountReportData(CompetitionSFACountReportFilters objGridVariables);

        Envelope<GridOutput<IEnumerable<MessageReportData>>> GetMessageReport(MessageReportFilters objGridVariables);

	    Envelope<GridOutput<IEnumerable<TargetVsAchievementReportData>>> GetTargetVsAchievementReport(TargetVsAchievementReportFilters objGridVariables);

        Envelope<GridOutput<IEnumerable<FestivalTargetVsAchievementReportData>>> GetFestivalTargetVsAchievementReport(FestivalTargetVsAchievementReportFilters objGridVariables);

        #region Daily Sales With Attribute
        Envelope<GridOutput<IEnumerable<DailySalesWithAttributeReportGrid>>> GetDailySalesWithAttributeReport(DailySalesWithAttributesReport objGridVariables);
        #endregion

        #region Daily Sales IMEI
        Envelope<GridOutput<IEnumerable<DailySalesReportIMEIGrid>>> GetDailySalesIMEIReport(DailySalesReportIMEI objGridVariables);
        #endregion

        Envelope<GridOutput<IEnumerable<DailyTimingReport>>> GetDailyTimingReport(DailyTimingReportInput Input);
		
		#region Stock Report
        Envelope<GridOutput<IEnumerable<StockReportGrid>>> GetStockReport(StockReport objGridVariables);
        #endregion

        #region Display Report
        Envelope<GridOutput<IEnumerable<DisplayReportGrid>>> GetDisplayReport(DisplayReport objGridVariables);
        #endregion

        Envelope<DataTable> GetMonthlyAttendanceReport(MonthlyAttendanceReportInput Input);
        Envelope<DataTable> GetMonthlyAttendanceReportDownload(MonthlyAttendanceReportInput Input);
        Envelope<bool> IsApprovedBranch(MonthlyAttendanceReportInput Input);

        #region Training Report
        Envelope<DataTable> GetTrainingReport(TrainingReport objGridVariables);
        #endregion

        Envelope<DataTable> GetMonthlyAttendanceApprovalReport(UpdateMonthlyReport Input);

        Envelope<List<ApprovalGrid>> GETApprovalDateStatusWise(UpdateMonthlyReport Input);

        #region ComboSalesReport
        Envelope<GridOutput<IEnumerable<ComboSalesReportGrid>>> GetComboSaleseReport(ComboSalesReport objGridVariables);
        //object GetSFABranchChangeReport(SFABranchChangeReportInput data);
        #endregion

       
    }
}
