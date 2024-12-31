using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class ProductDefinitionUnderTargetCategory : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 Id { get; set; }
        public Int64 ProductCategoryId { get; set; }
        public Int64 ProductSubCatId { get; set; }
        public Int64 MaterialId { get; set; }
        public Int64 TargetCategoryId { get; set; }
        public bool IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
    }
}
