using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class LocationMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }
        public string LocationName { get; set; }
        public Int64 CityId { get; set; }
        public bool IsActive { get; set; }
    }

    public class LocationData
    {
        public Int64 ID { get; set; }
        public string LocationName { get; set; }
    }

    public class LocationGridData : LocationMaster
    {
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string RegionName { get; set; }
        public string Status { get; set; }
        public Int64 StateId { get; set; }
        public Int64 RegionId { get; set; }
    }

    public class CreateLocationForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Location name cannot be empty.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Location name can have minimum 3 and maximum 150 characters.")]
        public string LocationName { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "City for this location is not selected.")]
        public Int64 CityId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Select Status.")]
        public bool IsActive { get; set; }
    }

    public class UpdateLocationForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "ID not sent for updating this record.")]
        public Int64 ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Location name cannot be empty.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Location name can have minimum 3 and maximum 150 characters.")]
        public string LocationName { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "City for this location is not selected.")]
        public Int64 CityId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Select Status.")]
        public bool IsActive { get; set; }
    }

    public class DeleteLocationForm : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "ID not sent for deleting this record.")]
        public Int64 ID { get; set; }
    }
}
