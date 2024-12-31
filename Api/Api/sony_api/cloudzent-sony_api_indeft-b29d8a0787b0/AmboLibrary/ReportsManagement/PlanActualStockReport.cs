using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class PlanActualStockReport
    {
        public string ModelName { get; set; }
        public Int64 Plan_Qty { get; set; }
        public Int64 Display_Qty { get; set; }
        public Int64 Store_Back_Stock { get; set; }
    }

    public class PlanActualStockList
    {
        public List<PlanActualStockReport> PlanActualStockData { get; set; }
        public PlanActualStockList()
        {
            PlanActualStockData = new List<PlanActualStockReport>();
        }
    }

    public class PlanActualStockDataInput
    {
        public Int64 DealerId { get; set; }
        public Int64 ProductCatId { get; set; }
        public List<Int64> ProductSubCatId { get; set; }
        public PlanActualStockDataInput()
        {
            ProductSubCatId = new List<Int64>();
        }
    }
}
