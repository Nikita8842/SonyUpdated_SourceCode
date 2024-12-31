using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.Modules
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
        [Required(ErrorMessage = "Select Dealer(s)")]
        public int[] DealerIds { get; set; }
        [Required(ErrorMessage = "New MasterCode is required")]
        [MaxLength(20, ErrorMessage = "Length cannot be more than 20 characters")]
        public string NewMasterCode { get; set; }
        
        public List<Dealerdetails> DealerDet { get; set; }
    }
}
