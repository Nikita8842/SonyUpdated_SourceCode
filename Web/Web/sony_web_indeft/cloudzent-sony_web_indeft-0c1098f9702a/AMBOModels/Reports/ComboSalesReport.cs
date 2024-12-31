using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.Reports
{
    public class ComboSalesReport : ReportsGrid
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public Int64? BranchId { get; set; }
        public Int64? StateId { get; set; }
        public Int64? SFAId { get; set; }
        public Int64? ProductCategoryId { get; set; }
        public Int64? ProSubCatId { get; set; }
        public Int64? ClassificationId { get; set; }
        public Int64? DivisionId { get; set; }
        public List<Int64> DealerIds { get; set; }
        //public List<Int64> CompanyIds { get; set; }
    }

    public class ComboSalesReportGrid
    {
        public Int64 DailySalesId { get; set; }
        public string Branch { get; set; }
        public string Date { get; set; }
        public string Month { get; set; }
        public string DealerName { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string PayerName { get; set; }
        public string Channel { get; set; }
        public string State { get; set; }
        public string SAPCode { get; set; }
        public string DealerCode { get; set; }
        public string DealerClassification { get; set; }
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string SFALevel { get; set; }
        public string IncentiveCate { get; set; }
        public string CompanyName { get; set; }
        public string Division { get; set; }
        public string ProCat1 { get; set; }
        public string ComboMaterial1 { get; set; }
        public string SaleDate1 { get; set; }
        public string InvoiceNo1 { get; set; }
        public string InvoiceImage1 { get; set; }
        public string ProCat2 { get; set; }
        public string ComboMaterial2 { get; set; }
        public string SaleDate2 { get; set; }
        public string InvoiceNo2 { get; set; }
        public string InvoiceImage2 { get; set; }

    }
}
