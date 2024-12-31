using AMBOModels.Abstract;
using AMBOModels.MasterMaintenance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBOModels.UserManagement
{
    public class UserManagement
    {
    }

    public class SearchSFA : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64[] DivisionIds { get; set; }
        public Int64[] BranchIds { get; set; }
        public Int64[] RoleIds { get; set; }
    }

    public class SearchSFAOutput
    {
        public Int64 SFAUserId { get; set; }
        public string SFAName { get; set; }
    }

    public class SearchSID : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64[] BranchIds { get; set; }
        public Int64[] RoleIds { get; set; }
    }

    public class SearchSIDOutput
    {
        public Int64 SIDUserId { get; set; }
        public string SIDName { get; set; }
    }

    public class UserBasicProperties : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64? EmployeeId { get; set; }
        public virtual string EmployeeCode { get; set; }
        public virtual string LoginId { get; set; }
        public virtual string Password { get; set; }
        public virtual string ReTypePassword { get; set; }
        public virtual Int64 BranchId { get; set; }
        public virtual Int64 RegionId { get; set; }
        public virtual Int64 StateId { get; set; }
        public virtual Int64 CityId { get; set; }
        public virtual Int64 DivisionId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string UserFullName { get; set; }
        public virtual string Address { get; set; }
        public virtual string MobileNumber { get; set; }
        public virtual string AltMobileNumber { get; set; }
        public virtual string EmailId { get; set; }
        public virtual string PANNo { get; set; }
        public virtual string DOB { get; set; }
        public virtual string DOJ { get; set; }
        public virtual string DOL { get; set; }
        public virtual Int64 RoleId { get; set; }
        public virtual Int64 IncentiveCategory { get; set; }
        public virtual Int64 SFALevel { get; set; }
        public virtual Int64 AgencyId { get; set; }
        public virtual string IMEI1 { get; set; }
        public virtual string IMEI2 { get; set; }
        public virtual Int64 SFATypeId { get; set; }
        public virtual bool Status { get; set; }
        public bool isUpdate { get; set; }
        public bool isPasswordChange { get; set; }
        public string SubmitMessage { get; set; }
        public string OldPassword { get; set; }
        public string ShiftName { get; set; }
        public Int64 ShiftNameId { get; set; }
    }

    public class GetUserList
    {
        public IEnumerable<UserBasicProperties> List { get; set; }
        public Int64 TotalSize { get; set; }
    }

    public class EmployeeGridData : UserBasicProperties
    {
        public string BranchName { get; set; }
        public string Role { get; set; }
        public string StatusName { get; set; }
        public string RegionName { get; set; }
        public string Division { get; set; }
        public string SFAType { get; set; }
        public string SFALevelName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string AgencyName { get; set; }
    }

    public class SFAGridData : UserBasicProperties
    {
        public string BranchName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string AgencyName { get; set; }
        public string Role { get; set; }
        public string StatusName { get; set; }
        public string RegionName { get; set; }
        public string Division { get; set; }
        public string SFAType { get; set; }
        public string SFALevelName { get; set; }
        public string PrimaryCode { get; set; }
        public string MasterCode { get; set; }
        public string DealerCity { get; set; }
        public string DealerLocation { get; set; }
        public string Channel { get; set; }
        public string DealerName { get; set; }
        public string IncentiveCategoryName { get; set; }
    }

  

    public class SFAGridSearchFilters : ModelGrid
    {
        public List<Int64> BranchIds { get; set; }
        public List<Int64> StateIds { get; set; }
        public List<Int64> CityIds { get; set; }
        public List<Int64> AgencyIds { get; set; }
        public List<Int64> SFAType { get; set; }
        public string Status { get; set; }
        public string LoginId { get; set; }
        public string EmployeeName { get; set; }
    }

    public class EmployeeGridSearchFilters : ModelGrid
    {
        public List<Int64> BranchIds { get; set; }
        public List<Int64> StateIds { get; set; }
        public List<Int64> CityIds { get; set; }
        public string Status { get; set; }
        public string LoginId { get; set; }
        public string EmployeeName { get; set; }
    }

    public class EmployeeFormData : UserBasicProperties
    {
        [Required(ErrorMessage = "(Mandatory)")]
        [MaxLength(50, ErrorMessage ="Length cannot be more than 50 characters")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Special characters not allowed in EmployeeCode")]
        public override string EmployeeCode { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        [MaxLength(50, ErrorMessage = "Length cannot be more than 50 characters")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Special characters not allowed in LoginId")]
        public override string LoginId { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        [MinLength(5, ErrorMessage = "Length should be of at least 5 characters")]
        [MaxLength(200, ErrorMessage = "Length cannot be more than 200 characters")]
        public override string Password { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        [MaxLength(200, ErrorMessage = "Length cannot be more than 200 characters")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public override string ReTypePassword { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "(Mandatory)")]
        public override Int64 BranchId { get; set; }

        

        [Required(ErrorMessage = "(Mandatory)")]
        [MaxLength(50, ErrorMessage = "Length cannot be more than 50 characters")]
        //[RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Special characters not allowed in FirstName")]
        public override string FirstName { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        [MaxLength(50, ErrorMessage = "Length cannot be more than 50 characters")]
        //[RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Special characters not allowed in LastName")]
        public override string LastName { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        [StringLength(10, ErrorMessage = "(Should be 10 digits only)"), MinLength(10, ErrorMessage = "Mobile number can't be less than 10 digits")]
        [RegularExpression("^[0-9]+$", ErrorMessage ="Mobile number should contain only digits")]
        public override string MobileNumber { get; set; }

        [StringLength(10, ErrorMessage = "(Should be 10 digits only)"), MinLength(10, ErrorMessage = "Alternate mobile number can't be less than 10 digits")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Alternate Mobile number should contain only digits")]
        public override string AltMobileNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public override string EmailId { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "(Mandatory)")]
        public override Int64 RoleId { get; set; }

        //[StringLength(20, MinimumLength = 14, ErrorMessage = "(Size should be between 14 to 20 digits)")]
        //[MinLength(14, ErrorMessage = "(Can not be less than 14 characters)")]
        //[StringLength(20, ErrorMessage = "(Can not be more than 20 characters)")]
        public override string IMEI1 { get; set; }

        //[StringLength(20, MinimumLength = 14, ErrorMessage = "(Size should be between 14 to 20 digits)")]
        //[MinLength(14, ErrorMessage = "(Can not be less than 14 characters)")]
        //[StringLength(20, ErrorMessage = "(Can not be more than 20 characters)")]
        public override string IMEI2 { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        public override bool Status { get; set; }

        
    }

    public class SFAFormData : UserBasicProperties
    {
        [Required(ErrorMessage = "(Mandatory)")]
        [MaxLength(50, ErrorMessage = "Length cannot be more than 50 characters")]
        public override string EmployeeCode { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        [MaxLength(50, ErrorMessage = "Length cannot be more than 50 characters")]
        public override string LoginId { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        [MinLength(5, ErrorMessage = "Length should be of at least 5 characters")]
        [MaxLength(200, ErrorMessage = "Length cannot be more than 200 characters")]
        public override string Password { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        [MaxLength(200, ErrorMessage = "Length cannot be more than 200 characters")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public override string ReTypePassword { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "(Mandatory)")]
        public override Int64 BranchId { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "(Mandatory)")]
        public override Int64 RegionId { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "(Mandatory)")]
        public override Int64 StateId { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "(Mandatory)")]
        public override Int64 CityId { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        public override string Address { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        public override string FirstName { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        public override string LastName { get; set; }

        
        public string Gender { get; set; }

        
        public string FatherName { get; set; }

        public string SFAFullName { get; set; }

        public string RDIFullName { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        [StringLength(10, ErrorMessage = "(Should be 10 digits only)"), MinLength(10, ErrorMessage ="Mobile number can't be less than 10 digits")]
        public override string MobileNumber { get; set; }

        [StringLength(10, ErrorMessage = "(Should be 10 digits only)"), MinLength(10, ErrorMessage = "Mobile number can't be less than 10 digits")]
        public override string AltMobileNumber { get; set; }

        [Required(ErrorMessage ="Mandatory")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public override string EmailId { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        [DataType(DataType.Date)]
        public override string DOJ { get; set; }        

        [Required(ErrorMessage = "(Mandatory)")]
        [DataType(DataType.Date)]
        public override string DOB { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "(Mandatory)")]
        public override Int64 RoleId { get; set; }

        //[StringLength(20, MinimumLength = 14, ErrorMessage = "(Size should be between 14 to 20 digits)")]
        //[MinLength(14, ErrorMessage = "(Can not be less than 14 characters)")]
        //[StringLength(20, ErrorMessage = "(Can not be more than 20 characters)")]
        public override string IMEI1 { get; set; }

        //[StringLength(20, MinimumLength = 14, ErrorMessage = "(Size should be between 14 to 20 digits)")]
        //[MinLength(14, ErrorMessage = "(Can not be less than 14 characters)")]
        //[StringLength(20, ErrorMessage = "(Can not be more than 20 characters)")]
        public override string IMEI2 { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "(Mandatory)")]
        public override Int64 AgencyId { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "(Mandatory)")]
        public override Int64 DivisionId { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "(Mandatory)")]
        public override Int64 SFATypeId { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "(Mandatory)")]
        public override Int64 SFALevel { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        public override bool Status { get; set; }

        public int IsUser18Plus()
        {
            DateTime validDate = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day);
            DateTime dtDOB = DateTime.ParseExact(DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            TimeSpan validAge = DateTime.Now.Subtract(validDate);
            TimeSpan actualAge = DateTime.Now.Subtract(dtDOB);
            return TimeSpan.Compare(validAge, actualAge);
        }

        public bool IsSFAOffered { get; set; }
    }

    public class SFADropdown
    {
        public Int64 Id { get; set; }
        public string SFAName { get; set; }
    }

    public class OfferedEmployeeGridSearchFilters : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public List<Int64> BranchIds { get; set; }
        public List<Int64> StateIds { get; set; }
        public List<Int64> CityIds { get; set; }
        public string LoginId { get; set; }
        public string EmployeeName { get; set; }
    }

    public class OfferedEmployeeGridData
    {
        public string RoleName { get; set; }
        public Int64 Id { get; set; }
        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public string BranchName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Role { get; set; }
        public string AgencyName { get; set; }
        public string Status { get; set; }
        public Int64 IsActive { get; set; }
    }

    public class SFAForStructureMappingInput
    {
        public Int64 BranchId { get; set; }
        public Int64[] SFATypeId { get; set; }
        public Int64[] DivisionIds { get; set; }
    }

    public class UserUpdatePasswordModel
    {
        public Int64 UserId { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(5, ErrorMessage = "Length should be of at least 5 characters")]
        [MaxLength(200, ErrorMessage = "Length cannot be more than 200 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "(Mandatory)")]
        [MaxLength(200, ErrorMessage = "Length cannot be more than 200 characters")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }

    public class ModuleList
    {
        public Int64 ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleUrl { get; set; }
        public int ModuleSequence { get; set; }
        public string MenuIcon { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public bool IsActive { get; set; }
    }
    public class ModuleSubModule
    {
        public Int64 ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleUrl { get; set; }
        public int ModuleSequence { get; set; }
        public string MenuIcon { get; set; }
        public Int64? SubModuleId { get; set; }
        public string SubModuleName { get; set; }
        public string SubModuleUrl { get; set; }
        public int SubModuleSequence { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string SubMenuIcon { get; set; }
        public bool IsActive { get; set; }
    }

    public class ModuleRightsByRoleInput : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public  Int64 RoleId { get; set; }
        public override string RoleName { get; set; }
        public override Int64 UserId { get; set; }
    }

    public class MenusList
    {
        public List<ModuleList> ModuleList { get; set; }
        public List<ModuleSubModule> SubModuleList { get; set; }
        public MenusList()
        {
            ModuleList = new List<ModuleList>();
            SubModuleList = new List<ModuleSubModule>();
        }
    }

    public class SFACodeAutoGenerate
    {
        public string SFACode { get; set; }
    }
}
