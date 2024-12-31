using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class ProductSubCategoryMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public Int64 ProductCategoryId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }

    }

    public class ProductSubCategoryGridData : ProductSubCategoryMaster
    {
        public string CategoryName { get; set; }
        public string Division { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class CreateProductSubCategoryForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Product sub category name cannot be empty.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Product sub category name can have minimum 3 and maximum 150 characters.")]
        public string SubCategoryName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Product sub category description cannot be empty.")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Product sub category description can have minimum 5 and maximum 500 characters.")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Select Status.")]
        public bool Status { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "Product Category is not selected.")]
        public Int64 ProductCategoryId { get; set; }
    }

    public class UpdateProductSubCategoryForm : CreateProductSubCategoryForm
    {
        [Range(1, Int64.MaxValue, ErrorMessage = "ID not sent for updating this record.")]
        public Int64 ID { get; set; }
    }

    public class DeleteProductSubCategoryForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "ID not sent for deleting this record.")]
        public Int64 ID { get; set; }
    }
}
