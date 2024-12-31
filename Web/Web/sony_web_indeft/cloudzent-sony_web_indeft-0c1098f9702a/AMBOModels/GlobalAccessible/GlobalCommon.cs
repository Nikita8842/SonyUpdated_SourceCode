using System;
using AMBOModels.Abstract;
using System.Collections.Generic;

namespace AMBOModels.GlobalAccessible
{
    /// <summary>
    /// will maintain the classes
    /// which can be used throughout the project
    /// </summary>
    public class GlobalCommon : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public Int64? Id { get; set; }
    }

    /// <summary>
    /// class to set respective pagination
    /// define PageIndex and PageSize
    /// </summary>
    public class GlobalPagination : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64 PageIndex { get; set; } = 1;
        public Int64 PageSize { get; set; } = 10;        
    }
    public class Common : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64? Id { get; set; }
        public string Value { get; set; }

    }

    public class RoleMaster : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public int RoleId { get; set; }
        public string Name { get; set; }
    }


    public class AgencyMaster : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64 AgencyId { get; set; }
        public string AgencyName { get; set; }
    }

    public class IncentiveCategoryMaster : MasterAbstract
    {
        public override Int64 UserId { get; set; }
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }

        public Int64 IncentiveCategoryId { get; set; }
        public string IncentiveCategoryName { get; set; }
    }
    public class CityTypeMaster
    {
        public Int64 CityTypeId { get; set; }
        public string CityType { get; set; }
    }

    public class CityTypeList
    {
        public List<CityTypeMaster> TypeList { get; set; }
        public CityTypeList()
        {
            TypeList = new List<CityTypeMaster>();
        }
    }
    public class Competitors
    {
        public Int64 ID { get; set; }
        public string Name { get; set; }
    }


    public class CompetitorProductsInput
    {
        public Int64 CompetitorID { get; set; }
    }

    public class CompetitorProducts
    {
        public Int64 ID { get; set; }
        public string Name { get; set; }
    }

    

    public class Materials
    {
        public Int64 ID { get; set; }
        public string Name { get; set; }
    }

    

    public class SFAType
    {
        public Int64 SFATypeId { get; set; }
        public string SFATypeName { get; set; }
    }

    public class DisplayOrder
    {
        public Int64 DisplayOrderId { get; set; }
        public string DisplayOrderName { get; set; }
    }

    public class LevelOfEducation
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }

    public class Source
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }

    public class Gender
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }

    public class OutletType
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }

    public class ModuleMaster
    {
        public Int64 ModuleId { get; set; }
        public string ModuleName { get; set; }
    }

    public class SubModuleMasterGet
    {
        public Int64 ModuleId { get; set; }
    }

    public class SubModuleMaster
    {
        public Int64 SubModuleId { get; set; }
        public string SubModuleName { get; set; }
    }

    public class RoleRightsMapping
    {
        public Int64 RoleRightsId { get; set; }
        public Int64 RoleId { get; set; }
        public Int64 SubModuleId { get; set; }
        public bool ViewPermission { get; set; }
        public bool CreatePermission { get; set; }
        public bool EditPermission { get; set; }
        public bool DeletePermission { get; set; }
    }

    public class RoleRightsMappingGet
    {
        public Int64 RoleRightsId { get; set; }
        public string RoleName { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public bool ViewPermission { get; set; }
        public bool CreatePermission { get; set; }
        public bool EditPermission { get; set; }
        public bool DeletePermission { get; set; }
    }

    public class RoleRightsMappingId
    {
        public Int64 RoleRightsId { get; set; }
    }

    public class FestivalIncentiveScheme
    {
        public Int64 Id { get; set; }
        public string SchemeName { get; set; }
    }
    public class FestivalIncentiveSchemeParam
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class AttendanceType
    {
        public Int64 AttTypeId { get; set; }
        public string AttType { get; set; }
    }

    public class GetBranch : MasterAbstract
    {
        public override string EncryptKey { get; set; }
        public override string RoleName { get; set; }
        public override long UserId { get; set; }

        public Int64 BranchId { get; set; }//
        public string Branch { get; set; }
    }

    public class GetTrainingDropdown
    {
        public Int64 BranchId { get; set; }
        public Int64 ChannelId { get; set; }
    }

    public class ErrorDetailsInput
    {
        public string ErrorSource { get; set; }
        public string ErrorDetails { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class AgencyDropdownInput
    {
        public Int64 BranchId { get; set; }
    }

    public class SubCatgeoryByProduct
    {
        public Int64[] ProductIds { get; set; }
    }

    public class SubCatgeory
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
    }

    public class Branchfohr
    {
        public List<Int64> BranchIds { get; set; }
    }

    public class ShiftTimingName
    {
        public Int64 ShiftNameId { get; set; }
        public string ShiftName { get; set; }
    }
}
