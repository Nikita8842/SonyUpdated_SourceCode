using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.Abstract;
using System.Data;
using AMBOModels.ABSTRACT;

namespace AMBOModels.Modules
{
    public class AssetCollectionFromSFA : DataTableAbstract
    {
        //public override string EncryptKey { get; set; }
        //public override string RoleName { get; set; }
        //public override Int64 UserId { get; set; }

        public override DataTable dtAsset { get; set; }
    }

    public class AssetCollectionFromSFAGet : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }
        public String [] SFACode { get; set; }
    }

    public class AssetCollectionFromSFAData
    {
        public string SFACode { get; set; }
        public Int32 SFAId { get; set; }
        public string SFAName { get; set; }
        public string MaterialCode { get; set; }
        public string ProductName { get; set; }
        public Int32 IssuedQuantity { get; set; }
        public Int32 ReturnQuantity { get; set; }
        public string IssuedDate { get; set; }
        public string Remarks { get; set; }

    }
}
