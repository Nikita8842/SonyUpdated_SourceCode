using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.SFAMobileApp
{
    #region Common
    public class CTInput
    {
        public Int64 Id { get; set; }
        public Int64 ProductCategoryId { get; set; }
    }
    #endregion

    #region Competitor Sale Thru
    public class SFACompetitionTrackingModel
    {
        public Int64 SalesId { get; set; }
        public Int64 CompanyId { get; set; }
        public Int64 ProductId { get; set; }
        public Int64 DealerId { get; set; }
        public Int64 UserId { get; set; }
        public DateTime Date { get; set; }
        public List<TackingMaterial> TackingMaterial { get; set; }
        public SFACompetitionTrackingModel()
        {
            TackingMaterial = new List<TackingMaterial>();
        }
    }
    public class TackingMaterial
    {
        public string MaterialName { get; set; }
        public Int64 Quantity { get; set; }
    }

    public class SFACompetitionTracking
    {
        public Int64 SalesId { get; set; }
        public string Company { get; set; }
        public string ProductCategory { get; set; }
        public Int64 CompanyId { get; set; }
        public Int64 SonyProductId { get; set; }
        public string MaterialName { get; set; }
        public string SonyProductCatName { get; set; }
        public string Quantity { get; set; }
        public Int64 Total { get; set; }
    }
    public class SFACompetitionTrackingList
    {
        public List<SFACompetitionTracking> SFACompetitionTracking { get; set; }
        public SFACompetitionTrackingList()
        {
            SFACompetitionTracking = new List<SFACompetitionTracking>();
        }
    }
    
    public class MonthlyModelWiseCompSalesThru
    {
        public Int64 Id { get; set; }
        public string Product { get; set; }
        public string Model { get; set; }
        public string Quantity { get; set; }
    }
    public class MonthlyModelWiseCompSalesThruList
    {
        public List<MonthlyModelWiseCompSalesThru> MonthlyModelWiseCompSalesThru { get; set; }
        public MonthlyModelWiseCompSalesThruList()
        {
            MonthlyModelWiseCompSalesThru = new List<MonthlyModelWiseCompSalesThru>();
        }
    }

    public class CounterShareTracking
    {
        public Int64 Id { get; set; }
        public string Brand { get; set; }
        public string LastMonthSale { get; set; }
        public string CurrentMonthSale { get; set; }
        public string CurrentMonthShare { get; set; }
    }
    public class CounterShareTrackingReport
    {
        public List<CounterShareTracking> CounterShareTracking { get; set; }
        public CounterShareTrackingReport()
        {
            CounterShareTracking = new List<CounterShareTracking>();
        }
    }
    #endregion

    #region Competitor Display SKU
    public class CompetitorDisplaySKU
    {
        public Int64 Id { get; set; }
        public Int64 CompetitorCompanyId { get; set; }
        public Int64 ProductCategoryId { get; set; }
        public Int64 LoginId { get; set; }
        public List<CompetitorDisplaySKUModels> CompetitorDisplaySKUModels { get; set; }
        public CompetitorDisplaySKU()
        {
            CompetitorDisplaySKUModels = new List<CompetitorDisplaySKUModels>();
        }
    }
    public class CompetitorDisplaySKUModels
    {
        public Int64 ModelId { get; set; }
        public Int64 Quantity { get; set; }
    }

    public class CompetitorDisplaySKUReport
    {
        public string CompetitionCode { get; set; }
        public string CompetitionProductCategory { get; set; }
        public string Model { get; set; }
        public Int64 Quantity { get; set; }
    }
    public class CompetitorDisplaySKUReportList
    {
        public List<CompetitorDisplaySKUReport> CompetitorDisplaySKUReport { get; set; }
        public CompetitorDisplaySKUReportList()
        {
            CompetitorDisplaySKUReport = new List<CompetitorDisplaySKUReport>();
        }
    }
    #endregion

    #region Competition Headcount Report
    public class CompetitionHeadCountModel
    {
        public Int64 SFAId { get; set; }
        public Int64 CompanyId { get; set; }
        public List<CompetitionSFACount> CompetitionSFACount { get; set; }
        public CompetitionHeadCountModel()
        {
            CompetitionSFACount = new List<CompetitionSFACount>();
        }
    }
    public class CompetitionSFACount
    {
        public Int64 TypeId { get; set; }
        public Int64 SFACount { get; set; }
    }
    public class CompetitionHeadCount
    {
        public string Company { get; set; }
        public string CountType { get; set; }
        public string HeadCount { get; set; }
    }
    public class CompetitorHeadCountReport
    {
        public List<CompetitionHeadCount> CompetitionHeadCount { get; set; }
        public CompetitorHeadCountReport()
        {
            CompetitionHeadCount = new List<CompetitionHeadCount>();
        }
    }
    #endregion
}
