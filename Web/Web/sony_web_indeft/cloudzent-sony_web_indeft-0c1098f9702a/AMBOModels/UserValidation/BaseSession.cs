using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.UserValidation
{
    public class BaseSession
    {
        public Int64 UserId { get; set; }
        public string UserName { get; set; }
        public Int64 RoleId { get; set; }
        public string RoleName { get; set; }
        public string EncryptKey { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}
