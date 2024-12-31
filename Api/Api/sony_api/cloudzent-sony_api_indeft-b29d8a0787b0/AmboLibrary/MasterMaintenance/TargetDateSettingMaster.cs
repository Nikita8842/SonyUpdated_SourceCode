using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class TargetDateSettingMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 Id { get; set; }
        public Int64 BranchId { get; set; }
        public int[] BranchIds { get; set; }
        public string BranchName { get; set; }
        public string TargetDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 LastModifiedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}
