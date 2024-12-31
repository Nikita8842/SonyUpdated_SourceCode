using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMBOWeb.Models
{
    public class ModelDashboard
    {
        public int RegisteredUserCount { get; set; }

        internal void InitDataUsingUserID(int id)
        {
            RegisteredUserCount = 0;
        }
    }
}