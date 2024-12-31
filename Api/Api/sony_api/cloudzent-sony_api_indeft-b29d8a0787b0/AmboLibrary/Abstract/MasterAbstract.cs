using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.Abstract
{
    public abstract class MasterAbstract
    {
        public abstract Int64 UserId { get; set; }
        //public virtual Int64 RoleId { get; set; }
        public abstract string EncryptKey { get; set; }
        public abstract string RoleName { get; set; }
    }
}
