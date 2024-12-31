using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class AssetMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }


        public Int64 Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
    }

    public class AssetGridData : AssetMaster
    {
        public string ActiveStatus { get; set; }
    }
}
