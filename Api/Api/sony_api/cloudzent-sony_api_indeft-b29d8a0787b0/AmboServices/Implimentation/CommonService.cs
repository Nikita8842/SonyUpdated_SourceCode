using System.Collections.Generic;
using System.Linq;
using System;
using AmboLibrary.UserManagement;
using AmboLibrary.Modules;
using AmboDataServices.Interface;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.GlobalAccessible;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using AmboLibrary.Mappings;
using AmboLibrary.IncentiveManagement;

namespace AmboServices.Implimentation
{ 
    /// <summary>
    /// Fetching data by Id only 
    /// Kritika chadha
    /// </summary>
    public class CommonService: ICommonService
    {
        private readonly ICommonDataService _ICommonDataService;
        public CommonService(ICommonDataService ICommonDataService)
        {
            _ICommonDataService = ICommonDataService;
        }

        public Envelope<IEnumerable<StateMaster>> GetStatesByRegion(Common InputParam)
        {
            var output = _ICommonDataService.GetStatesByRegion(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<StateMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<StateMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "States for Region fetched successfully." };
        }

        public Envelope<IEnumerable<CityMaster>> GetCityByState(Common InputParam)
        {
            var output = _ICommonDataService.GetCityByState(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<CityMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<CityMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Cities for state fetched successfully." };
        }

        public Envelope<IEnumerable<CityData>> GetAllCities()
        {
            var output = _ICommonDataService.GetAllCities();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<CityData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<CityData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Cities fetched successfully." };
        }

        public Envelope<IEnumerable<LocationData>> GetAllLocations()
        {
            var output = _ICommonDataService.GetAllLocations();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<LocationData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<LocationData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Locations fetched successfully." };
        }

        public Envelope<IEnumerable<LocationMaster>> GetLocationByCity(Common InputParam)
        {
            var output = _ICommonDataService.GetLocationByCity(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<LocationMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<LocationMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Location for City fetched successfully." };
        }
        public Envelope<IEnumerable<DealerMaster>> GetDealersByLocation(Common InputParam)
        {
            var output = _ICommonDataService.GetDealersByLocation(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<DealerMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<DealerMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Dealers in a location fetched successfully." };
        }

        public Envelope<IEnumerable<DealerMaster>> GetDealersByLocationNonSFA(Common InputParam)
        {
            var output = _ICommonDataService.GetDealersByLocationNonSFA(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<DealerMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<DealerMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Dealers in a location fetched successfully." };
        }

        public Envelope<IEnumerable<DealerMaster>> GetDealersByBranch(Common InputParam)
        {
            var output = _ICommonDataService.GetDealersByBranch(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<DealerMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<DealerMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Dealers under a branch fetched successfully." };
        }

        public Envelope<IEnumerable<SearchSFAOutput>> GetAllActiveSFA()
        {
            var output = _ICommonDataService.GetAllActiveSFA();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SearchSFAOutput>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SearchSFAOutput>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All active SFA fetched successfully." };
        }

        public Envelope<IEnumerable<DealerList>> GetAllActiveDealers()
        {
            var output = _ICommonDataService.GetAllActiveDealers();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<DealerList>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<DealerList>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All active dealers fetched successfully." };
        }

        public Envelope<IEnumerable<DealerList>> GetAllActiveDealersNonSFA()
        {
            var output = _ICommonDataService.GetAllActiveDealersNonSFA();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<DealerList>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<DealerList>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All active dealers fetched successfully." };
        }

        public Envelope<IEnumerable<BrandList>> GetActiveBrands()
        {
            var List = _ICommonDataService.GetActiveBrands();

            return (List != null && List.Count() != 0) ?
                new Envelope<IEnumerable<BrandList>> { Data = List, MessageCode = (int)Acceptable.Found, Message = "Brands list fetched successfully." } :
                new Envelope<IEnumerable<BrandList>> { Data = List, MessageCode = (int)NotAcceptable.NotFound, Message = "Please try again." };
        }

        public Envelope<IEnumerable<GetFeedbackForm>> GetActiveFeedbackForms()
        {
            var List = _ICommonDataService.GetActiveFeedbackForms();

            return (List != null && List.Count() != 0) ?
                new Envelope<IEnumerable<GetFeedbackForm>> { Data = List, MessageCode = (int)Acceptable.Found, Message = "Feedback form list fetched successfully." } :
                new Envelope<IEnumerable<GetFeedbackForm>> { Data = List, MessageCode = (int)NotAcceptable.NotFound, Message = "Please try again." };
        }

        public Envelope<IEnumerable<DealerMasterCode>> GetNonSFADealerMasterCodesByBranch(Common InputParam)
        {
            var output = _ICommonDataService.GetNonSFADealerMasterCodesByBranch(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<DealerMasterCode>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<DealerMasterCode>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Non SFA Dealer Master Codes under a branch fetched successfully." };
        }

        public Envelope<IEnumerable<NonSFADealer>> GetNonSFADealersByMasterCodes(NonSFAMasterCodeList objInput)
        {
            string Message = string.Empty;
            var output = _ICommonDataService.GetNonSFADealersByMasterCodes(objInput, out Message);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<NonSFADealer>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = Message } :
                new Envelope<IEnumerable<NonSFADealer>> { Data = output, MessageCode = (int)Acceptable.Found, Message = Message };
        }

        public Envelope<IEnumerable<SFAFormData>> GetSFAByDealer(Common InputParam)
        {
            var output = _ICommonDataService.GetSFAByDealer(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SFAFormData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SFAFormData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA of a dealer fetched successfully." };
        }

        public Envelope<IEnumerable<SFADropdown>> GetSFADropdown()
        {
            var output = _ICommonDataService.GetSFADropdown();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SFADropdown>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SFADropdown>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA dropdown fetched successfully." };
        }

        public Envelope<IEnumerable<SFAFormData>> GetSFAByBranch(Common InputParam)
        {
            var output = _ICommonDataService.GetSFAByBranch(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SFAFormData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SFAFormData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA of a branch fetched successfully." };
        }

        public Envelope<IEnumerable<SearchSFAOutput>> GetSFAFromDivisionAndBranch(SearchSFA InputParam)
        {
            var output = _ICommonDataService.GetSFAFromDivisionAndBranch(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SearchSFAOutput>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SearchSFAOutput>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA fetched successfully." };
        }


        public Envelope<IEnumerable<SearchSFAOutput>> GetSFAFromDivisionAndBranchAndRole(SearchSFA InputParam)
        {
            var output = _ICommonDataService.GetSFAFromDivisionAndBranchAndRole(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SearchSFAOutput>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SearchSFAOutput>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA fetched successfully." };
        }

        public Envelope<IEnumerable<SearchSIDOutput>> GetSIDFromBranchForBroadcast(SearchSID InputParam)
        {
            var output = _ICommonDataService.GetSIDFromBranchForBroadcast(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SearchSIDOutput>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SearchSIDOutput>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SID fetched successfully." };
        }

        public Envelope<IEnumerable<EmployeeFormData>> GetAllActiveUsersByRole(Common InputParam)
        {
            var output = _ICommonDataService.GetAllActiveUsersByRole(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<EmployeeFormData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<EmployeeFormData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All active users by role fetched successfully." };
        }

        public Envelope<IEnumerable<SalesPIC>> GetSalesPICByBranch(Common InputParam)
        {
            var output = _ICommonDataService.GetSalesPICByBranch(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SalesPIC>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SalesPIC>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All active Sales PIC by branch fetched successfully." };
        }

        public Envelope<IEnumerable<SFAFormData>> GetAllActiveRDI()
        {
            var output = _ICommonDataService.GetAllActiveRDI();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SFAFormData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SFAFormData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All active RDI fetched successfully." };
        }

        public Envelope<IEnumerable<SFALevelData>> GetActiveSFALevels()
        {
            var output = _ICommonDataService.GetActiveSFALevels();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SFALevelData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SFALevelData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All active SFA levels fetched successfully." };
        }

        public Envelope<IEnumerable<SFAFormData>> GetUnmappedSFAByBranch(Common InputParam)
        {
            var output = _ICommonDataService.GetUnmappedSFAByBranch(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SFAFormData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SFAFormData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA of a branch fetched successfully." };
        }

        public Envelope<IEnumerable<RegionMaster>> GetRegion()
        {
            var output = _ICommonDataService.GetRegion();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<RegionMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<RegionMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Region fetched successfully." };
        }

        public Envelope<IEnumerable<ChannelMaster>> GetChannels()
        {
            var output = _ICommonDataService.GetChannels();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ChannelMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ChannelMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Channels fetched successfully." };
        }

        public Envelope<IEnumerable<StateMaster>> GetStates()
        {
            var output = _ICommonDataService.GetStates();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<StateMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<StateMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All states fetched successfully." };
        }

        public Envelope<IEnumerable<RoleMaster>> GetRole()
        {
            var output = _ICommonDataService.GetRole();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<RoleMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<RoleMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Role list fetched successfully." };
        }

        public Envelope<IEnumerable<ClassificationTypeMaster>> GetDealerClassificationTypes()
        {
            var output = _ICommonDataService.GetDealerClassificationTypes();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ClassificationTypeMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ClassificationTypeMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Dealer Classification Types fetched successfully." };
        }

        public Envelope<IEnumerable<AgencyMaster>> GetAgency(AgencyDropdownInput InputData)
        {
            var output = _ICommonDataService.GetAgency(InputData);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<AgencyMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<AgencyMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Agency list fetched successfully." };
        }
        public Envelope<IEnumerable<IncentiveCategoryMaster>> GetIncentiveCategory()
        {
            var output = _ICommonDataService.GetIncentiveCategory();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<IncentiveCategoryMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<IncentiveCategoryMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Incentive category list fetched successfully." };
        }

        public Envelope<IEnumerable<ProductTargetCategoryMaster>> GetProductTargetCategories()
        {
            var output = _ICommonDataService.GetProductTargetCategories();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductTargetCategoryMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductTargetCategoryMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product target category list fetched successfully." };
        }

        public Envelope<IEnumerable<ProductTargetCategoryData>> GetTargetCategoriesByProductCategories(ProductTargetCategorySearch objSearchData)
        {
            var output = _ICommonDataService.GetTargetCategoriesByProductCategories(objSearchData);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductTargetCategoryData>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductTargetCategoryData>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product target category list fetched successfully." };
        }

        public Envelope<IEnumerable<TargetTypeMaster>> GetTargetTypes()
        {
            var output = _ICommonDataService.GetTargetTypes();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<TargetTypeMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<TargetTypeMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Target Type list fetched successfully." };
        }

        public Envelope<IEnumerable<DivisionMaster>> GetDivisions()
        {
            var output = _ICommonDataService.GetDivisions();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<DivisionMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<DivisionMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Division dropdown fetched successfully." };
        }

        public Envelope<IEnumerable<ProductCategoryMaster>> GetProductCategories()
        {
            var output = _ICommonDataService.GetProductCategories();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductCategoryMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductCategoryMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product category dropdown fetched successfully." };
        }

        public Envelope<IEnumerable<ProductCategoryMaster>> GetUnmappedProdCatsForSFA(Common InputParam)
        {
            var output = _ICommonDataService.GetUnmappedProdCatsForSFA(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductCategoryMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductCategoryMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product category dropdown for SFA mapping fetched successfully." };
        }

        public Envelope<IEnumerable<ProductCategoryGroupDD>> GetProductCategoryForGroupMapping(Common InputParam)
        {
            var output = _ICommonDataService.GetProductCategoryForGroupMapping(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductCategoryGroupDD>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductCategoryGroupDD>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product category dropdown fetched successfully." };
        }

        public Envelope<IEnumerable<SFASubLevelMaster>> GetSFALevels()
        {
            var output = _ICommonDataService.GetSFALevels();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SFASubLevelMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SFASubLevelMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA Level dropdown fetched successfully." };
        }
        public Envelope<CityTypeList> GetCityType()
        {
            var output = _ICommonDataService.GetCityType();
            return (output.TypeList == null || output.TypeList.Count() == 0) ?
                new Envelope<CityTypeList> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<CityTypeList> { Data = output, MessageCode = (int)Acceptable.Found, Message = "City Type data fetched successfully." };
        }
        public Envelope<IEnumerable<DivisionMaster>> GetDivisionForProductCategory()
        {
            var output = _ICommonDataService.GetDivisionForProductCategory();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<DivisionMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<DivisionMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Divison data fetched successfully." };
        }

        public Envelope<IEnumerable<ProductCategoryMaster>> GetProductCategoryByDivision(Common InputParam)
        {
            var output = _ICommonDataService.GetProductCategoryByDivision(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductCategoryMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductCategoryMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "product category by division data fetched successfully." };
        }

        public Envelope<IEnumerable<Size>> GetSize(AttributeGet InputParam)
        {
            var output = _ICommonDataService.GetSize(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<Size>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<Size>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Size data fetched successfully." };
        }

        public Envelope<IEnumerable<Segment>> GetSegment(AttributeGet InputParam)
        {
            var output = _ICommonDataService.GetSegment(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<Segment>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<Segment>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "segment data fetched successfully." };
        }

        public Envelope<IEnumerable<D3>> GetD3(AttributeGet InputParam)
        {
            var output = _ICommonDataService.GetD3(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<D3>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<D3>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "3D data fetched successfully." };
        }

        public Envelope<IEnumerable<Internet>> GetInternet(AttributeGet InputParam)
        {
            var output = _ICommonDataService.GetInternet(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<Internet>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<Internet>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "internet data fetched successfully." };
        }


        public Envelope<IEnumerable<Resolution>> GetResolution(AttributeGet InputParam)
        {
            var output = _ICommonDataService.GetResolution(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<Resolution>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<Resolution>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Resolution data fetched successfully." };
        }

        public Envelope<IEnumerable<TVType>> GetTVType(AttributeGet InputParam)
        {
            var output = _ICommonDataService.GetTVType(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<TVType>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<TVType>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "TVType data fetched successfully." };
        }

        public Envelope<bool> ValidateMaterialCode(Common InputParam)
        {
            var Message = string.Empty;
            var output = _ICommonDataService.ValidateMaterialCode(InputParam, out Message);
            return (output) ?
                new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message } :
                new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message };
        }
		
		public Envelope<IEnumerable<ProductSubCategoryMaster>> GetProductSubCategoryByCategoryId(Common InputParam)
        {
            var output = _ICommonDataService.GetProductSubCategoryByCategoryId(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductSubCategoryMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductSubCategoryMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Sub category for product category fetched successfully." };
        }

        public Envelope<IEnumerable<Competitors>> GetCompetitors()
        {
            var output = _ICommonDataService.GetCompetitors();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<Competitors>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<Competitors>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competitors fetched successfully." };
        }
        public Envelope<IEnumerable<CompetitorProducts>> GetCompetitorProducts(CompetitorProductsInput CompetitorId)
        {
            var output = _ICommonDataService.GetCompetitorProducts(CompetitorId);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<CompetitorProducts>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<CompetitorProducts>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Competitor's products fetched successfully." };
        }
        public Envelope<IEnumerable<Materials>> GetMaterialDataforDD()
        {
            var output = _ICommonDataService.GetMaterialDataforDD();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<Materials>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<Materials>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Materials fetched successfully." };
        }

        public Envelope<IEnumerable<Materials>> GetMaterialDataforDDByProSub(MaterialDDInput Input)
        {
            var output = _ICommonDataService.GetMaterialDataforDDByProSub(Input);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<Materials>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<Materials>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Materials fetched successfully." };
        }

        public Envelope<CompetitorData> GetSonyProducts(CompetitorDataInput Id)
        {
            var output = _ICommonDataService.GetSonyProducts(Id);
            return (output == null || output.SProductSubCatList.Count() == 0) ?
                new Envelope<CompetitorData> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<CompetitorData> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Data fetched successfully." };
        }
        public Envelope<IEnumerable<SFAType>> GetSFAType()
        {
            var output = _ICommonDataService.GetSFAType();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SFAType>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SFAType>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SFA Type data fetched successfully." };
        }

        public Envelope<IEnumerable<DisplayOrder>> GetDisplayOrder()
        {
            var output = _ICommonDataService.GetDisplayOrder();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<DisplayOrder>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<DisplayOrder>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Display Order Type data fetched successfully." };
        }

        public Envelope<bool> ValidateSAPCode(Common InputParam)
        {
            var Message = string.Empty;
            var output = _ICommonDataService.ValidateSAPCode(InputParam, out Message);
            return (output == false) ?
            new Envelope<bool> { Data = output, MessageCode = (int)NotAcceptable.NotAcceptable, Message = Message } :
            new Envelope<bool> { Data = output, MessageCode = (int)Acceptable.Accepted, Message = Message };
        }

        public Envelope<IEnumerable<AssetAssignmentToRDIGet>> GetReferenceId()
        {
            var output = _ICommonDataService.GetReferenceId();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<AssetAssignmentToRDIGet>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<AssetAssignmentToRDIGet>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "References fetched successfully." };
        }

        public Envelope<List<string>> GetMaterialMasterCodeList()
        {
            var output = _ICommonDataService.GetMaterialMasterCodeList();
            return (output == null) ?
                new Envelope<List<string>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<List<string>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All material master code data fetched successfully." };
        }

        public Envelope<List<string>> GetDealerCodeList()
        {
            var output = _ICommonDataService.GetDealerCodeList();
            return (output == null) ?
                new Envelope<List<string>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<List<string>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "All dealer master code data fetched successfully." };
        }

        public Envelope<IEnumerable<LevelOfEducation>> GetLevelOfEducation()
        {
            var output = _ICommonDataService.GetLevelOfEducation();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<LevelOfEducation>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<LevelOfEducation>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "LevelofEducation fetched successfully." };
        }

        public Envelope<IEnumerable<Source>> GetSource()
        {
            var output = _ICommonDataService.GetSource();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<Source>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<Source>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Sources fetched successfully." };
        }

        public Envelope<IEnumerable<Gender>> GetGender()
        {
            var output = _ICommonDataService.GetGender();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<Gender>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<Gender>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Gender fetched successfully." };
        }

        public Envelope<IEnumerable<OutletType>> GetOutletType()
        {
            var output = _ICommonDataService.GetOutletType();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<OutletType>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<OutletType>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "OutletType fetched successfully." };
        }

        public Envelope<IEnumerable<IncentiveCategoryMaster>> GetAllIncentiveCategory()
        {
            var output = _ICommonDataService.GetAllIncentiveCategory();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<IncentiveCategoryMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<IncentiveCategoryMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Incentive Categories fetched successfully." };
        }

        public Envelope<IEnumerable<ModuleMaster>> GetAllModules()
        {
            var output = _ICommonDataService.GetAllModules();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ModuleMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ModuleMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Modules fetched successfully." };
        }

        public Envelope<IEnumerable<SubModuleMaster>> GetSubModulesByModuleId(SubModuleMasterGet InputParam)
        {
            var output = _ICommonDataService.GetSubModulesByModuleId(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SubModuleMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SubModuleMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Sub Modules fetched successfully." };
        }

        public Envelope<IEnumerable<FestivalIncentiveScheme>> GetAllFestivalScheme(FestivalIncentiveSchemeParam InputParam)
        {
            var output = _ICommonDataService.GetAllFestivalScheme(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<FestivalIncentiveScheme>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<FestivalIncentiveScheme>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Festival Schemes fetched successfully." };
        }

        public Envelope<IEnumerable<AttendanceType>> GetAllAttendanceType()
        {
            var output = _ICommonDataService.GetAllAttendanceType();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<AttendanceType>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<AttendanceType>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Attendance Type List fetched successfully." };
        }

        public Envelope<IEnumerable<DeviationReasons>> GetAllDeviationReasons()
        {
            var output = _ICommonDataService.GetAllDeviationReasons();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<DeviationReasons>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<DeviationReasons>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Deviation Reasons List fetched successfully." };
        }
		
		public Envelope<IEnumerable<ProductGroupCategoryMaster>> GetProductCategoryGroupDropDown()
        {
            var output = _ICommonDataService.GetProductCategoryGroupDropDown();
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<ProductGroupCategoryMaster>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ProductGroupCategoryMaster>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Product Category Groups fetched successfully." };
        }

        public Envelope<GetBranch> GetBranchByUserId(GetBranch InputParam)
        {
            var output = _ICommonDataService.GetBranchByUserId(InputParam);
            return (output == null) ?
                new Envelope<GetBranch> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<GetBranch> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Branch fetched successfully." };
        }

        public Envelope<IEnumerable<TrainingDropdown>> GetTrainings(GetTrainingDropdown InputParam)
        {
            var output = _ICommonDataService.GetTrainings(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<TrainingDropdown>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<TrainingDropdown>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Training dropdown fetched successfully." };
        }

        public Envelope<IEnumerable<SearchSIDOutput>> GetSIDFromBranchAndRoleForBroadcast(SearchSID InputParam)
        {
            var output = _ICommonDataService.GetSIDFromBranchAndRoleForBroadcast(InputParam);
            return (output == null || output.Count() == 0) ?
                new Envelope<IEnumerable<SearchSIDOutput>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SearchSIDOutput>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "SID fetched successfully." };
        }

        public Envelope<IEnumerable<SubCatgeory>> GetSubCategoryforMultipleProducts(SubCatgeoryByProduct InputParam)
        {
            var output = _ICommonDataService.GetSubCategoryforMultipleProducts(InputParam);
            return (output == null) ?
                new Envelope<IEnumerable<SubCatgeory>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<SubCatgeory>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "Sub categories fetched successfully." };
        }

        public Envelope<Branchfohr> GetBranchMappedForHR(GetBranch InputParam)
        {
            var output = _ICommonDataService.GetBranchMappedForHR(InputParam);
            return (output == null || output.BranchIds.Count == 0) ?
                new Envelope<Branchfohr> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<Branchfohr> { Data = output, MessageCode = (int)Acceptable.Found, Message = "branch fetched successfully." };
        }

        public Envelope<IEnumerable<ShiftTimingName>> GetShiftTiming()
        {
            var output = _ICommonDataService.GetShiftTiming();
            return (output == null) ?
                new Envelope<IEnumerable<ShiftTimingName>> { Data = output, MessageCode = (int)NotAcceptable.NotFound, Message = "No Data Found." } :
                new Envelope<IEnumerable<ShiftTimingName>> { Data = output, MessageCode = (int)Acceptable.Found, Message = "List of Shift." };
        }
    }
}
