using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMBOWeb.Models
{
    public class ModelLogin
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserTypeID { get; set; }
        public string UserTypeName { get; set; }
        public string Message { get; set; }

        internal string Authenticate()
        {
            try
            {
                UserID = 1;
                Message = "User authenticated.";
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return "SUCCESS";
            }
        }

        internal string CreateNewUser()
        {
            return "SUCCESS";
        }
    }
}