using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class StateMaster : MasterAbstract
    {
        #region Abstract Overrides
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        #endregion Abstract Overrides

        #region Data Members
        public virtual Int64 ID { get; set; }
        public virtual string StateName { get; set; }
        public virtual bool isActive { get; set; }
        public virtual Int64 RegionID { get; set; }
        public virtual string RegionName { get; set; }
        #endregion Data Members
    }

    public class StateGridData : StateMaster
    {
        public string RegionName { get; set; }
        public string Status { get; set; }
    }

    public class CreateStateForm : MasterAbstract
    {
        #region Abstract Overrides
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        #endregion Abstract Overrides

        [Required(AllowEmptyStrings = false, ErrorMessage = "State name cannot be empty.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "State name can have minimum 3 and maximum 150 characters.")]
        public string StateName { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "Region for this state is not selected.")]
        public Int64 RegionID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Select Status.")]
        public bool IsActive { get; set; }
    }

    public class UpdateStateForm : MasterAbstract
    {
        #region Abstract Overrides
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        #endregion Abstract Overrides

        [Range(1, Int64.MaxValue, ErrorMessage = "ID not sent for deleting this record.")]
        public Int64 ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "State name cannot be empty.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "State name can have minimum 3 and maximum 150 characters.")]
        public string StateName { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "Region for this state is not selected.")]
        public Int64 RegionID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Select Status.")]
        public bool IsActive { get; set; }
    }

    public class DeleteStateForm : MasterAbstract
    {
        #region Abstract Overrides
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        #endregion Abstract Overrides

        [Range(1, Int64.MaxValue, ErrorMessage = "ID not sent for deleting this record.")]
        public Int64 ID { get; set; }
    }
}
