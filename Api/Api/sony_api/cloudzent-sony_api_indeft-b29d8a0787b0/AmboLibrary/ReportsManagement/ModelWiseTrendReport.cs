using System;
using System.Collections.Generic;

namespace AmboLibrary.ReportsManagement
{
    public class ModelWiseTrendReport
    {
        public string ModelName { get; set; }
        public Int64 CurrentMonthSale { get; set; }
        public Int64 LastMonthSale { get; set; }
        public Int64 SecondLastMonthSale { get; set; }
        public Int64 ThirdLastMonthSale { get; set; }
        public Int64 PreYrCurrentMonthSale { get; set; }
        public Int64 PreYrLastMonthSale { get; set; }
        public Int64 PreYrSecondLastMonthSale { get; set; }
        public Int64 PreYrThirdLastMonthSale { get; set; }
        public Int64 ClosingStock { get; set; }
        public Int64 DisplayStock { get; set; }
    }
    public class ModelWiseTrendList
    {
        public string CurrentMonth { get; set; }
        public string LastMonth { get; set; }
        public string SecondLastMonth { get; set; }
        public string ThirdLastMonth { get; set; }
        public string PreYrCurrentMonth { get; set; }
        public string PreYrLastMonth { get; set; }
        public string PreYrSecondLastMonth { get; set; }
        public string PreYrThirdLastMonth { get; set; }
        public List<ModelWiseTrendReport> ModelWiseTrendData { get; set; }
        public ModelWiseTrendList()
        {
            ModelWiseTrendData = new List<ModelWiseTrendReport>();
        }
    }

    public class InputParam
    {
        public Int64 BranchId { get; set; }
        public Int64 ProductCategoryId { get; set; }
        public Int64 UserId { get; set; }
        public List<Int64> ProductSubCategoryId { get; set; }
        public Int64 ChannelId { get; set; }
        public InputParam()
        {
            ProductSubCategoryId = new List<Int64>();
        }
    }
}
