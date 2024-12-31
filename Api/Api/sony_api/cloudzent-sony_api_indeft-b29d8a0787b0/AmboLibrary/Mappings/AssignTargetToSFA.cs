using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Abstract;
using System.Data;

namespace AmboLibrary.Mappings
{

    public class AssignTargetToSFA : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 Id { get; set; }
    }

    public class AssignTargetToSFAGrid : AssignTargetToSFA
    {
        //public string TargetDate { get; set; }//
        public string Branch { get; set; }
        public string Month { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string DealerCode { get; set; }
        public string MasterCode { get; set; }
        public string DealerName { get; set; }
        public string SFACategory { get; set; }
        public string IncentiveCategory { get; set; }
        public string TargetCategory { get; set; }
        public string ProductCategory { get; set; }
        public string QtyTarget { get; set; }
        public string ValueTarget { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }

    public class AssignTargetToSFAGet : AssignTargetToSFA
    {
        public string TargetDate { get; set; }//
        public Int64 BranchId { get; set; }//
        public string Branch { get; set; }
    }

    public class AssignTargetToSFAUpload : DataTableAbstract
    {
        public override DataTable dtAsset { get; set; }
        public string TargetDate { get; set; }//
    }

    public class AssignFestivalTargetGrid
    {
        public string FestivalScheme { get; set; }
        public string Branch { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string DealerCode { get; set; }
        public string MasterCode { get; set; }
        public string DealerName { get; set; }
        public string SFACategory { get; set; }
        public string FestivalIncentiveCategory { get; set; }
        public string TargetCategory { get; set; }
        public string ProductCategory { get; set; }
        public string QtyTarget { get; set; }
        public string ValueTarget { get; set; }
    }

    public class AssignFestivalTargetGet
    {
        public Int64 FestivalSchemeId { get; set; }
        public Int64 UserId { get; set; }
    }

    public class AssignFestivalTargetUpload : DataTableAbstract
    {
        public override DataTable dtAsset { get; set; }
        public override Int64 UserId { get; set; }
        public Int64 FestivalSchemeId { get; set; }//
    }

}
