using System.Collections.Generic;
using AmboLibrary.GlobalAccessible;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.Modules;
using AmboLibrary.UserManagement;
using AmboUtilities;
using AmboLibrary.Mappings;
using AmboLibrary.IncentiveManagement;

namespace AmboServices.Interface
{
    /// <summary>
    /// Fetching data by Id only 
    /// Kritika chadha
    /// </summary>
    public interface ICommonService
    {
        #region Get data API for dropdown

        Envelope<IEnumerable<StateMaster>> GetStatesByRegion(Common InputParam);
        Envelope<IEnumerable<CityMaster>> GetCityByState(Common InputParam);
        Envelope<IEnumerable<CityData>> GetAllCities();
        Envelope<IEnumerable<LocationData>> GetAllLocations();
        Envelope<IEnumerable<LocationMaster>> GetLocationByCity(Common InputParam);
        Envelope<IEnumerable<DealerMaster>> GetDealersByLocation(Common InputParam);
        Envelope<IEnumerable<DealerMaster>> GetDealersByLocationNonSFA(Common InputParam);
        Envelope<IEnumerable<DealerMaster>> GetDealersByBranch(Common InputParam);
        Envelope<IEnumerable<SearchSFAOutput>> GetAllActiveSFA();
        Envelope<IEnumerable<DealerList>> GetAllActiveDealers();
        Envelope<IEnumerable<DealerList>> GetAllActiveDealersNonSFA();
        Envelope<IEnumerable<BrandList>> GetActiveBrands();
        Envelope<IEnumerable<GetFeedbackForm>> GetActiveFeedbackForms();
        Envelope<IEnumerable<DealerMasterCode>> GetNonSFADealerMasterCodesByBranch(Common InputParam);
        Envelope<IEnumerable<NonSFADealer>> GetNonSFADealersByMasterCodes(NonSFAMasterCodeList objInput);
        Envelope<IEnumerable<SFAFormData>> GetSFAByDealer(Common InputParam);
        Envelope<IEnumerable<SFADropdown>> GetSFADropdown();
        Envelope<IEnumerable<SFAFormData>> GetSFAByBranch(Common InputParam);
        Envelope<IEnumerable<SearchSFAOutput>> GetSFAFromDivisionAndBranch(SearchSFA InputParam);
        Envelope<IEnumerable<SearchSIDOutput>> GetSIDFromBranchForBroadcast(SearchSID InputParam);
        Envelope<IEnumerable<EmployeeFormData>> GetAllActiveUsersByRole(Common InputParam);
        Envelope<IEnumerable<SalesPIC>> GetSalesPICByBranch(Common InputParam);
        Envelope<IEnumerable<SFAFormData>> GetAllActiveRDI();
        Envelope<IEnumerable<SFALevelData>> GetActiveSFALevels();
        Envelope<IEnumerable<SFAFormData>> GetUnmappedSFAByBranch(Common InputParam);
        Envelope<IEnumerable<ProductSubCategoryMaster>> GetProductSubCategoryByCategoryId(Common InputParam);
        Envelope<IEnumerable<RegionMaster>> GetRegion();
        Envelope<IEnumerable<ChannelMaster>> GetChannels();
        Envelope<IEnumerable<StateMaster>> GetStates();
        Envelope<IEnumerable<RoleMaster>> GetRole();
        Envelope<IEnumerable<ClassificationTypeMaster>> GetDealerClassificationTypes();
        Envelope<IEnumerable<AgencyMaster>> GetAgency(AgencyDropdownInput InputData);
        Envelope<IEnumerable<IncentiveCategoryMaster>> GetIncentiveCategory();
        Envelope<IEnumerable<ProductTargetCategoryMaster>> GetProductTargetCategories();
        Envelope<IEnumerable<ProductTargetCategoryData>> GetTargetCategoriesByProductCategories(ProductTargetCategorySearch objSearchData);
        Envelope<IEnumerable<TargetTypeMaster>> GetTargetTypes();
        Envelope<IEnumerable<DivisionMaster>> GetDivisions();
        Envelope<IEnumerable<ProductCategoryMaster>> GetProductCategories();
        Envelope<IEnumerable<ProductCategoryMaster>> GetUnmappedProdCatsForSFA(Common InputParam);
        Envelope<IEnumerable<ProductCategoryGroupDD>> GetProductCategoryForGroupMapping(Common InputParam);
        Envelope<IEnumerable<SFASubLevelMaster>> GetSFALevels();
        Envelope<CityTypeList> GetCityType();
        Envelope<IEnumerable<DivisionMaster>> GetDivisionForProductCategory();
        Envelope<IEnumerable<ProductCategoryMaster>> GetProductCategoryByDivision(Common InputParam);
        Envelope<IEnumerable<Size>> GetSize(AttributeGet InputParam);
        Envelope<IEnumerable<Segment>> GetSegment(AttributeGet InputParam);
        Envelope<IEnumerable<TVType>> GetTVType(AttributeGet InputParam);
        Envelope<IEnumerable<D3>> GetD3(AttributeGet InputParam);
        Envelope<IEnumerable<Resolution>> GetResolution(AttributeGet InputParam);
        Envelope<IEnumerable<Internet>> GetInternet(AttributeGet InputParam);
        Envelope<bool> ValidateMaterialCode(Common InputParam);
        Envelope<IEnumerable<Competitors>> GetCompetitors();
        Envelope<IEnumerable<CompetitorProducts>> GetCompetitorProducts(CompetitorProductsInput CompetitorId);
        Envelope<IEnumerable<Materials>> GetMaterialDataforDD();
        Envelope<IEnumerable<Materials>> GetMaterialDataforDDByProSub(MaterialDDInput Input);
        Envelope<CompetitorData> GetSonyProducts(CompetitorDataInput Id);
        Envelope<IEnumerable<SFAType>> GetSFAType();
        Envelope<IEnumerable<DisplayOrder>> GetDisplayOrder();
        Envelope<bool> ValidateSAPCode(Common InputParam);
        Envelope<IEnumerable<AssetAssignmentToRDIGet>> GetReferenceId();
        Envelope<List<string>> GetMaterialMasterCodeList();
        Envelope<List<string>> GetDealerCodeList();
        Envelope<IEnumerable<LevelOfEducation>> GetLevelOfEducation();
        Envelope<IEnumerable<Source>> GetSource();
        Envelope<IEnumerable<Gender>> GetGender();
        Envelope<IEnumerable<OutletType>> GetOutletType();
        Envelope<IEnumerable<IncentiveCategoryMaster>> GetAllIncentiveCategory();
        Envelope<IEnumerable<ModuleMaster>> GetAllModules();
        Envelope<IEnumerable<SubModuleMaster>> GetSubModulesByModuleId(SubModuleMasterGet InputParam);
        Envelope<IEnumerable<FestivalIncentiveScheme>> GetAllFestivalScheme(FestivalIncentiveSchemeParam InputParam);
        Envelope<IEnumerable<AttendanceType>> GetAllAttendanceType();
        Envelope<IEnumerable<DeviationReasons>> GetAllDeviationReasons();
		Envelope<IEnumerable<ProductGroupCategoryMaster>> GetProductCategoryGroupDropDown();
        Envelope<GetBranch> GetBranchByUserId(GetBranch InputParam);
        Envelope<IEnumerable<TrainingDropdown>> GetTrainings(GetTrainingDropdown InputParam);
        Envelope<IEnumerable<SearchSFAOutput>> GetSFAFromDivisionAndBranchAndRole(SearchSFA InputParam);
        Envelope<IEnumerable<SearchSIDOutput>> GetSIDFromBranchAndRoleForBroadcast(SearchSID InputParam);
        Envelope<IEnumerable<SubCatgeory>> GetSubCategoryforMultipleProducts(SubCatgeoryByProduct InputParam);
        Envelope<Branchfohr> GetBranchMappedForHR(GetBranch InputParam);
        Envelope<IEnumerable<ShiftTimingName>> GetShiftTiming();
        #endregion Get Data API for dropdown
    }
}
