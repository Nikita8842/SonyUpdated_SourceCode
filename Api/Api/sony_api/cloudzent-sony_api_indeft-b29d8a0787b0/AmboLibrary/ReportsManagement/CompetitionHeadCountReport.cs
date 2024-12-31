using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class CompetitionHeadCountReport
    {
        public string BrandName { get; set; }
        public int SFACount { get; set; }
    }
    public class CompetitionHeadCountReportList
    {
        public List<CompetitionHeadCountReport> CompetitionHeadCountData { get; set; }
        public CompetitionHeadCountReportList()
        {
            CompetitionHeadCountData = new List<CompetitionHeadCountReport>();
        }
    }
    public class HeadCountReportInput
    {
        public Int64 BranchId { get; set; }
        public Int64 TypeId { get; set; }
        public Int64 ChannelId { get; set; }
        public Int64 DealerId { get; set; }
        public Int64 UserId { get; set; }
    }

    ///for competition headcount report for mobile
    public class ComptHeadCountParam
    {
        public int BranchId { get; set; }
        public int ChannelId { get; set; }
        public int CompBrandId { get; set; }
        public int StoreId { get; set; }
    }
    public class ComptHeadCountList
    {
        public List<ComptHeadCount> ComptHeadCountData { get; set; }
        public ComptHeadCountList()
        {
            ComptHeadCountData = new List<ComptHeadCount>();
        }
    }
    public class ComptHeadCount
    {
        public string Company { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
    }
}
