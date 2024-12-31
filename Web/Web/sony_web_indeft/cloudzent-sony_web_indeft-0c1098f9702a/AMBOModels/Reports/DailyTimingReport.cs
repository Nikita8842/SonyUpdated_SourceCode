using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.Reports
{
    public class DailyTimingReport
    {
        public Int64 RowNum { get; set; }
        public string LoginId { get; set; }
        public string Region { get; set; }
        public string BranchName { get; set; }
        public string MasterCode { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string DealerLocation { get; set; }
        public string DealerCity { get; set; }
        public string SFACode { get; set; }
        public string SFALevel { get; set; }
        public string IncentiveCategory { get; set; }
        public string SFACategory { get; set; }
        public string SFAName { get; set; }
        public string SFALocation { get; set; }
        public string Attendance { get; set; }
        public Int64 AttendanceTypeId { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string ImagePath { get; set; }
        public string CreationDate { get; set; }
        public string IMEI1 { get; set; }
        public string SalesStatus { get; set; }
        public string CoreSalesStatus { get; set; }
        public string TotalWorkingHrs { get; set; }
        public string ShiftName { get; set; }
        public string StoreLocation { get; set; }
    }
    public class DailyTimingReportList
    {
        public List<DailyTimingReport> DailyTimingReport { get; set; }
        public DailyTimingReportList()
        {
            DailyTimingReport = new List<DailyTimingReport>();
        }
    }
    public class DailyTimingReportInput : ReportsGrid
    {
        public Int64? BranchId { get; set; }
        public Int64? SFAId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Int64? AttType { get; set; }
        public Int64 PageStart { get; set; }
        public Int64 PageLength { get; set; }
    }
}
