using AmboLibrary.ReportsManagement;
using AmboLibrary.SFAMobileApp;
using AmboUtilities;
using System.Collections.Generic;
using System.Data;

namespace AmboServices.Interface
{
    public interface ISFAMobileReportsService
    {
        Envelope<MTDModelWiseDSRReportList> GetMTDSalesReport(ReportInput InputParam);
        Envelope<List<MTDModelWiseComboReportModel>> GetMTDModelWiseComboReport(ReportInput Input);
        Envelope<List<MTDComboSalesReportModel>> GetMTDComboSalesReport(ReportInput Input);
        Envelope<TodaySalesReportList> GetTodaySalesReport(ReportInput Input);
        Envelope<LastDaySalesReportList> GetLastDaySalesReport(ReportInput Input);
        Envelope<SFATargetvsAchievementList> GetSFATargetvsAchievementReport(SFATvsAInput Input);
        Envelope<List<SFAFestivalTargetvsAchievementModel>> GetSFAFestivalTargetVsAchievement(SFATvsAInput Input);
        Envelope<List<SFAAttendanceReportForRDI>> GetAttendanceCountSFAForRDI(SFAAttendanceReportForRDIInput Input);
        Envelope<DataTable> GetComboSalesReportMobile(ComboSalesReportInput Input);
    }
}
