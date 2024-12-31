using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.Reports
{
    public class DisplayReport : ReportsGrid
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public Int64? BranchId { get; set; }
        public Int64? StateId { get; set; }
        public Int64? ProductCategoryId { get; set; }
        public Int64? DealerId { get; set; }
        public Int64? SFATypeId { get; set; }
    }

    public class DisplayReportGrid
    {
        public string Branch { get; set; }
        public string Date { get; set; }
        public string DealerName { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string Channel { get; set; }
        public string State { get; set; }
        public string SAPCode { get; set; }
        public string PartyName { get; set; }
        public string DealerCode { get; set; }
        public string DealerClassification { get; set; }
        public string SonyMaterial { get; set; }
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string SFALevel { get; set; }
        public string IncentiveCate { get; set; }
        public string ProCat { get; set; }
        public string ProSubCat { get; set; }
        public string Division { get; set; }
        public string PlannedQuantity { get; set; }
        public string DisplayQuantity { get; set; }
        public string Gap { get; set; }
        public string SFAType { get; set; }
        public string FY { get; set; }                                             /* added 7_july_2024 vijay*/
        public string SubCatGroup { get; set; }
        public string Segment { get; set; }
        public string Resolution { get; set; }
        public string Internet { get; set; }
        public string S3D { get; set; }
        public string TVType { get; set; }
        //public string Attribute_1 { get; set; }                                    /* added 7_july_2024 vijay*/
        //public string Attribute_2 { get; set; }                                    /* added 7_july_2024 vijay*/
        //public string Attribute_3 { get; set; }                                    /* added 7_july_2024 vijay*/
        //public string Attribute_4 { get; set; }                                    /* added 7_july_2024 vijay*/
        //public string Attribute_5 { get; set; }                                    /* added 7_july_2024 vijay*/
        //public string Attribute_6 { get; set; }                                    /* added 7_july_2024 vijay*/

        public string Model { get; set; }
        public string Brand { get; set; }
        public string Quantity { get; set; }
        public string CompanyName { get; set; }
        public string QSRStatus { get; set; }
    }
}
