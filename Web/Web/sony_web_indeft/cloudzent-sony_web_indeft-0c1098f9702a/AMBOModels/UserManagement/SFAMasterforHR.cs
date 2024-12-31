using AMBOModels.Abstract;
using AMBOModels.ABSTRACT;
using AMBOModels.GlobalAccessible;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.UserManagement
{
    public class SFAMasterforHR : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64? Id { get; set; }
        public Int64 SFATypeId { get; set; }
        public Int64 LoginId { get; set; }
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Source { get; set; }
        public string SourceName { get; set; }
        public string AgencyRef { get; set; }
        [Required(ErrorMessage = "Required")]
        public string SourceCode { get; set; }
        public string DOB { get; set; }
        public string DOJ { get; set; }
        [Required(ErrorMessage = "Required")]
        public string AssetIssued { get; set; }
        public string DOL { get; set; }
        public string SFAType { get; set; }
        [Required(ErrorMessage = "Required")]
        public string LevelofEducation { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Education { get; set; }
        [Required(ErrorMessage = "Required")]
        [MaxLength(5, ErrorMessage ="Length cannot be more than 5 digits")]
        public string Experience { get; set; }
        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public Int64 RegionId { get; set; }
        public Int64 BranchId { get; set; }
        public Int64 StateId { get; set; }
        public Int64 CityId { get; set; }
        public Int64 DivisionId { get; set; }
        public string Region { get; set; }
        public string Branch { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Division { get; set; }
        public string SFALevel { get; set; }
        public string ESIAccountNo { get; set; }
        [Required(ErrorMessage = "Required")]
        public string PFAccountNo { get; set; }
        public string MedicalInsuranceNo { get; set; }
        public string MICoverageFrom { get; set; }
        public string MICoverageTo { get; set; }
        public string PersonalInsuranceNo { get; set; }
        public string PICoverageFrom { get; set; }
        public string PICoverageTo { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string CityType { get; set; }
        public string Location { get; set; }
        public string DealerName { get; set; }
        public string DealerCode { get; set; }
        public string Channel { get; set; }
        public string Agency { get; set; }
        public string SFASubLevel { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public Int64[] BranchIds { get; set; }
    }

    public class SFAMasterforHRDelete : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64 Id { get; set; }
    }

    public class SFAMasterforHRUpload 
    {
        public string SFACode { get; set; }
        public string SFAName { get; set; }
        public string Branch { get; set; }
        public string City { get; set; }
        public string Division { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
    }

    public class SFAMasterforHRDownload
    {
        public Int64[] BranchIds { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class EncryptionInput
    {
        public string LoginId { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
    }
}
