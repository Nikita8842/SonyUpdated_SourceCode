using AmboDataServices.Interface;
using AmboLibrary.ReportsManagement;
using AmboLibrary.SFAMobileApp;
using AmboProvider.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboDataServices.Implimentation
{
    public class SFAMobileReportsDataService : ISFAMobileReportsDataService
    {
        private readonly ISFAMobileReportsProvider _SFAMobileReports;

        public SFAMobileReportsDataService(ISFAMobileReportsProvider ISFAMobileReportsProvider)
        {
            _SFAMobileReports = ISFAMobileReportsProvider;
        }

        public MTDModelWiseDSRReportList GetMTDSalesReport(ReportInput Input)
        {
            return _SFAMobileReports.GetMTDSalesReport(Input);
        }

        public TodaySalesReportList GetTodaySalesReport(ReportInput Input)
        {
            return _SFAMobileReports.GetTodaySalesReport(Input);
        }

        public LastDaySalesReportList GetLastDaySalesReport(ReportInput Input)
        {
            return _SFAMobileReports.GetLastDaySalesReport(Input);
        }
        public SFATargetvsAchievementList GetSFATargetvsAchievementReport(SFATvsAInput Input)
        {
            return _SFAMobileReports.GetSFATargetvsAchievementReport(Input);
        }

        public List<MTDModelWiseComboReportModel> GetMTDModelWiseComboReport(ReportInput Input)
        {
            return _SFAMobileReports.GetMTDModelWiseComboReport(Input);
        }

        public List<SFAFestivalTargetvsAchievementModel> GetSFAFestivalTargetVsAchievement(SFATvsAInput Input)
        {
            return _SFAMobileReports.GetSFAFestivalTargetVsAchievement(Input);
        }

        public List<MTDComboSalesReportModel> GetMTDComboSalesReport(ReportInput Input)
        {
            return _SFAMobileReports.GetMTDComboSalesReport(Input);
        }

        public List<SFAAttendanceReportForRDI> GetAttendanceCountSFAForRDI(SFAAttendanceReportForRDIInput Input)
        {
            return _SFAMobileReports.GetAttendanceCountSFAForRDI(Input);
        }

        public DataTable GetComboSalesReportMobile(ComboSalesReportInput Input)
        {
            return _SFAMobileReports.GetComboSalesReportMobile(Input);
        }
    }
}
