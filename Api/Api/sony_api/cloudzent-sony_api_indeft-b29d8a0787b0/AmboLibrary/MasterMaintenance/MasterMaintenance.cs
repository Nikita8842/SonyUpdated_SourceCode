using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Abstract;

namespace AmboLibrary.MasterMaintenance
{
    public class MasterMaintenance
    {
        public Int64 BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }
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
        { List = new List<MasterMaintenance>(); }
    }

    public class CreateBranchForm : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateBranchForm : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64 ID { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }
    }

    public class DeleteBranchForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

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
