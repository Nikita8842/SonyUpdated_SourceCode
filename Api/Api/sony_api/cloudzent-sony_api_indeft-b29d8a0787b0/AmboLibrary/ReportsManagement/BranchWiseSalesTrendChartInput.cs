using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class BranchWiseSalesTrendChart
    {
        public string SaleDate { get; set; }
        public Int64 SalesCount { get; set; }
    }
    public class BranchWiseSalesChartData
    {
        public List<BranchWiseSalesTrendChart> BranchWiseTrendData { get; set; }
        public BranchWiseSalesChartData()
        {
            BranchWiseTrendData = new List<BranchWiseSalesTrendChart>();
        }
    }

    public class BranchWiseSalesChartInput
    {
        public Int64 BranchId { get; set; }
    }
}
