using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class CompetitorMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 ID { get; set; }

        [Required(ErrorMessage ="Please enter Competitor Code.")]
        public string CompetitorCode { get; set; }

        [Required(ErrorMessage = "Please enter Competitor Name.")]
        public string CompetitorName { get; set; }

        public bool Discontinued { get; set; }

        [Required(ErrorMessage = "Please select Status.")]
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Int64 LastModifiedBy { get; set; }
    }
    public class CompetitorMasterData : CompetitorMaster
    {
        public string CompetitorStatus { get; set; }
    }

    public class BrandList
    {
        public Int64 BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
