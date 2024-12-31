using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.MPRIntegration
{
    public class GetSFADetailsBySFACodeInput
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string SFACode { get; set; }
    }

    public class GetSFADetailsBySFACodeOutput
    {
        public string SFA { get; set; }
        public string Dealer { get; set; }
        public string Branch { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string CityType { get; set; }
        public string Division { get; set; }
        public string SFAStatus { get; set; }
    }
}
