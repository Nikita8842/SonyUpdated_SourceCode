using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.MasterMaintenance;
using AMBOModels.Reports;
using APIAccessLayer.Helper;

namespace APIAccessLayer.INTERFACE
{
    public interface IAPIWebReportsData
    {
        Envelope<GridOutput<IEnumerable<CompetitionDisplayReportData>>> GetCompetitionDisplayReport(CompetitionDisplayReportFilters objGridData);

        Envelope<GridOutput<IEnumerable<CompetitionSFACountReportData>>> GetCompetitionSFACountReport(CompetitionSFACountReportFilters objGridData);

        Envelope<DownloadCompetitionSFACountReportData> DownloadCompetitionSFACountReportData(CompetitionSFACountReportFilters objGridVariables);

        Envelope<GridOutput<List<MessageReportData>>> GetMessageReport(MessageReportFilters objGridData);

        Envelope<GridOutput<IEnumerable<TargetVsAchievementReportData>>> GetTargetVsAchievementReport(TargetVsAchievementReportFilters objGridData);

        Envelope<GridOutput<IEnumerable<DailySalesWithAttributeReportGrid>>> GetDailySalesWithAttributeReport(DailySalesWithAttributesReport objGridVariables);

        Envelope<GridOutput<IEnumerable<DailySalesReportIMEIGrid>>> GetDailySalesIMEIReport(DailySalesReportIMEI objGridVariables);

        Envelope<GridOutput<IEnumerable<DailyTimingReport>>> GetDailyTimingReport(DailyTimingReportInput objGridData);
		
		Envelope<GridOutput<IEnumerable<StockReportGrid>>> GetStockReport(StockReport objGridVariables);

        Envelope<GridOutput<List<DisplayReportGrid>>> GetDisplayReport(DisplayReport objGridVariables);

        Envelope<DataTable> GetMonthlyAttendanceReport(MonthlyAttendanceReportInput Input);
        Envelope<DataTable> GetMonthlyAttendanceReportDownload(MonthlyAttendanceReportInput Input);

        Envelope<bool> IsApprovedBranch(MonthlyAttendanceReportInput Input); 

        Envelope<DataTable> GetTrainingReport(TrainingReport objGridVariables);

        Envelope<bool> UpdateAttendance(UpdateMonthlyReport objFormData);
      

        Envelope<DataTable> GetMonthlyAttendanceApprovalReport(UpdateMonthlyReport Input);
        Envelope<List<ApprovalGrid>> GETApprovalDateStatusWise(UpdateMonthlyReport Input);

        Envelope<GridOutput<IEnumerable<ComboSalesReportGrid>>> GetComboSaleseReport(ComboSalesReport objGridVariables);

        Envelope<GridOutput<IEnumerable<FestivalTargetVsAchievementReportData>>> GetFestivalTargetVsAchievementReport(FestivalTargetVsAchievementReportFilters objGridVariables);

       

    }
}
