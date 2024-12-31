using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class CategoryWiseTrendReport
    {
        public string ProductCategory { get; set; }
        public Int64 CurrentMonthSale { get; set; }
        public Int64 LastMonthSale { get; set; }
        public Int64 SecondLastMonthSale { get; set; }
        public Int64 ThirdLastMonthSale { get; set; }
        public Int64 PreYrCurrentMonthSale { get; set; }
        public Int64 PreYrLastMonthSale { get; set; }
        public Int64 PreYrSecondLastMonthSale { get; set; }
        public Int64 PreYrThirdLastMonthSale { get; set; }
    }
    public class CategoryWiseTrendList
    {
        public string CurrentMonth { get; set; }
        public string LastMonth { get; set; }
        public string SecondLastMonth { get; set; }
        public string ThirdLastMonth { get; set; }
        public string PreYrCurrentMonth { get; set; }
        public string PreYrLastMonth { get; set; }
        public string PreYrSecondLastMonth { get; set; }
        public string PreYrThirdLastMonth { get; set; }
        public List<CategoryWiseTrendReport> CategoryWiseTrendData { get; set; }
        public CategoryWiseTrendList()
        {
            CategoryWiseTrendData = new List<CategoryWiseTrendReport>();
        }
    }
    public class ReportInputParam
    {
        public Int64 BranchId { get; set; }
        public Int64 ChannelId { get; set; }
        public Int64 UserId { get; set; }
    }
}
