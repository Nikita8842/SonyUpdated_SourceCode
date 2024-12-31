using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmboLibrary.Abstract;
using AmboLibrary.MasterMaintenance;

namespace AmboLibrary.UserManagement
{
    /// <summary>
    /// UserManagement Class
    /// here will manage all the users 
    /// like SFA / Employee
    /// Reference :: Sony AMBO => EmployeeMaster
    /// Manbeer Singh Makhloga 06-March-2017
    /// </summary>
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


    /// <summary>
    /// Class to Create New User
    /// </summary>
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
        public virtual string Gender { get; set; }
        public virtual string FatherName { get; set; }
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
        public virtual bool Status { get; set; }
        public virtual Int64 SFATypeId { get; set; }
        public bool isUpdate { get; set; }
        public bool isPasswordChange { get; set; }
        public string SubmitMessage { get; set; }
        public string OldPassword { get; set; }
        public Int64 ShiftNameId { get; set; }
        public string ShiftName { get; set; }
    }

    /// <summary>
    /// to get user list
    /// </summary>
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
    }

    public class SFAFormData : UserBasicProperties
    {
        public string SFAFullName { get; set; }
        public string RDIFullName { get; set; }
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
