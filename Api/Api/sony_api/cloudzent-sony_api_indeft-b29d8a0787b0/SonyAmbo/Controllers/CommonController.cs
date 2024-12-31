using AmboLibrary.GlobalAccessible;
using AmboLibrary.MasterMaintenance;
using AmboLibrary.UserManagement;
using AmboServices.Interface;
using AmboUtilities;
using AmboUtilities.Helper;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SonyAmbo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[ExceptionHandling]
    //[Compression]
    public class CommonController : ApiController
    {
        /// <summary>
        /// Fetching data by Id only 
        /// Kritika chadha
        /// </summary>
        private readonly ICommonService _ICommonService;

        /// <summary>
        /// Constructor
        /// </summary>
        public CommonController(ICommonService ICommonService)
        {
            _ICommonService = ICommonService;
        }
        #region Get Location data by Id's API

        /// <summary>
        /// Get Active States by Region.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetStatesByRegion(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetStatesByRegion(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active Cities by State.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetCityByState(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetCityByState(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active Cities.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllCities()
        {
            var output = _ICommonService.GetAllCities();
            return Ok(output);
        }

        /// <summary>
        /// Get Active Locations.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllLocations()
        {
            var output = _ICommonService.GetAllLocations();
            return Ok(output);
        }

        /// <summary>
        /// Get Active Location by City.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetLocationByCity(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetLocationByCity(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active Dealers by Location.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetDealersByLocation(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetDealersByLocation(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active Dealers NONSFA by Location.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetDealersByLocationNonSFA(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetDealersByLocationNonSFA(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active Dealers by Branch.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetDealersByBranch(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetDealersByBranch(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get all Active SFA.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllActiveSFA()
        {
            var output = _ICommonService.GetAllActiveSFA();
            return Ok(output);
        }

        /// <summary>
        /// Get all Active dealers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllActiveDealers()
        {
            var output = _ICommonService.GetAllActiveDealers();
            return Ok(output);
        }
        /// <summary>
        /// Get all Active dealers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllActiveDealersNonSFA()
        {
            var output = _ICommonService.GetAllActiveDealersNonSFA();
            return Ok(output);
        }
        


        /// <summary>
        /// Get all Active dealers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetActiveBrands()
        {
            var output = _ICommonService.GetActiveBrands();
            return Ok(output);
        }

        /// <summary>
        /// Get all Active dealers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetActiveFeedbackForms()
        {
            var output = _ICommonService.GetActiveFeedbackForms();
            return Ok(output);
        }

        /// <summary>
        /// Get Active Non SFA Dealer Master Codes by Branch.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetNonSFADealerMasterCodesByBranch(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetNonSFADealerMasterCodesByBranch(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active Non SFA Dealers by Master Codes.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetNonSFADealersByMasterCodes(Envelope<NonSFAMasterCodeList> InputParam)
        {
            var output = _ICommonService.GetNonSFADealersByMasterCodes(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active unmapped SFA by dealer.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSFAByDealer(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetSFAByDealer(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active unmapped SFA by dealer.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetSFADropdown()
        {
            var output = _ICommonService.GetSFADropdown();
            return Ok(output);
        }

        /// <summary>
        /// Get Active unmapped SFA by branch.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSFAByBranch(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetSFAByBranch(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active SFA by branch and division.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSFAFromDivisionAndBranch(Envelope<SearchSFA> InputParam)
        {
            var output = _ICommonService.GetSFAFromDivisionAndBranch(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active SID by branch for broadcasting message.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSIDFromBranchForBroadcast(Envelope<SearchSID> InputParam)
        {
            var output = _ICommonService.GetSIDFromBranchForBroadcast(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get all active users by role.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetAllActiveUsersByRole(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetAllActiveUsersByRole(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Sales PIC user list by branch.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSalesPICByBranch(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetSalesPICByBranch(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active RDI
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllActiveRDI()
        {
            var output = _ICommonService.GetAllActiveRDI();
            return Ok(output);
        }

        /// <summary>
        /// Get Active SFA levels.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetActiveSFALevels()
        {
            var output = _ICommonService.GetActiveSFALevels();
            return Ok(output);
        }

        /// <summary>
        /// Get Active unmapped SFA by branch.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetUnmappedSFAByBranch(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetUnmappedSFAByBranch(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active Product Sub Categories by Product Category Id.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetProductSubCategoryByCategoryId(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetProductSubCategoryByCategoryId(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active Region.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetRegion()
        {
            var output = _ICommonService.GetRegion();
            return Ok(output);
        }

        /// <summary>
        /// Get Active Channels.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetChannels()
        {
            var output = _ICommonService.GetChannels();
            return Ok(output);
        }

        /// <summary>
        /// Get Active States.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetStates()
        {
            var output = _ICommonService.GetStates();
            return Ok(output);
        }

        /// <summary>
        /// Get Active Role.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetRole()
        {
            var output = _ICommonService.GetRole();
            return Ok(output);
        }

        /// <summary>
        /// Get Dealer Classification Types.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDealerClassificationTypes()
        {
            var output = _ICommonService.GetDealerClassificationTypes();
            return Ok(output);
        }

        /// <summary>
        /// Get Active Agency.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetAgency(Envelope<AgencyDropdownInput> InputData)
        {
            var output = _ICommonService.GetAgency(InputData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active IncentiveCategory.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetIncentiveCategory()
        {
            var output = _ICommonService.GetIncentiveCategory();
            return Ok(output);
        }
        #endregion Get Location data by Id's API

        /// <summary>
        /// Get Active ProductTargetCategories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetProductTargetCategories()
        {
            var output = _ICommonService.GetProductTargetCategories();
            return Ok(output);
        }

        /// <summary>
        /// Get Active ProductTargetCategories by product category.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetTargetCategoriesByProductCategories(Envelope<ProductTargetCategorySearch> objSearchData)
        {
            var output = _ICommonService.GetTargetCategoriesByProductCategories(objSearchData.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active Target Types.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetTargetTypes()
        {
            var output = _ICommonService.GetTargetTypes();
            return Ok(output);
        }

        /// <summary>
        /// Get Active Divisions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDivisions()
        {
            var output = _ICommonService.GetDivisions();
            return Ok(output);
        }

        /// <summary>
        /// Get Active ProductCategories.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetProductCategories()
        {
            var output = _ICommonService.GetProductCategories();
            return Ok(output);
        }

        /// <summary>
        /// Get Active unmapped ProductCategories for a SFA.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetUnmappedProdCatsForSFA(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetUnmappedProdCatsForSFA(InputParam.Data);
            return Ok(output);
        }



        /// <summary>
        /// Get Active ProductCategoryForGroupMapping.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetProductCategoryForGroupMapping(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetProductCategoryForGroupMapping(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get Active SFALevels.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetSFALevels()
        {
            var output = _ICommonService.GetSFALevels();
            return Ok(output);
        }

        /// <summary>
        /// To get list of City Type.
        /// </summary>
        /// <returns>List of City Type.</returns>
        [HttpGet]
        public IHttpActionResult GetCityType()
        {
            var output = _ICommonService.GetCityType();
            return Ok(output);
        }

        /// <summary>
        /// To get list of Division list from material master.
        /// </summary>
        /// <returns>List of Division.</returns>
        [HttpGet]
        public IHttpActionResult GetDivisionForProductCategory()
        {
            var output = _ICommonService.GetDivisionForProductCategory();
            return Ok(output);
        }

        /// <summary>
        /// To get list of Product Category by division from material master.
        /// </summary>
        /// <returns>List of ProductCategory.</returns>
        [HttpPost]
        public IHttpActionResult GetProductCategoryByDivision(Envelope<Common> InputParam)
        {
            var output = _ICommonService.GetProductCategoryByDivision(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get list of Size.
        /// </summary>
        [HttpPost]
        public IHttpActionResult GetSize(Envelope<AttributeGet> InputParam)
        {
            var output = _ICommonService.GetSize(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get list of Segment.
        /// </summary>
        [HttpPost]
        public IHttpActionResult GetSegment(Envelope<AttributeGet> InputParam)
        {
            var output = _ICommonService.GetSegment(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get list of TVType.
        /// </summary>
        [HttpPost]
        public IHttpActionResult GetTVType(Envelope<AttributeGet> InputParam)
        {
            var output = _ICommonService.GetTVType(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get list of Resolution.
        /// </summary>
        [HttpPost]
        public IHttpActionResult GetResolution(Envelope<AttributeGet> InputParam)
        {
            var output = _ICommonService.GetResolution(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get list of 3D
        /// </summary>
        [HttpPost]
        public IHttpActionResult GetD3(Envelope<AttributeGet> InputParam)
        {
            var output = _ICommonService.GetD3(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get list of Internet
        /// </summary>
        [HttpPost]
        public IHttpActionResult GetInternet(Envelope<AttributeGet> InputParam)
        {
            var output = _ICommonService.GetInternet(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// Validating the material code is it valid or not
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ValidateMaterialCode(Envelope<Common> param)
        {
            var output = _ICommonService.ValidateMaterialCode(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get list of Competitors.
        /// </summary>
        /// <returns>List of Competitors.</returns>
        [HttpGet]
        public IHttpActionResult GetCompetitors()
        {
            var output = _ICommonService.GetCompetitors();
            return Ok(output);
        }

        /// <summary>
        /// To get Competitor's Product Categories.
        /// </summary>
        /// <param name="param">Competitor ID</param>
        /// <returns>Competitor's Product Categories.</returns>
        [HttpPost]
        public IHttpActionResult GetCompetitorProducts(Envelope<CompetitorProductsInput> param)
        {
            var output = _ICommonService.GetCompetitorProducts(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get list of Materials(Models).
        /// </summary>
        /// <returns>Materials(Models)</returns>
        [HttpGet]
        public IHttpActionResult GetMaterialDataforDD()
        {
            var output = _ICommonService.GetMaterialDataforDD();
            return Ok(output);
        }

        /// <summary>
        /// To get list of Materials(Models).
        /// </summary>
        /// <returns>Materials(Models)</returns>
        [HttpPost]
        public IHttpActionResult GetMaterialDataforDDByProSub(Envelope<MaterialDDInput> Input)
        {
            var output = _ICommonService.GetMaterialDataforDDByProSub(Input.Data);
            return Ok(output);
        }

        /// <summary>
        /// To fetch Sony Product Category and Sub Category data based on Competitor Category Id.
        /// </summary>
        /// <param name="param">Competitor Category Id.</param>
        /// <returns>Sony Product Category and Sub Category data</returns>
        [HttpPost]
        public IHttpActionResult GetSonyProducts(Envelope<CompetitorDataInput> param)
        {
            var output = _ICommonService.GetSonyProducts(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get list of SFAType(Models).
        /// </summary>
        /// <returns>SFAType(Models)</returns>
        [HttpGet]
        public IHttpActionResult GetSFAType()
        {
            var output = _ICommonService.GetSFAType();
            return Ok(output);
        }

        /// <summary>
        /// To get list of DisplayOrder(Models).
        /// </summary>
        /// <returns>DisplayOrder(Models)</returns>
        [HttpGet]
        public IHttpActionResult GetDisplayOrder()
        {
            var output = _ICommonService.GetDisplayOrder();
            return Ok(output);
        }
        /// <summary>
        /// Validating the SAP code is it valid or not
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ValidateSAPCode(Envelope<Common> param)
        {
            var output = _ICommonService.ValidateSAPCode(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// Get References for Asset Assignment To RDI
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetReferenceId()
        {
            var output = _ICommonService.GetReferenceId();
            return Ok(output);
        }

        /// <summary>
        /// Get list of material master code for typeahead
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMaterialMasterCodeList()
        {
            var output = _ICommonService.GetMaterialMasterCodeList();
            return Ok(output);
        }

        /// <summary>
        /// Get list of dealer master code for typeahead
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDealerCodeList()
        {
            var output = _ICommonService.GetDealerCodeList();
            return Ok(output);
        }

        /// <summary>
        /// to get list of level of education for dropdown
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetLevelOfEducation()
        {
            var output = _ICommonService.GetLevelOfEducation();
            return Ok(output);
        }

        /// <summary>
        /// to get list of source for dropdown
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetSource()
        {
            var output = _ICommonService.GetSource();
            return Ok(output);
        }

        /// <summary>
        /// to get gender for dropdown
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetGender()
        {
            var output = _ICommonService.GetGender();
            return Ok(output);
        }

        /// <summary>
        /// to get OutletType for dropdown
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetOutletType()
        {
            var output = _ICommonService.GetOutletType();
            return Ok(output);
        }

        /// <summary>
        /// to get incentive categories for dropdown
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllIncentiveCategory()
        {
            var output = _ICommonService.GetAllIncentiveCategory();
            return Ok(output);
        }

        /// <summary>
        /// to get list of all modules for dropdown
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllModules()
        {
            var output = _ICommonService.GetAllModules();
            return Ok(output);
        }

        /// <summary>
        /// to get list of all sub modules for dropdown by moduleid
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetSubModulesByModuleId(Envelope<SubModuleMasterGet> param)
        {
            var output = _ICommonService.GetSubModulesByModuleId(param.Data);
            return Ok(output);
        }

        /// <summary>
        /// to get list of all festival schemes
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetAllFestivalScheme(Envelope<FestivalIncentiveSchemeParam> InputParam)
        {
            var output = _ICommonService.GetAllFestivalScheme(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get all Attendance Type Data.
        /// Dhruv Sharma, ValueFirst, Gurgaon
        /// </summary>
        /// <returns>Attendance Type List</returns>
        [HttpGet]
        public IHttpActionResult GetAllAttendanceType()
        {
            var output = _ICommonService.GetAllAttendanceType();
            return Ok(output);
        }

        /// <summary>
        /// To get all Deviation Reasons Data.
        /// Nikhil Thakral, ValueFirst, Gurgaon
        /// </summary>
        /// <returns>Deviation Reasons List</returns>
        [HttpGet]
        public IHttpActionResult GetAllDeviationReasons()
        {
            var output = _ICommonService.GetAllDeviationReasons();
            return Ok(output);
        }
		
		/// <summary>
        /// to get all product category groups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetProductCategoryGroupDropDown()
        {
            var output = _ICommonService.GetProductCategoryGroupDropDown();
            return Ok(output);
        }

        /// <summary>
        /// To get Branch details of a RDI or any particular user.
        /// Bela Nalavade, ValueFirst, Gurgaon
        /// </summary>
        /// <returns>branch</returns>
        [HttpPost]
        public IHttpActionResult GetBranchByUserId(Envelope<GetBranch> InputParam)
        {
            var output = _ICommonService.GetBranchByUserId(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get all training(s) or filter by branch/channel.
        /// Bela Nalavade, ValueFirst, Gurgaon
        /// </summary>
        /// <returns>branch</returns>
        [HttpPost]
        public IHttpActionResult GetTrainings(Envelope<GetTrainingDropdown> InputParam)
        {
            var output = _ICommonService.GetTrainings(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get all SFA(s) or filter by branch/Division/role.
        /// Ehtesham Ansari, ValueFirst, Gurgaon
        /// </summary>
        /// <returns>sfa</returns>

        [HttpPost]
        public IHttpActionResult GetSFAFromDivisionAndBranchAndRole(Envelope<SearchSFA> InputParam)
        {
            var output = _ICommonService.GetSFAFromDivisionAndBranchAndRole(InputParam.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetSIDFromBranchAndRoleForBroadcast(Envelope<SearchSID> InputParam)
        {
            var output = _ICommonService.GetSIDFromBranchAndRoleForBroadcast(InputParam.Data);
            return Ok(output);
        }

        [HttpPost]
        public IHttpActionResult GetSubCategoryforMultipleProducts(Envelope<SubCatgeoryByProduct> InputParam)
        {
            var output = _ICommonService.GetSubCategoryforMultipleProducts(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// To get Branch for Regional HR.
        /// Bela Nalavade, ValueFirst, Gurgaon
        /// </summary>
        /// <returns>branch</returns>
        [HttpPost]
        public IHttpActionResult GetBranchMappedForHR(Envelope<GetBranch> InputParam)
        {
            var output = _ICommonService.GetBranchMappedForHR(InputParam.Data);
            return Ok(output);
        }

        /// <summary>
        /// This is for mapping shift in dealer.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetShiftTiming()
        {
            var output = _ICommonService.GetShiftTiming();
            return Ok(output);
        }

    }
}