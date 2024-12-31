using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.Mappings
{
    public class QSRQuantityReports : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 Id { get; set; }
    }

    public class QsrReportsGrid : QSRQuantityReports
    {
        //public string TargetDate { get; set; }//
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
        public string ProductCategory { get; set; }
        public string QSRDate { get; set; }
        public string Material { get; set; }
        public string PayerName { get; set; }
        public string DealerClassification { get; set; }
        public string CompanyName { get; set; }
        public string AmboQuantity { get; set; }
        public string QSRQuantity { get; set; }
        public string FinalQuantity { get; set; }
        // public string Status { get; set; }
        // public string Reason { get; set; }
    }
    public class QSRQuantityGet : QSRQuantityReports
    {
        public string TargetDate { get; set; }//
        public Int64 BranchId { get; set; }//
        public string Branch { get; set; }
    }

    public class QSRQuantityUpload : DataTableAbstract
    {
        public override DataTable dtAsset { get; set; }
        public string TargetDate { get; set; }//
    }

}
