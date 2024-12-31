using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class FestivalReport
    {
        public string Name { get; set; }
        public Int64 CurrYear { get; set; }
        public Int64 PrevYear { get; set; }
        public Int64 Prev2Year { get; set; }
        public Int64 CurrVSPrev { get; set; }
        public Int64 PrevVS2ndPrev { get; set; }
    }
    public class FestivalReportList
    {
        public List<FestivalReport> FestivalReport { get; set; }
        public FestivalReportList()
        {
            FestivalReport = new List<FestivalReport>();
        }
    }
    public class FestivalReportInput
    {
        public Int64 RoleId { get; set; }
        public Int64 FestivalTypeId { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 ProductCategoryId { get; set; }
        public Int64 ChannelId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
