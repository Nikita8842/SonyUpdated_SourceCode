using AmboLibrary.ReportsManagement;
using AmboLibrary.SFAMobileApp;
using System.Collections.Generic;
using System.Data;

namespace AmboProvider.Interface
{
    public interface ISFAMobileReportsProvider
    {
        MTDModelWiseDSRReportList GetMTDSalesReport(ReportInput Input);
        List<MTDModelWiseComboReportModel> GetMTDModelWiseComboReport(ReportInput Input);
        List<MTDComboSalesReportModel> GetMTDComboSalesReport(ReportInput Input);
        TodaySalesReportList GetTodaySalesReport(ReportInput Input);
        LastDaySalesReportList GetLastDaySalesReport(ReportInput Input);
        SFATargetvsAchievementList GetSFATargetvsAchievementReport(SFATvsAInput Input);
        List<SFAFestivalTargetvsAchievementModel> GetSFAFestivalTargetVsAchievement(SFATvsAInput Input);
        List<SFAAttendanceReportForRDI> GetAttendanceCountSFAForRDI(SFAAttendanceReportForRDIInput Input);
        DataTable GetComboSalesReportMobile(ComboSalesReportInput Input);
    }
}
