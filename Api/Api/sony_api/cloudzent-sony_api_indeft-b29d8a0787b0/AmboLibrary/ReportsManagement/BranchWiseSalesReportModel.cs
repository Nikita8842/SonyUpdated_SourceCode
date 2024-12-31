using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class BranchWiseSalesReportModel
    {
        public string Name { get; set; }
        public Int64 CurrentMonthSale { get; set; }
        public Int64 LastMonthSale { get; set; }
        public Int64 SecondLastMonthSale { get; set; }
        public Int64 PreYrCurrentMonthSale { get; set; }
        public Int64 PreYrLastMonthSale { get; set; }
        public Int64 PreYrSecondLastMonthSale { get; set; }
    }
    public class BranchWiseSalesReportData
    {
        public string CurrentMonth { get; set; }
        public string LastMonth { get; set; }
        public string SecondLastMonth { get; set; }
        public string PreYrCurrentMonth { get; set; }
        public string PreYrLastMonth { get; set; }
        public string PreYrSecondLastMonth { get; set; }
        public List<BranchWiseSalesReportModel> BranchWiseTrendData { get; set; }
        public BranchWiseSalesReportData()
        {
            BranchWiseTrendData = new List<BranchWiseSalesReportModel>();
        }
    }

    public class BranchWiseSalesReportInput
    {
        public Int64 BranchId { get; set; }
        public Int64 ProductCategoryId { get; set; }
        public Int64 UserId { get; set; }
        public List<Int64> ProductSubCategoryId { get; set; }
        public Int64 ChannelId { get; set; }
        public Int64 DealerId { get; set; }
        public BranchWiseSalesReportInput()
        {
            ProductSubCategoryId = new List<Int64>();
        }
    }
}
