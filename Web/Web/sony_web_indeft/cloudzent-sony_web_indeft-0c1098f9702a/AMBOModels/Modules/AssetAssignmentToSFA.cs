using AMBOModels.Abstract;
using System;
using System.Data;

namespace AMBOModels.Modules
{
    public class AssetAssignmentToSFA : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public DataTable assetAssignmentToSFAdt { get; set; }

    }

    public class AssetIssuedToSFAGet :MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public string MaterialName { get; set; }
    }

    public class AssetIssuedToSFAGrid 
    {
        public Int64 MaterialId { get; set; }
        public string MaterialCode { get; set; }
        public string ProductName { get; set; }
        public int IssuedQuantity { get; set; }
        public string IssuedDate { get; set; }
        public Int64 SFAId { get; set; }
        public string SFAName { get; set; }
        public string SFACode { get; set; }
        public string Remarks { get; set; }
    }

    public class AssetsDropDownData
    {
        public Int64 Id { get; set; }
        public string MaterialName { get; set; }
    }
}
