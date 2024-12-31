using AmboLibrary.MasterMaintenance;
using AmboLibrary.GlobalAccessible;
using System.Collections.Generic;
using AmboLibrary.UserManagement;
using AmboLibrary.Modules;
using AmboLibrary.Mappings;
using AmboLibrary.IncentiveManagement;

namespace AmboProvider.Interface
{
    /// <summary>
    /// Fetching data by Id only 
    /// Kritika chadha
    /// </summary>
    public interface ICommonProvider
    {
        #region Get data  API for dropdown

        IEnumerable<StateMaster> GetStatesByRegion(Common InputParam);
        IEnumerable<CityMaster> GetCityByState(Common InputParam);
        IEnumerable<CityData> GetAllCities();
        IEnumerable<LocationData> GetAllLocations();
        IEnumerable<LocationMaster> GetLocationByCity(Common InputParam);
        IEnumerable<DealerMaster> GetDealersByLocation(Common InputParam);
        IEnumerable<DealerMaster> GetDealersByLocationNonSFA(Common InputParam);
        IEnumerable<DealerMaster> GetDealersByBranch(Common InputParam);
        IEnumerable<SearchSFAOutput> GetAllActiveSFA();
        IEnumerable<DealerList> GetAllActiveDealers();
        IEnumerable<DealerList> GetAllActiveDealersNonSFA(); 
         IEnumerable<BrandList> GetActiveBrands();
        IEnumerable<GetFeedbackForm> GetActiveFeedbackForms();
        IEnumerable<DealerMasterCode> GetNonSFADealerMasterCodesByBranch(Common InputParam);
        IEnumerable<NonSFADealer> GetNonSFADealersByMasterCodes(NonSFAMasterCodeList objInput, out string Message);
        IEnumerable<SFAFormData> GetSFAByDealer(Common InputParam);
        IEnumerable<SFADropdown> GetSFADropdown();
        IEnumerable<SFAFormData> GetSFAByBranch(Common InputParam);
        IEnumerable<SearchSFAOutput> GetSFAFromDivisionAndBranch(SearchSFA InputParam);
        IEnumerable<SearchSIDOutput> GetSIDFromBranchForBroadcast(SearchSID InputParam);
        IEnumerable<EmployeeFormData> GetAllActiveUsersByRole(Common InputParam);
        IEnumerable<SalesPIC> GetSalesPICByBranch(Common InputParam);
        IEnumerable<SFAFormData> GetAllActiveRDI();
        IEnumerable<SFALevelData> GetActiveSFALevels();
        IEnumerable<SFAFormData> GetUnmappedSFAByBranch(Common InputParam);
        IEnumerable<ProductSubCategoryMaster> GetProductSubCategoryByCategoryId(Common InputParam);
        IEnumerable<RegionMaster> GetRegion();
        IEnumerable<ChannelMaster> GetChannels();
        IEnumerable<StateMaster> GetStates();
        IEnumerable<RoleMaster> GetRole();
        IEnumerable<ClassificationTypeMaster> GetDealerClassificationTypes();
        IEnumerable<AgencyMaster> GetAgency(AgencyDropdownInput InputData);
        IEnumerable<IncentiveCategoryMaster> GetIncentiveCategory();
        IEnumerable<ProductTargetCategoryMaster> GetProductTargetCategories();
        IEnumerable<ProductTargetCategoryData> GetTargetCategoriesByProductCategories(ProductTargetCategorySearch objSearchData);
        IEnumerable<TargetTypeMaster> GetTargetTypes();
        IEnumerable<DivisionMaster> GetDivisions();
        IEnumerable<ProductCategoryMaster> GetProductCategories();
        IEnumerable<ProductCategoryMaster> GetUnmappedProdCatsForSFA(Common InputParam);
        IEnumerable<ProductCategoryGroupDD> GetProductCategoryForGroupMapping(Common InputParam);
        IEnumerable<SFASubLevelMaster> GetSFALevels();
        IEnumerable<DivisionMaster> GetDivisionForProductCategory();
        CityTypeList GetCityType();
        IEnumerable<ProductCategoryMaster> GetProductCategoryByDivision(Common InputParam);
        IEnumerable<Size> GetSize(AttributeGet InputParam);
        IEnumerable<Segment> GetSegment(AttributeGet InputParam);
        IEnumerable<TVType> GetTVType(AttributeGet InputParam);
        IEnumerable<D3> GetD3(AttributeGet InputParam);
        IEnumerable<Resolution> GetResolution(AttributeGet InputParam);
        IEnumerable<Internet> GetInternet(AttributeGet InputParam);
        int ValidateMaterialCode(Common InputParam, out string Message);
        IEnumerable<Competitors> GetCompetitors();
        IEnumerable<CompetitorProducts> GetCompetitorProducts(CompetitorProductsInput CompetitorId);
        IEnumerable<Materials> GetMaterialDataforDD();
        IEnumerable<Materials> GetMaterialDataforDDByProSub(MaterialDDInput Input);
        CompetitorData GetSonyProducts(CompetitorDataInput Id);
        IEnumerable<DisplayOrder> GetDisplayOrder();
        IEnumerable<SFAType> GetSFAType();
        int ValidateSAPCode(Common InputParam, out string Message);
        IEnumerable<AssetAssignmentToRDIGet> GetReferenceId();
        List<string> GetMaterialMasterCodeList();
        List<string> GetDealerCodeList();
        IEnumerable<LevelOfEducation> GetLevelOfEducation();
        IEnumerable<Source> GetSource();
        IEnumerable<Gender> GetGender();
        IEnumerable<OutletType> GetOutletType();
        IEnumerable<IncentiveCategoryMaster> GetAllIncentiveCategory();
        IEnumerable<ModuleMaster> GetAllModules();
        IEnumerable<SubModuleMaster> GetSubModulesByModuleId(SubModuleMasterGet InputParam);
        IEnumerable<FestivalIncentiveScheme> GetAllFestivalScheme(FestivalIncentiveSchemeParam InputParam);
        IEnumerable<AttendanceType> GetAllAttendanceType();
        IEnumerable<DeviationReasons> GetAllDeviationReasons();
		IEnumerable<ProductGroupCategoryMaster> GetProductCategoryGroupDropDown();
        GetBranch GetBranchByUserId(GetBranch InputParam);
        IEnumerable<TrainingDropdown> GetTrainings(GetTrainingDropdown InputParam);
        IEnumerable<SearchSFAOutput> GetSFAFromDivisionAndBranchAndRole(SearchSFA InputParam);
        IEnumerable<SearchSIDOutput> GetSIDFromBranchAndRoleForBroadcast(SearchSID InputParam);
        IEnumerable<SubCatgeory> GetSubCategoryforMultipleProducts(SubCatgeoryByProduct InputParam);
        Branchfohr GetBranchMappedForHR(GetBranch InputParam);
        IEnumerable<ShiftTimingName> GetShiftTiming();
        #endregion Get data API for dropdown
    }
}
