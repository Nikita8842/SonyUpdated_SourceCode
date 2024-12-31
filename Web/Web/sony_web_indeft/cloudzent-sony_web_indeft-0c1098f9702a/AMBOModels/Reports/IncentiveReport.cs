using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.Reports
{
    public class IncentiveReport
    {
    }
    #region FestivalIncentive Report
    public class FestivalIncentiveReportInputParam
    {
        public Int64 BranchId { get; set; }
        public Int64 SFAId { get; set; }
        public Int64 DivisionId { get; set; }
        public Int64 FestivalSchemeId { get; set; }
        public Int64 ProductCategoryId { get; set; }
    }
    public class FestivalIncentiveDisplayReportList
    {
        public List<FestivalIncentiveDisplayReportData> FestivalIncentiveData { get; set; }
        public FestivalIncentiveDisplayReportList()
        {
            FestivalIncentiveData = new List<FestivalIncentiveDisplayReportData>();
        }
    }
    public class FestivalIncentiveDisplayReportData
    {
        public string SchemeName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string SFAType { get; set; }
        public string SFALevel { get; set; }
        public string SFADivision { get; set; }
        public string Dealer { get; set; }
        public string Channel { get; set; }
        public string DealerState { get; set; }
        public string MasterCode { get; set; }
        public string DealerCode { get; set; }
        public string IncentiveCategory { get; set; }
        //public int QtyPercentage { get; set; }
        //public int ValuePercentage { get; set; }
        public int FestivalIncentiveAmount { get; set; }
        public int ProposedDeviation { get; set; }
        public int ApprovedDeviationAmount { get; set; }
        public string Reasons { get; set; }
        public string FirstHeader { get; set; }
        public string FirstRemark { get; set; }
        public string SecondHeader { get; set; }
        public string SecondRemark { get; set; }
        public int FinalPayableAmount { get; set; }
        public string HORemark { get; set; }
        public string DeviationStage { get; set; }
    }

    ///
    /// for detail report of festival incentive report
    ///
    public class FestivalIncentiveDetailReportList
    {
        public List<FestivalIncentiveDetailReportData> FestivalIncentiveDetailData { get; set; }
        public FestivalIncentiveDetailReportList()
        {
            FestivalIncentiveDetailData = new List<FestivalIncentiveDetailReportData>();
        }
    }
    public class FestivalIncentiveDetailReportData
    {
        public string SchemeName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string SFAType { get; set; }
        public string SFALevel { get; set; }
        public string SFADivision { get; set; }
        public string Dealer { get; set; }
        public string Channel { get; set; }
        public string DealerState { get; set; }
        public string MasterCode { get; set; }
        public string DealerCode { get; set; }
        public string IncentiveCategory { get; set; }
        public string TargetCategory { get; set; }
        public string ProductCategory { get; set; }
        public int QtyTarget { get; set; }
        public int ValueTarget { get; set; }
        public int QtyAchievement { get; set; }
        public int ValueAchievement { get; set; }
        public int QtyPercentage { get; set; }
        public int ValuePercentage { get; set; }
        public int FestivalSlabCalculated { get; set; }
        public int ProposedDeviation { get; set; }
        public string Reasons { get; set; }
        public string FirstHeader { get; set; }
        public string FirstRemark { get; set; }
        public string SecondHeader { get; set; }
        public string SecondRemark { get; set; }
        public int FinalPayableAmount { get; set; }
        public string HORemark { get; set; }
        public string DeviationStage { get; set; }

        public int ApprovedDeviationAmount { get; set; }
        public int FestivalIncentiveAmount { get; set; }
    }
    #endregion
    public class BaseIncentiveReport
    {
        public int BranchId { get; set; }
        public int SFAId { get; set; }
        public int DivisionId { get; set; }
        public int ProdCatId { get; set; }
    }
    public class PerPieceIncentiveReport
    {
        public int BranchId { get; set; }
        public int SFAId { get; set; }
        public int DivisionId { get; set; }
        public int ProdCatId { get; set; }
    }
    public class FestivalIncentiveReport
    {
        public int BranchId { get; set; }
        public int SFAId { get; set; }
        public int DivisionId { get; set; }
        public Int64 SchemeId { get; set; }
        public int ProdCatId { get; set; }
    }

    public class BasePerPieceIncentiveReportInputParam
    {
        public Int64 BranchId { get; set; }
        public Int64 SFAId { get; set; }
        public Int64 DivisionId { get; set; }
        public string Month { get; set; }
        public Int64 ProductCategoryId { get; set; }
    }
    public class BasePerPieceIncentiveDisplayReportList
    {
        public List<BasePerPieceIncentiveDisplayReportData> BasePerPieceIncentiveData { get; set; }
        public BasePerPieceIncentiveDisplayReportList()
        {
            BasePerPieceIncentiveData = new List<BasePerPieceIncentiveDisplayReportData>();
        }
    }
    public class BasePerPieceIncentiveDisplayReportData
    {
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string SFAType { get; set; }
        public string SFALevel { get; set; }
        public string SFADivision { get; set; }
        public string Dealer { get; set; }
        public string Channel { get; set; }
        public string DealerState { get; set; }
        public string MasterCode { get; set; }
        public string DealerCode { get; set; }
        public string IncentiveCategory { get; set; }
        //public int QtyPercentage { get; set; }
        //public int ValuePercentage { get; set; }
        public int BaseIncentiveAmount { get; set; }
        public int PerPieceIncentiveAmount { get; set; }
        public int ProposedDeviation { get; set; }
        public int ApprovedDeviationAmount { get; set; }
        public string Reasons { get; set; }
        public string FirstHeader { get; set; }
        public string FirstRemark { get; set; }
        public string SecondHeader { get; set; }
        public string SecondRemark { get; set; }
        public int FinalPayableAmount { get; set; }
        public string HORemark { get; set; }
        public string DeviationStage { get; set; }
        
    }

    ///
    /// for detail report of base per piece incentive report
    ///
    public class BasePerPieceIncentiveDetailReportList
    {
        public List<BasePerPieceIncentiveDetailReportData> BasePerPieceIncentiveDetailData { get; set; }
        public BasePerPieceIncentiveDetailReportList()
        {
            BasePerPieceIncentiveDetailData = new List<BasePerPieceIncentiveDetailReportData>();
        }
    }
    public class BasePerPieceIncentiveDetailReportData
    {
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string SFAType { get; set; }
        public string SFALevel { get; set; }
        public string SFADivision { get; set; }
        public string Dealer { get; set; }
        public string Channel { get; set; }
        public string DealerState { get; set; }
        public string MasterCode { get; set; }
        public string DealerCode { get; set; }
        public string IncentiveCategory { get; set; }
        public string TargetCategory { get; set; }
        public string ProductCategory { get; set; }
        public int QtyTarget { get; set; }
        public int ValueTarget { get; set; }
        public int QtyAchievement { get; set; }
        public int ValueAchievement { get; set; }
        public int QtyPercentage { get; set; }
        public int ValuePercentage { get; set; }
        public int BaseSlabCalculated { get; set; }
        public int BaseIncentiveAmount { get; set; }
        public int PerPieceQty { get; set; }
        public int PerPieceSlabCalculated { get; set; }
        public string PerPieceIncentiveScheme { get; set; }
        public int PerPieceIncentiveAmount { get; set; }
        public int ProposedDeviation { get; set; }
        public int ApprovedDeviationAmount { get; set; }
        public string Reasons { get; set; }
        public string FirstHeader { get; set; }
        public string FirstRemark { get; set; }
        public string SecondHeader { get; set; }
        public string SecondRemark { get; set; }
        public int FinalPayableAmount { get; set; }
        public string HORemark { get; set; }
        public string DeviationStage { get; set; }
        public string QSRDate { get; set; }
        public string Material { get; set; }
        public string PayerName { get; set; }
        public string DealerClassification { get; set; }
        public string CompanyName { get; set; }
        public string AmboQuantity { get; set; }

    }
    public class BasePerPieceIncentiveReportInputParamQSR
    {
        public Int64 BranchId { get; set; }
        public Int64 SFAId { get; set; }
        public Int64 DivisionId { get; set; }
        public string Month { get; set; }
        public Int64 ProductCategoryId { get; set; }
    }

}
