using System;
using AmboLibrary.Abstract;

namespace AmboLibrary.MasterMaintenance
{
    public class DivisionMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 Id { get; set; }
        public string IdValue { get; set; } //this field will use only fro product category division dropdown
        public string DivisionName { get; set; }
        public bool IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
}
