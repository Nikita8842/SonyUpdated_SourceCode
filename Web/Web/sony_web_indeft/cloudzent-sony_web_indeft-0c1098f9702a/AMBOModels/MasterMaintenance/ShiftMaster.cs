using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class ShiftMaster: MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }


        public Int64? Id { get; set; }        
        [Required(ErrorMessage = "Shift name is required")]
        [MaxLength(50, ErrorMessage = "Length cannot be more than 50 characters")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Special characters not allowed in Shift name")]
        public string ShiftName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }
    }

    public class ShiftFilter
    {
        public Int64[] ShiftIds { get; set; }
    }
}
