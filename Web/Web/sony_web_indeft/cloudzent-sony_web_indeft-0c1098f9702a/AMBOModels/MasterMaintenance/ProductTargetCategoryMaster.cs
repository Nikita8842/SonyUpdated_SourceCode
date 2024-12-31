using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class ProductTargetCategoryMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 Id { get; set; }
        [Required(ErrorMessage = "Product category is required")]        
        public Int64 ProductCategoryId { get; set; }
        [Required(ErrorMessage = "Target category is required")]
        [MaxLength(200, ErrorMessage = "Target category name length should not be more than 200 characters")]
        public string TargetCategory { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
    }

    public class ProductTargetCategoryGridData : ProductTargetCategoryMaster
    {
        public string ProductCategory { get; set; }
        public string ActiveStatus { get; set; }
    }

    public class ProductTargetCategoryGridFilters : ModelGrid
    {
        public List<Int64> ProductCategoryIds { get; set; }
        public string TargetCategory { get; set; }
    }

    public class ProductTargetCategoryData
    {
        public Int64 TargetCategoryId { get; set; }
        public string TargetCategoryName { get; set; }
    }

    public class ProductTargetCategorySearch : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public List<Int64> ProductCategoryIds { get; set; }
    }
}
