using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.ReportsManagement
{
    public class MonthPlanvsActualReportModel
    {
        public string Product { get; set; }
        public int Plan_Data { get; set; }
        public int Actual_Data { get; set; }
    }
    public class Plan_vs_Actual_Graph
    {
        public List<MonthPlanvsActualReportModel> Report { get; set; }
        public Plan_vs_Actual_Graph()
        {
            Report = new List<MonthPlanvsActualReportModel>();
        }
    }
}
