using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMBOWeb.Models
{
    public class ModelSession
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string EncryptKey { get; set; }
    }
}