using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MasterMaintenance
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

        public string StateName { get; set; }
        public Int64 RegionID { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateStateForm : MasterAbstract
    {
        #region Abstract Overrides
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        #endregion Abstract Overrides

        public Int64 ID { get; set; }
        public string StateName { get; set; }
        public Int64 RegionID { get; set; }
        public bool IsActive { get; set; }
    }

    public class DeleteStateForm : MasterAbstract
    {
        #region Abstract Overrides
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        #endregion Abstract Overrides

        public Int64 ID { get; set; }
    }
}
