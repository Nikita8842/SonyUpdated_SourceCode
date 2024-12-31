using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class TargetvsAchievementModel
    {
        public string ProductCategoryId { get; set; }
        public string ProductCategory { get; set; }
        public string QtyMonthTarget1 { get; set; }
        public string QtyMonthAch1 { get; set; }
        public string QtyMonthTarget2 { get; set; }
        public string QtyMonthAch2 { get; set; }
        public string QtyMonthTarget3 { get; set; }
        public string QtyMonthAch3 { get; set; }
        public string ValueMonthTarget1 { get; set; }
        public string ValueMonthAch1 { get; set; }
        public string ValueMonthTarget2 { get; set; }
        public string ValueMonthAch2 { get; set; }
        public string ValueMonthTarget3 { get; set; }
        public string ValueMonthAch3 { get; set; }
    }
    public class TargetvsAchievementReportModel
    {
        public string C_Month { get; set; }
        public string L_Month { get; set; }
        public string L_2_Month { get; set; }
        public List<TargetvsAchievementModel> TargetvsAchievementReport { get; set; }
        public TargetvsAchievementReportModel()
        {
            TargetvsAchievementReport = new List<TargetvsAchievementModel>();
        }
    }
    public class TargetvsAchievementInputModel
    {
        public int DealerId { get; set; }
    }
}
