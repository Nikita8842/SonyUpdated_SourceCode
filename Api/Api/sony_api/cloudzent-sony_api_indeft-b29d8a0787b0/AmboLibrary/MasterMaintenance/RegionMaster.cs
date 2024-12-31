using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class RegionMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
        public string RegionName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }

    }

    public class RegionGridData : RegionMaster
    {
        public string Status { get; set; }
    }
}
