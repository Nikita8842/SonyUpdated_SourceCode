using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class SIDDisplayReport
    {
        public string WeekISO { get; set; }
        public string Date { get; set; }
        public string DealerName { get; set; }
        public string ModelName { get; set; }
        public Int64 PlannedQty { get; set; }
        public Int64 DisplayQty { get; set; }
        public Int64 StoreBackStock { get; set; }
    }
    public class SIDDisplayList
    {
        public List<SIDDisplayReport> SIDDisplayReport { get; set; }
        public SIDDisplayList()
        {
            SIDDisplayReport = new List<SIDDisplayReport>();
        }
    }

    public class SIDDisplayReportInput
    {
        public Int64 BranchId { get; set; }
        public Int64 ProductCategoryId { get; set; }
        public Int64 ChannelId { get; set; }
        public Int64 DealerId { get; set; }
        public Int64 UserId { get; set; }
    }
}
