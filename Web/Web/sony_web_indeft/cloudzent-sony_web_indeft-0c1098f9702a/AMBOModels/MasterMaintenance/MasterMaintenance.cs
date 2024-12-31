using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMBOModels.Abstract;
using System.ComponentModel.DataAnnotations;

namespace AMBOModels.MasterMaintenance
{
    public class MasterMaintenance
    {
        public Int64 BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }

    /// <summary>
    /// Maintain Master 
    /// </summary>
    public class BranchMaster : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int32? BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
    }

    public class BranchGridData : BranchMaster
    {
        public Int64 ID { get; set; }
        public string Status { get; set; }
    }

    /// <summary>
    /// Class to get all Branch details
    /// </summary>
    public class GetBranchDetail
    {
        public List<MasterMaintenance> List { get; set; }
       public GetBranchDetail()
        {List = new List<MasterMaintenance>();}
    }

    public class CreateBranchForm : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Branch code cannot be empty.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Branch code can have minimum 3 and maximum 50 characters.")]
        public string BranchCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Branch name cannot be empty.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Branch name can have minimum 3 and maximum 150 characters.")]
        public string BranchName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Select Status.")]
        public bool IsActive { get; set; }
    }

    public class UpdateBranchForm : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "ID not sent for updating this record.")]
        public Int64 ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Branch code cannot be empty.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Branch code can have minimum 3 and maximum 50 characters.")]
        public string BranchCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Branch name cannot be empty.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Branch name can have minimum 3 and maximum 150 characters.")]
        public string BranchName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Select Status.")]
        public bool IsActive { get; set; }
    }

    public class DeleteBranchForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "ID not sent for deleting this record.")]
        public Int64 ID { get; set; }
    }

    /// <summary>
    /// To Delete Respective Branch
    /// </summary>
    public class DeleteBranch : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int32? BranchId { get; set; }
        public string BranchCode { get; set; }
    }
}
