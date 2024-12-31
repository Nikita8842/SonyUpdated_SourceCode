using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.Reports
{
    public class TargetVsAchievementReportFilters : ReportsGrid
    {
        public string Month { get; set; }
        public Int64? BranchId { get; set; }
        public Int64? SFAId { get; set; }
        public Int64? DivisionId { get; set; }
        public List<Int64> ProductCategoryIds { get; set; }
    }

    public class TargetVsAchievementReportData
    {
        public string MonthName { get; set; }                                          /* added 29_june_24 vijay*/
        public string BranchName { get; set; }
        public string SFAName { get; set; }
        public string SFACode { get; set; }
        public string SFALevel { get; set; }
        public string DealerName { get; set; }
        public string DealerCode { get; set; }
        public string SAPCode { get; set; }
        public string Channel { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string ProductCategory { get; set; }
        public string TargetCategory { get; set; }
        public string IncentiveCategory { get; set; }
        public string Division { get; set; }
        public Int64 TargetQty { get; set; }
        public Int64 AchQty { get; set; }
        public Int64 TargetValue { get; set; }
        public Int64 AchValue { get; set; }
    }
}
