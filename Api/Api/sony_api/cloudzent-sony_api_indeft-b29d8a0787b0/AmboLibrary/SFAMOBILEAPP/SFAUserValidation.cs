using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.SFAMobileApp
{
    public class SFAUserValidation
    {
        public Int64 SFAId { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string SFALevel { get; set; }
        public String DOJ { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string TimeIn { get; set; }
        public string Dealer { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string GPSLocation { get; set; }
        public string AssetIssued { get; set; }
        public Int64 CurrentAppVersion { get; set; }
        public string URL { get; set; }
    }
    public class SFAValidationInput
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IMEI { get; set; }
        public string EncryptKey { get; set; }
        public DateTime SessionTime { get; set; }
        public string FCMId { get; set; }
    }
    public class UserUpdatePasswordModel
    {
        public Int64 UserId { get; set; }
        public string Password { get; set; }
    }
}
