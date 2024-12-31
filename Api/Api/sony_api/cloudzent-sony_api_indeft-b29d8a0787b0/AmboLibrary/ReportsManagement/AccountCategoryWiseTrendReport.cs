using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class AccountCategoryWiseTrendReport
    {
        public string CurrentMonth { get; set; }
        public string LastMonth { get; set; }
        public string SecondLastMonth { get; set; }
        public string ThirdLastMonth { get; set; }
        public List<AccountCategoryWiseTrendData> AccountCategoryWiseTrendData { get; set; }
        public AccountCategoryWiseTrendReport()
        {
            AccountCategoryWiseTrendData = new List<AccountCategoryWiseTrendData>();
        }
    }
    public class AccountCategoryWiseTrendData
    {
        public string Brand { get; set; }
        public Int64 CurrentMonthSale { get; set; }
        public Int64 LastMonthSale { get; set; }
        public Int64 SecondLastMonthSale { get; set; }
        public Int64 ThirdLastMonthSale { get; set; }
        public Decimal CurrentMonthSharePer { get; set; }
        public Decimal LastMonthSharePer { get; set; }
        public Decimal SecondLastMonthSharePer { get; set; }
        public Decimal ThirdLastMonthSharePer { get; set; }
    }
    public class AccountCategoryWiseTrendInput
    {
        public Int64 MasterCode { get; set; }
        public Int64 ProductId { get; set; }
        public List<Int64> SubProductId { get; set; }
        public AccountCategoryWiseTrendInput()
        {
            SubProductId = new List<Int64>();
        }
}
}
