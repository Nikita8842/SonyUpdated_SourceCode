using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.WebReports
{
    public class TrainingReport 
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public Int64? BranchId { get; set; }
        public Int64? TrainingId { get; set; }
        public Int64? ProductCategoryId { get; set; }
        public Int64? ChannelId { get; set; }
    }

    public class TrainingReportGrid
    {
        public Int64 TrainingId { get; set; }
        public string CourseName { get; set; }
        public string Branch { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string DealerName { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string Channel { get; set; }
        public string DealerState { get; set; }
        public string MasterCode { get; set; }
        public string DealerCode { get; set; }
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string SFALevel { get; set; }
        public string Question1 { get; set; }
        public string Question2 { get; set; }
        public string Question3 { get; set; }
    }
}
