using AmboLibrary.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmboLibrary.Modules
{
    public class Dealerdetails : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 Id { get; set; }
        public string DealerDetail { get; set; }
        public bool IsChecked { get; set; }
        public string MasterCode { get; set; }
    }

    public class DealerMasterCodeUpdate : Dealerdetails
    {
        public int[] DealerIds { get; set; }        
        public string NewMasterCode { get; set; }
        public List<Dealerdetails> DealerDet { get; set; }
    }
}
