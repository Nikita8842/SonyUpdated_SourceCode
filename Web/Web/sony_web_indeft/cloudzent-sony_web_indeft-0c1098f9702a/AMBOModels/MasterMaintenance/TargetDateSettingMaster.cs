using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class TargetDateSettingMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 Id { get; set; }
        public Int64 BranchId { get; set; }
        public string BranchName { get; set; }
        [Required(ErrorMessage = "Select at least one Branch")]
        public int[] BranchIds { get; set; }
        [Required(ErrorMessage = "Please enter target date")]
        public string TargetDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 LastModifiedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}
