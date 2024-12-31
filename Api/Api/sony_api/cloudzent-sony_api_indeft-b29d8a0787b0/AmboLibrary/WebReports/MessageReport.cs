using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.WebReports
{
    public class MessageReportFilters : ReportsGrid
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public Int64? BranchId { get; set; }
        public Int64? StateId { get; set; }
        public Int64? SFAId { get; set; }
        public Int64? DivisionId { get; set; }
        public Int64 MessageTypeId { get; set; }
        public List<Int64> DealerIds { get; set; }
    }

    public class MessageReportData
    {
        public string SendDate { get; set; }
        public string ReplyDate { get; set; }
        public string Branch { get; set; }
        public string Dealer { get; set; }
        public string MasterCode { get; set; }
        public string DealerCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string Channel { get; set; }
        public string SFACode { get; set; }
        public string SFA { get; set; }
        public string SFALevel { get; set; }
        public string SFADivision { get; set; }
        public string IncentiveCategory { get; set; }
        public string Message { get; set; }
        public string MessageFile { get; set; }
        public string Reply { get; set; }
        public string ReplyFile { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string MessageType { get; set; }
    }
}
