using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class ProductCategoryMaster : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64 ID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Division { get; set; }
        public string DivisionId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Required")]
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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select division.")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "Invalid division selected.")]
        public string Division { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Product category name cannot be empty.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Product category name can have minimum 3 and maximum 150 characters.")]
        public string CategoryName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Product category description cannot be empty.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Product category description can have minimum 3 and maximum 200 characters.")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Select Status.")]
        public bool Status { get; set; }
    }

    public class UpdateProductCategoryForm : CreateProductCategoryForm
    {
        [Range(1, Int64.MaxValue, ErrorMessage = "ID not sent for updating this record.")]
        public Int64 ID { get; set; }
    }

    public class DeleteProductCategoryForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "ID not sent for deleting this record.")]
        public Int64 ID { get; set; }
    }

    public class ProductCategoryGroupDD
    {
        public Int64 ID { get; set; }
        public string CategoryName { get; set; }
        public Int64 ProductId { get; set; }
    }
}
