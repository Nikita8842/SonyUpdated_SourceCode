using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
  public  class ProductCategoryGroupMaster: MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64 GroupId { get; set; }
        [Required(ErrorMessage = "Required")]
        [MaxLength(100, ErrorMessage = "Length should not be more than 100 characters")]
        public string GroupName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Required")]
        public int DisplayOrder { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Required")]
        public int[] ProductIds { get; set; }
        public string CategoryName { get; set; }
        // public string[] tempPId { get; set; }
    }
}
