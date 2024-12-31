using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
{
    public class ProductCategoryMaster : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64 ID { get; set; }
        public string Division { get; set; }
        public string DivisionId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }
    }

    public class ProductCategoryGridData : ProductCategoryMaster
    {
        public string DivisionName { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class CreateProductCategoryForm : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public string Division { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }

    public class UpdateProductCategoryForm : CreateProductCategoryForm
    {
        public Int64 ID { get; set; }
    }

    public class DeleteProductCategoryForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
    }

    public class ProductCategoryGroupDD
    {
        public Int64 ID { get; set; }
        public string CategoryName { get; set; }
        public Int64 ProductId { get; set; }
    }
}
