using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class MonthSellThruReportModel
    {
        public string Date { get; set; }
        public Int64 Quantity { get; set; }
    }
    public class Sell_Thru_Report
    {
        public List<MonthSellThruReportModel> Report { get; set; }
        public Sell_Thru_Report()
        {
            Report = new List<MonthSellThruReportModel>();
        }
    }
    public class MonthWiseSellThruReportIds
    {
        public Int64 BranchId { get; set; }
        public Int64 ProductCatId { get; set; }
        public Int64 UserId { get; set; }
        public List<Int64> ProductSubCatIdList { get; set; }
        public MonthWiseSellThruReportIds()
        {
            ProductSubCatIdList = new List<Int64>();
        }
    }
}
