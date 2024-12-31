using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.SFAMobileApp
{
    public class SFATargetvsAchievementModel
    {
       
        public string TargetCategory { get; set; }
        public string QtyTagret { get; set; }
        public string QtyAch { get; set; }
        public string ValueTarget { get; set; }
        public string ValueAch { get; set; }
    }
    public class SFATargetvsAchievementList
    {
        public List<SFATargetvsAchievementModel> SFATargetvsAchievement { get; set; }
        public SFATargetvsAchievementList()
        {
            SFATargetvsAchievement = new List<SFATargetvsAchievementModel>();
        }
    }

    public class SFATvsAInput
    {
        public Int64 SFAId { get; set; }
    }

    public class SFAFestivalTargetvsAchievementModel
    {
        public string Month { get; set; }
        public string SchemeName { get; set; }
        public string TargetCategory { get; set; }
        public string QtyTagret { get; set; }
        public string QtyAch { get; set; }
        public string ValueTarget { get; set; }
        public string ValueAch { get; set; }
    }
}
