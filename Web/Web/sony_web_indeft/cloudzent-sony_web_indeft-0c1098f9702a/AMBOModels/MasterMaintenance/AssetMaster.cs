using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class AssetMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }


        public Int64 Id { get; set; }
        [Required(ErrorMessage = "(Please enter Product Code)")]
        [MaxLength(200, ErrorMessage = "Length cannot be more than 200 characters")]
        public string ProductCode { get; set; }
        [Required(ErrorMessage = "(Please enter Product Name)")]
        [MaxLength(200, ErrorMessage = "Length cannot be more than 200 characters")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "(Please enter Category)")]
        [MaxLength(200, ErrorMessage = "Length cannot be more than 200 characters")]
        public string Category { get; set; }
        [Required(ErrorMessage = "(Please select Type)")]
        public string Type { get; set; }
        [Required(ErrorMessage = "(Please select status)")]
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
    }

    public class AssetGridData : AssetMaster
    {
        public string ActiveStatus { get; set; }
    }
}
