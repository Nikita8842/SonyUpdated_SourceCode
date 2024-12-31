using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class DailyRangingChartReportModel
    {
        public string Day { get; set; }
        public string Quantity { get; set; }
        public string PlannedQuantity { get; set; }
    }
    public class DailyRangingChartReportData
    {
        public List<DailyRangingChartReportModel> DailyRangingChartReport { get; set; }
        public DailyRangingChartReportData()
        {
            DailyRangingChartReport = new List<DailyRangingChartReportModel>();
        }
    }
    public class DailyRangingChartReportInputModel
    {
        public Int64 UserId { get; set; }
        public int BranchId { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public int ModelId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
