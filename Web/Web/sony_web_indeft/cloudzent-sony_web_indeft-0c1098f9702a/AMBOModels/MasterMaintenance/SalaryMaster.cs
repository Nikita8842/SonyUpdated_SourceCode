using AMBOModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.MasterMaintenance
{
    public class SalaryMaster : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64? Id { get; set; }

        public Int64 LoginId { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(10, ErrorMessage = "Length cannot be more than 10 digits")]
        [Range(0.0, float.MaxValue, ErrorMessage = "Invalid Basic value")]               
        public string Basic { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(10, ErrorMessage = "Length cannot be more than 10 digits")]
        [Range(0.0, float.MaxValue, ErrorMessage = "Invalid HRA value")]        
        public string HRA { get; set; }

        [Required(ErrorMessage = "Required")]
        
        [MaxLength(10, ErrorMessage = "Length cannot be more than 10 digits")]
        [Range(0.0, float.MaxValue, ErrorMessage = "Invalid Medical value")]
        public string Med { get; set; }

        [Required(ErrorMessage = "Required")]        
        [MaxLength(10, ErrorMessage = "Length cannot be more than 10 digits")]
        [Range(0.0, float.MaxValue, ErrorMessage = "Invalid Conv value")]
        public string Conv { get; set; }

        [Required(ErrorMessage = "Required")]        
        [MaxLength(10, ErrorMessage = "Length cannot be more than 10 digits")]
        [Range(0.0, float.MaxValue, ErrorMessage = "Invalid Airtime value")]
        public string Airtime { get; set; }

        [Required(ErrorMessage = "Required")]
        [Range(0.0, float.MaxValue, ErrorMessage = "Other cannot be less than 0")]
        [MaxLength(10, ErrorMessage = "Invalid value")]
        public string Other { get; set; }

        [Required(ErrorMessage = "Required")]        
        [MaxLength(10, ErrorMessage = "Length cannot be more than 10 digits")]
        [Range(0.0, float.MaxValue, ErrorMessage = "Invalid Insurance value")]
        public string Insurance { get; set; }

        public string OtherAllowance1 { get; set; }

        public string OtherAllowance2 { get; set; }

        public bool IsActive { get; set; }
        
    }

    public class SFASalaryMasterGrid : SalaryMaster
    {
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string SFALevel { get; set; }
        public string Region { get; set; }
        public string Branch { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Division { get; set; }
        public Int64 BranchId { get; set; }
        public Int64[] BranchIds { get; set; }
    }

    public class SFASalaryMasterDelete : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }

        public Int64 Id { get; set; }
    }

    public class SFASalaryMasterUpload
    {
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string Branch { get; set; }
        public string City { get; set; }
        public string Division { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }

    public class SFASalaryMasterDownload
    {
        public Int64[] BranchIds { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
