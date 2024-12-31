using AMBOModels.Abstract;
using AMBOModels.MasterMaintenance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.Reports
{
    public class CompetitionSFACountReportFilters : ReportsGrid
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public Int64? BranchId { get; set; }
        public Int64? StateId { get; set; }
        public Int64? SFAId { get; set; }
        public Int64? ClassificationId { get; set; }
        public Int64? DivisionId { get; set; }
        public List<Int64> DealerIds { get; set; }
    }

    public class DownloadCompetitionSFACountReportData
    {
        public DataTable ExcelData { get; set; }
    }

    public class CompetitionSFACountReportData
    {
        public string BranchName { get; set; }
        public string DealerName { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string Channel { get; set; }
        public string DealerState { get; set; }
        public string MasterCode { get; set; }
        public string DealerCode { get; set; }
        public string DealerClass { get; set; }
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string SFALevel { get; set; }
        public string IncentiveCategory { get; set; }
        public string Division { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public Int64 Count { get; set; }
    }
}
