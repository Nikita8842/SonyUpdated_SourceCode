using System;
using System.Collections.Generic;
using AmboLibrary.UserManagement;
using AmboDataServices.Interface;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.GlobalAccessible;
using AmboProvider.Interface;
using AmboLibrary.Modules;
using AmboLibrary.Mappings;
using AmboLibrary.IncentiveManagement;

namespace AmboDataServices.Implimentation
{
    /// <summary>
    /// Fetching data by Id only 
    /// Kritika chadha
    /// </summary>
    public class CommonDataService : ICommonDataService
    {
        private readonly ICommonProvider _ICommonProvider;

        public CommonDataService(ICommonProvider ICommonProvider)
        {
            _ICommonProvider = ICommonProvider;
        }
        #region Get data API for dropdown

        public IEnumerable<StateMaster> GetStatesByRegion(Common InputParam)
        {
            return _ICommonProvider.GetStatesByRegion(InputParam);
        }
        public IEnumerable<CityMaster> GetCityByState(Common InputParam)
        {
            return _ICommonProvider.GetCityByState(InputParam);
        }
        public IEnumerable<CityData> GetAllCities()
        {
            return _ICommonProvider.GetAllCities();
        }
        public IEnumerable<LocationData> GetAllLocations()
        {
            return _ICommonProvider.GetAllLocations();
        }
        public IEnumerable<LocationMaster> GetLocationByCity(Common InputParam)
        {
            return _ICommonProvider.GetLocationByCity(InputParam);
        }
        public IEnumerable<DealerMaster> GetDealersByLocation(Common InputParam)
        {
            return _ICommonProvider.GetDealersByLocation(InputParam);
        }
        public IEnumerable<DealerMaster> GetDealersByLocationNonSFA(Common InputParam)
        {
            return _ICommonProvider.GetDealersByLocationNonSFA(InputParam);
        }

        public IEnumerable<DealerMaster> GetDealersByBranch(Common InputParam)
        {
            return _ICommonProvider.GetDealersByBranch(InputParam);
        }

        public IEnumerable<SearchSFAOutput> GetAllActiveSFA()
        {
            return _ICommonProvider.GetAllActiveSFA();
        }

        public IEnumerable<DealerList> GetAllActiveDealers()
        {
            return _ICommonProvider.GetAllActiveDealers();
        }
        public IEnumerable<DealerList> GetAllActiveDealersNonSFA()
        {
            return _ICommonProvider.GetAllActiveDealersNonSFA();
        }

        public IEnumerable<BrandList> GetActiveBrands()
        {
            return _ICommonProvider.GetActiveBrands();
        }

        public IEnumerable<GetFeedbackForm> GetActiveFeedbackForms()
        {
            return _ICommonProvider.GetActiveFeedbackForms();
        }

        public IEnumerable<DealerMasterCode> GetNonSFADealerMasterCodesByBranch(Common InputParam)
        {
            return _ICommonProvider.GetNonSFADealerMasterCodesByBranch(InputParam);
        }
        public IEnumerable<NonSFADealer> GetNonSFADealersByMasterCodes(NonSFAMasterCodeList objInput, out string Message)
        {
            return _ICommonProvider.GetNonSFADealersByMasterCodes(objInput, out Message);
        }
        public IEnumerable<SFAFormData> GetSFAByDealer(Common InputParam)
        {
            return _ICommonProvider.GetSFAByDealer(InputParam);
        }

        public IEnumerable<SFADropdown> GetSFADropdown()
        {
            return _ICommonProvider.GetSFADropdown();
        }

        public IEnumerable<EmployeeFormData> GetAllActiveUsersByRole(Common InputParam)
        {
            return _ICommonProvider.GetAllActiveUsersByRole(InputParam);
        }
        public IEnumerable<SalesPIC> GetSalesPICByBranch(Common InputParam)
        {
            return _ICommonProvider.GetSalesPICByBranch(InputParam);
        }
        public IEnumerable<SFAFormData> GetAllActiveRDI()
        {
            return _ICommonProvider.GetAllActiveRDI();
        }
        public IEnumerable<SFALevelData> GetActiveSFALevels()
        {
            return _ICommonProvider.GetActiveSFALevels();
        }
        public IEnumerable<SFAFormData> GetSFAByBranch(Common InputParam)
        {
            return _ICommonProvider.GetSFAByBranch(InputParam);
        }
        public IEnumerable<SearchSFAOutput> GetSFAFromDivisionAndBranch(SearchSFA InputParam)
        {
            return _ICommonProvider.GetSFAFromDivisionAndBranch(InputParam);
        }
        public IEnumerable<SearchSIDOutput> GetSIDFromBranchForBroadcast(SearchSID InputParam)
        {
            return _ICommonProvider.GetSIDFromBranchForBroadcast(InputParam);
        }
        public IEnumerable<SFAFormData> GetUnmappedSFAByBranch(Common InputParam)
        {
            return _ICommonProvider.GetUnmappedSFAByBranch(InputParam);
        }
        public IEnumerable<RegionMaster> GetRegion()
        {
            return _ICommonProvider.GetRegion();
        }
        public IEnumerable<ChannelMaster> GetChannels()
        {
            return _ICommonProvider.GetChannels();
        }
        public IEnumerable<StateMaster> GetStates()
        {
            return _ICommonProvider.GetStates();
        }
        public IEnumerable<RoleMaster> GetRole()
        {
            return _ICommonProvider.GetRole();
        }
        public IEnumerable<ClassificationTypeMaster> GetDealerClassificationTypes()
        {
            return _ICommonProvider.GetDealerClassificationTypes();
        }
        public IEnumerable<AgencyMaster> GetAgency(AgencyDropdownInput InputData)
        {
            return _ICommonProvider.GetAgency(InputData);
        }
        public IEnumerable<IncentiveCategoryMaster> GetIncentiveCategory()
        {
            return _ICommonProvider.GetIncentiveCategory();
        }

        public IEnumerable<ProductTargetCategoryMaster> GetProductTargetCategories()
        {
            return _ICommonProvider.GetProductTargetCategories();
        }
        public IEnumerable<ProductTargetCategoryData> GetTargetCategoriesByProductCategories(ProductTargetCategorySearch objSearchData)
        {
            return _ICommonProvider.GetTargetCategoriesByProductCategories(objSearchData);
        }
        public IEnumerable<TargetTypeMaster> GetTargetTypes()
        {
            return _ICommonProvider.GetTargetTypes();
        }

        public IEnumerable<DivisionMaster> GetDivisions()
        {
            return _ICommonProvider.GetDivisions();
        }

        public IEnumerable<ProductCategoryMaster> GetProductCategories()
        {
            return _ICommonProvider.GetProductCategories();
        }

        public IEnumerable<ProductCategoryMaster> GetUnmappedProdCatsForSFA(Common InputParam)
        {
            return _ICommonProvider.GetUnmappedProdCatsForSFA(InputParam);
        }

        public IEnumerable<ProductCategoryGroupDD> GetProductCategoryForGroupMapping(Common InputParam)
        {
            return _ICommonProvider.GetProductCategoryForGroupMapping(InputParam);
        }
        public IEnumerable<SFASubLevelMaster> GetSFALevels()
        {
            return _ICommonProvider.GetSFALevels();
        }
        public CityTypeList GetCityType()
        {
            return _ICommonProvider.GetCityType();
        }
        public IEnumerable<DivisionMaster> GetDivisionForProductCategory()
        {
            return _ICommonProvider.GetDivisionForProductCategory();
        }

        public IEnumerable<ProductCategoryMaster> GetProductCategoryByDivision(Common InputParam)
        {
            return _ICommonProvider.GetProductCategoryByDivision(InputParam);
        }
        public IEnumerable<Size> GetSize(AttributeGet InputParam)
        {
            return _ICommonProvider.GetSize(InputParam);
        }
        public IEnumerable<Segment> GetSegment(AttributeGet InputParam)
        {
            return _ICommonProvider.GetSegment(InputParam);
        }
        public IEnumerable<TVType> GetTVType(AttributeGet InputParam)
        {
            return _ICommonProvider.GetTVType(InputParam);
        }
        public IEnumerable<D3> GetD3(AttributeGet InputParam)
        {
            return _ICommonProvider.GetD3(InputParam);
        }
        public IEnumerable<Resolution> GetResolution(AttributeGet InputParam)
        {
            return _ICommonProvider.GetResolution(InputParam);
        }
        public IEnumerable<Internet> GetInternet(AttributeGet InputParam)
        {
            return _ICommonProvider.GetInternet(InputParam);
        }
        public bool ValidateMaterialCode(Common InputParam, out string Message)
        {
            return Convert.ToBoolean(_ICommonProvider.ValidateMaterialCode(InputParam, out Message));
        }
        public IEnumerable<ProductSubCategoryMaster> GetProductSubCategoryByCategoryId(Common InputParam)
        {
            return _ICommonProvider.GetProductSubCategoryByCategoryId(InputParam);
        }
        public IEnumerable<Competitors> GetCompetitors()
        {
            return _ICommonProvider.GetCompetitors();
        }
        public IEnumerable<CompetitorProducts> GetCompetitorProducts(CompetitorProductsInput CompetitorId)
        {
            return _ICommonProvider.GetCompetitorProducts(CompetitorId);
        }
        public IEnumerable<Materials> GetMaterialDataforDD()
        {
            return _ICommonProvider.GetMaterialDataforDD();
        }

        public IEnumerable<Materials> GetMaterialDataforDDByProSub(MaterialDDInput Input)
        {
            return _ICommonProvider.GetMaterialDataforDDByProSub(Input);
        }

        public CompetitorData GetSonyProducts(CompetitorDataInput Id)
        {
            return _ICommonProvider.GetSonyProducts(Id);
        }

        public IEnumerable<SFAType> GetSFAType()
        {
            return _ICommonProvider.GetSFAType();
        }
        public IEnumerable<DisplayOrder> GetDisplayOrder()
        {
            return _ICommonProvider.GetDisplayOrder();
        }
        public bool ValidateSAPCode(Common InputParam, out string Message)
        {
            return Convert.ToBoolean(_ICommonProvider.ValidateSAPCode(InputParam, out Message));
        }

        public IEnumerable<AssetAssignmentToRDIGet> GetReferenceId()
        {
            return _ICommonProvider.GetReferenceId();
        }

        public List<string> GetMaterialMasterCodeList()
        {
            return _ICommonProvider.GetMaterialMasterCodeList();
        }

        public List<string> GetDealerCodeList()
        {
            return _ICommonProvider.GetDealerCodeList();
        }

        public IEnumerable<LevelOfEducation> GetLevelOfEducation()
        {
            return _ICommonProvider.GetLevelOfEducation();
        }

        public IEnumerable<Source> GetSource()
        {
            return _ICommonProvider.GetSource();
        }

        public IEnumerable<Gender> GetGender()
        {
            return _ICommonProvider.GetGender();
        }

        public IEnumerable<OutletType> GetOutletType()
        {
            return _ICommonProvider.GetOutletType();
        }

        public IEnumerable<IncentiveCategoryMaster> GetAllIncentiveCategory()
        {
            return _ICommonProvider.GetAllIncentiveCategory();
        }

        public IEnumerable<ModuleMaster> GetAllModules()
        {
            return _ICommonProvider.GetAllModules();
        }

        public IEnumerable<SubModuleMaster> GetSubModulesByModuleId(SubModuleMasterGet InputParam)
        {
            return _ICommonProvider.GetSubModulesByModuleId(InputParam);
        }

        public IEnumerable<FestivalIncentiveScheme> GetAllFestivalScheme(FestivalIncentiveSchemeParam InputParam)
        {
            return _ICommonProvider.GetAllFestivalScheme(InputParam);
        }

        public IEnumerable<AttendanceType> GetAllAttendanceType()
        {
            return _ICommonProvider.GetAllAttendanceType();
        }

        public IEnumerable<DeviationReasons> GetAllDeviationReasons()
        {
            return _ICommonProvider.GetAllDeviationReasons();
        }
		
		public IEnumerable<ProductGroupCategoryMaster> GetProductCategoryGroupDropDown()
        {
            return _ICommonProvider.GetProductCategoryGroupDropDown();
        }

        public GetBranch GetBranchByUserId(GetBranch InputParam)
        {
            return _ICommonProvider.GetBranchByUserId(InputParam);
        }

        public IEnumerable<TrainingDropdown> GetTrainings(GetTrainingDropdown InputParam)
        {
            return _ICommonProvider.GetTrainings(InputParam);
        }

        public IEnumerable<SearchSFAOutput> GetSFAFromDivisionAndBranchAndRole(SearchSFA InputParam)
        {
            return _ICommonProvider.GetSFAFromDivisionAndBranchAndRole(InputParam);
        }


        public IEnumerable<SearchSIDOutput> GetSIDFromBranchAndRoleForBroadcast(SearchSID InputParam)
        {
            return _ICommonProvider.GetSIDFromBranchAndRoleForBroadcast(InputParam);
        }

        public IEnumerable<SubCatgeory> GetSubCategoryforMultipleProducts(SubCatgeoryByProduct InputParam)
        {
            return _ICommonProvider.GetSubCategoryforMultipleProducts(InputParam);
        }

        public Branchfohr GetBranchMappedForHR(GetBranch InputParam)
        {
            return _ICommonProvider.GetBranchMappedForHR(InputParam);
        }

        public IEnumerable<ShiftTimingName> GetShiftTiming()
        {
            return _ICommonProvider.GetShiftTiming();
        }
        #endregion Get Data API for dropdown


    }
}
