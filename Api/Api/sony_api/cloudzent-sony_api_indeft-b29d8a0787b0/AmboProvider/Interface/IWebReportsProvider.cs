using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.WebReports;

namespace AmboProvider.Interface
{
    public interface IWebReportsProvider
    {
        GridOutput<IEnumerable<CompetitionDisplayReportData>> GetCompetitionDisplayReport(CompetitionDisplayReportFilters objGridVariables);

        GridOutput<IEnumerable<CompetitionSFACountReportData>> GetCompetitionSFACountReport(CompetitionSFACountReportFilters objGridVariables);

        DownloadCompetitionSFACountReportData DownloadCompetitionSFACountReportData(CompetitionSFACountReportFilters objGridVariables);

        GridOutput<IEnumerable<MessageReportData>> GetMessageReport(MessageReportFilters objGridVariables);

        GridOutput<IEnumerable<TargetVsAchievementReportData>> GetTargetVsAchievementReport(TargetVsAchievementReportFilters objGridVariables);

        GridOutput<IEnumerable<FestivalTargetVsAchievementReportData>> GetFestivalTargetVsAchievementReport(FestivalTargetVsAchievementReportFilters objGridVariables);

        #region Daily Sales With Attribute
        GridOutput<IEnumerable<DailySalesWithAttributeReportGrid>> GetDailySalesWithAttributeReport(DailySalesWithAttributesReport objGridVariables);
        #endregion

        #region ComboSalesReport
        GridOutput<IEnumerable<ComboSalesReportGrid>> GetComboSaleseReport(ComboSalesReport objGridVariables);
        #endregion

        #region Daily Sales IMEI
        GridOutput<IEnumerable<DailySalesReportIMEIGrid>> GetDailySalesIMEIReport(DailySalesReportIMEI objGridVariables);
        #endregion

        GridOutput<IEnumerable<DailyTimingReport>> GetDailyTimingReport(DailyTimingReportInput Input);
		
		#region Stock Report
        GridOutput<IEnumerable<StockReportGrid>> GetStockReport(StockReport objGridVariables);
        #endregion

        #region Display Report
        GridOutput<IEnumerable<DisplayReportGrid>> GetDisplayReport(DisplayReport objGridVariables);
        #endregion

        #region Training Report
        DataTable GetTrainingReport(TrainingReport objGridVariables);
        #endregion

        DataTable GetMonthlyAttendanceReport(MonthlyAttendanceReportInput Input);


        int IsApprovedBranch(MonthlyAttendanceReportInput Input,out string Message);

        
        DataTable GetMonthlyAttendanceApprovalReport(UpdateMonthlyReport Input);
        DataTable GetMonthlyAttendanceReportDownload(MonthlyAttendanceReportInput Input);
        //DataTable GetMonthlyAttendanceReporttDownload(UpdateMonthlyReport Input);


        List<ApprovalGrid> GETApprovalDateStatusWise(UpdateMonthlyReport Input);


       
    }
}
