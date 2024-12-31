using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{

    public class RegionMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public virtual Int64 ID { get; set; }
        public virtual string RegionName { get; set; }
        public virtual bool IsActive { get; set; }
    }

    public class RegionGridData : RegionMaster
    {
        public string Status { get; set; }
    }

    public class CreateRegionForm : RegionMaster
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Region name cannot be empty.")]
        [StringLength(150, MinimumLength = 4, ErrorMessage = "Region name can have minimum 4 and maximum 150 characters.")]
        public override string RegionName { get; set; }
    }

    public class UpdateRegionForm : RegionMaster
    {
        [Range(1,Int64.MaxValue,ErrorMessage = "ID not sent for updating this record.")]
        public override Int64 ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Region name cannot be empty.")]
        [StringLength(150, MinimumLength = 4, ErrorMessage = "Region name can have minimum 4 and maximum 150 characters.")]
        public override string RegionName { get; set; }
    }

    public class DeleteRegionForm : RegionMaster
    {
        [Range(1, Int64.MaxValue, ErrorMessage = "ID not sent for deleting this record.")]
        public override Int64 ID { get; set; }
    }
}
