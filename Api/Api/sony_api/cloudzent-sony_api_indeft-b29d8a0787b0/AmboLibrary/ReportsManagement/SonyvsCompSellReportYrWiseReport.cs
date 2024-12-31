using System;
using System.Collections.Generic;

namespace AmboLibrary.ReportsManagement
{
    public class SonyVsCompSellReportYrWise
    {
        public string CurrentMonth { get; set; }
        public string LastMonth { get; set; }
        public string SecondLastMonth { get; set; }
        public string ThirdLastMonth { get; set; }
        public string PreYrCurrentMonth { get; set; }
        public string PreYrLastMonth { get; set; }
        public string PreYrSecondLastMonth { get; set; }
        public string PreYrThirdLastMonth { get; set; }
        public List<SonyVsCompSellReportYrWiseData> SonyVsCompSellReportYrWiseList { get; set; }
        public SonyVsCompSellReportYrWise ()
        {
            SonyVsCompSellReportYrWiseList = new List<SonyVsCompSellReportYrWiseData>();
        }
    }
    public class SonyVsCompSellReportYrWiseData
    {
        public string Brand { get; set; }
        public Int64 C_Month { get; set; }
        public Int64 C_1_Month { get; set; }
        public Int64 C_2_Month { get; set; }
        public Int64 C_3_Month { get; set; }
        public Int64 Last_C_Month { get; set; }
        public Int64 Last_C_1_Month { get; set; }
        public Int64 Last_C_2_Month { get; set; }
        public Int64 Last_C_3_Month { get; set; }
        public Decimal Share_C_Month { get; set; }
        public Decimal Share_C_1_Month { get; set; }
        public Decimal Share_C_2_Month { get; set; }
        public Decimal Share_C_3_Month { get; set; }
        public Decimal Last_Share_c_Month { get; set; }
        public Decimal Last_Share_c_1_Month { get; set; }
        public Decimal Last_Share_c_2_Month { get; set; }
        public Decimal Last_Share_c_3_Month { get; set; }
    }
}
