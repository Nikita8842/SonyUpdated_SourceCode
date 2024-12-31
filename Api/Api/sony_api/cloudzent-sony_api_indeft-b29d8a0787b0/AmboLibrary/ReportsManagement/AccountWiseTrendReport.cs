using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class AccountWiseTrendReport
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
    public class AccountWiseTrendList
    {
        public string CurrentMonth { get; set; }
        public string LastMonth { get; set; }
        public string SecondLastMonth { get; set; }
        public string ThirdLastMonth { get; set; }
        public string PreYrCurrentMonth { get; set; }
        public string PreYrLastMonth { get; set; }
        public string PreYrSecondLastMonth { get; set; }
        public string PreYrThirdLastMonth { get; set; }
        public List<AccountWiseTrendReport> AccountWiseTrendData { get; set; }
        public AccountWiseTrendList()
        {
            AccountWiseTrendData = new List<AccountWiseTrendReport>();
        }
    }
    public class AccountWiseTrendInput
    {
        public Int64 MasterCode { get; set; }
    }
}
