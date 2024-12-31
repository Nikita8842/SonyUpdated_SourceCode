using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class Last3DaysSalesReportModel
    {
        public String ModelName { get; set; }
        public Int64 MTD { get; set; }
        public Int64 CurrentDaySale { get; set; }
        public Int64 LastDaySale { get; set; }
        public Int64 SecondLastDaySale { get; set; }
        public Int64 CurrentSale { get; set; }
    }
    public class LastThreeDaysData
    {
        public string CurrentDay { get; set; }
        public string LastDay { get; set; }
        public string SecondLastDay { get; set; }
        public List<Last3DaysSalesReportModel> ModelWiseTrendLastThreeDaysData { get; set; }
        public LastThreeDaysData()
        {
            ModelWiseTrendLastThreeDaysData = new List<Last3DaysSalesReportModel>();
        }
    }
    public class Last3DaysSalesInput
    {
        public Int64 BranchId { get; set; }
        public Int64 ProductCatId { get; set; }
        public Int64 UserId { get; set; }
        public Int64 ChannelId { get; set; }
        public DateTime Date { get; set; }
        public List<Int64> ProductSubCatIdList { get; set; }
        public Last3DaysSalesInput()
        {
            ProductSubCatIdList = new List<Int64>();
        }
    }
}
