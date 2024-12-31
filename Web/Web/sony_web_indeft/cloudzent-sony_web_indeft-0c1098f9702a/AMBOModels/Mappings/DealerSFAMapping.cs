using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.Mappings
{
    public class DealerSFAMapping : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 ID { get; set; }
        [Required]
        public Int64 EmployeeId { get; set; }
        [Required(ErrorMessage ="Please select Dealer")]
        public Int64 DealerId { get; set; }
        public Int64 StateId { get; set; }
        public Int64 CityId { get; set; }
        public Int64 LocationId { get; set; }
        public Int64 BranchId { get; set; }
        public bool IsNavigated { get; set; }

    }

    public class DealerSFAMappingGridData : DealerSFAMapping
    {
        public string SFAName { get; set; }
        public string BranchName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string LocationName { get; set; }
        public string DealerName { get; set; }
        public string LastModificationDate { get; set; }
    }

    public class NavigationDealerSFAMapping : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 ID { get; set; }
        public Int64 EmployeeId { get; set; }
        [Required(ErrorMessage = "Please select Dealer")]
        public Int64 DealerId { get; set; }
        public Int64 BranchId { get; set; }
        public string DealerName { get; set; }

    }
}
