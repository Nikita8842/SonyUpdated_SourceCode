using AMBOModels.Abstract;
using AMBOModels.Global;
using AMBOModels.GlobalAccessible;
using AMBOModels.MasterMaintenance;
using AMBOModels.UserManagement;
using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AMBOWeb.Controllers
{
    public class CommonController : SonyBaseController
    {
        private readonly IAPICommon _APICommon;

        public CommonController(IAPICommon IAPICommon)
        {
            _APICommon = IAPICommon;
        }

        [HttpGet]
        public JsonResult GetRegion()
        {
            var output = _APICommon.GetRegion();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetChannels()
        {
            var output = _APICommon.GetChannels();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetStates()
        {
            var output = _APICommon.GetStates();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllCities()
        {
            var output = _APICommon.GetAllCities();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllLocations()
        {
            var output = _APICommon.GetAllLocations();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCityTypes()
        {
            var output = _APICommon.GetCityType();
            return Json(output.TypeList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDivisions()
        {
            var output = _APICommon.GetDivisions();
            return Json(output,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSizes(AttributeGet InputParam)
        {
            var output = _APICommon.GetSize(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSegment(AttributeGet InputParam)
        {
            var output = _APICommon.GetSegment(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetResolution(AttributeGet InputParam)
        {
            var output = _APICommon.GetResolution(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProductCategoryForGroupMapping(Common InputParam)
        {
            var output = _APICommon.GetProductCategoryForGroupMapping(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        

        [HttpPost]
        public JsonResult GetTVType(AttributeGet InputParam)
        {
            var output = _APICommon.GetTVType(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetInternet(AttributeGet InputParam)
        {
            var output = _APICommon.GetInternet(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Get3D(AttributeGet InputParam)
        {
            var output = _APICommon.Get3D(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductCategories()
        {
            var output = _APICommon.GetProductCategories();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductTargetCategories()
        {
            var output = _APICommon.GetProductTargetCategories();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTargetCategoriesByProductCategories(ProductTargetCategorySearch objSearchData)
        {
            objSearchData.UserId = objUserSession.UserId;
            objSearchData.EncryptKey = objUserSession.EncryptKey;
            objSearchData.RoleName = objUserSession.RoleName;
            var output = _APICommon.GetTargetCategoriesByProductCategories(objSearchData);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTargetTypes()
        {
            var output = _APICommon.GetTargetTypes();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUnmappedProdCatsForSFA(Int64 sfaId)
        {
            Common InputParam = new Common();
            InputParam.Id = sfaId;
            var output = _APICommon.GetUnmappedProdCatsForSFA(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetStateByRegion(int regionId)
        {
            Common InputParam = new Common();
            InputParam.Id = regionId;
            var output = _APICommon.GetStateByRegion(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCityByState(int stateId)
        {
            Common InputParam = new Common();
            InputParam.Id = stateId;
            var output = _APICommon.GetCityByState(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProductSubCategoryByCategoryId(int CategoryId)
        {
            Common InputParam = new Common();
            InputParam.Id = CategoryId;
            var output = _APICommon.GetProductSubCategoryByCategoryId(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLocationByCity(int cityId)
        {
            Common InputParam = new Common();
            InputParam.Id = cityId;
            var output = _APICommon.GetLocationByCity(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDealersByLocation(int locationId)
        {
            Common InputParam = new Common();
            InputParam.Id = locationId;
            var output = _APICommon.GetDealersByLocation(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDealersByLocationNonSFA(int locationId)
        {
            Common InputParam = new Common();
            InputParam.Id = locationId;
            var output = _APICommon.GetDealersByLocationNonSFA(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDealersByBranch(int branchId)
        {
            Common InputParam = new Common();
            InputParam.Id = branchId;
            var output = _APICommon.GetDealersByBranch(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllActiveSFA()
        {
            var output = _APICommon.GetAllActiveSFA();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllActiveDealers()
        {
            var output = _APICommon.GetAllActiveDealers();
            return Json(output, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetAllActiveDealersNonSFA()
        {
            var output = _APICommon.GetAllActiveDealersNonSFA();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetActiveBrands()
        {
            var output = _APICommon.GetActiveBrands();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetActiveFeedbackForms()
        {
            var output = _APICommon.GetActiveFeedbackForms();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetNonSFADealerMasterCodesByBranch(Int64 branchId)
        {
            Common InputParam = new Common();
            InputParam.Id = branchId;
            var output = _APICommon.GetNonSFADealerMasterCodesByBranch(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetNonSFADealersByMasterCodes(NonSFAMasterCodeList objInput)
        {
            var output = _APICommon.GetNonSFADealersByMasterCodes(objInput);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSFAByDealer(int dealerId)
        {
            Common InputParam = new Common();
            InputParam.Id = dealerId;
            var output = _APICommon.GetSFAByDealer(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSFADropdown()
        {
            var output = _APICommon.GetSFADropdown();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAllActiveUsersByRole(Int64 roleId)
        {
            Common InputParam = new Common();
            InputParam.Id = roleId;
            var output = _APICommon.GetAllActiveUsersByRole(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSalesPICByBranch(Int64 branchId)
        {
            Common InputParam = new Common();
            InputParam.Id = branchId;
            var output = _APICommon.GetSalesPICByBranch(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllActiveRDI()
        {
            var output = _APICommon.GetAllActiveRDI();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetActiveSFALevels()
        {
            var output = _APICommon.GetActiveSFALevels();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSFAByBranch(int branchId)
        {
            Common InputParam = new Common();
            InputParam.Id = branchId;
            var output = _APICommon.GetSFAByBranch(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSFAFromDivisionAndBranch(SearchSFA objDataForSearch)
        {
            objDataForSearch.UserId = objUserSession.UserId;
            objDataForSearch.EncryptKey = objUserSession.EncryptKey;
            objDataForSearch.RoleName = objUserSession.RoleName;
            var output = _APICommon.GetSFAFromDivisionAndBranch(objDataForSearch);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSIDFromBranchForBroadcast(SearchSID objDataForSearch)
        {
            objDataForSearch.UserId = objUserSession.UserId;
            objDataForSearch.EncryptKey = objUserSession.EncryptKey;
            objDataForSearch.RoleName = objUserSession.RoleName;
            var output = _APICommon.GetSIDFromBranchForBroadcast(objDataForSearch);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUnmappedSFAByBranch(int branchId)
        {
            Common InputParam = new Common();
            InputParam.Id = branchId;
            var output = _APICommon.GetUnmappedSFAByBranch(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetRoles()
        {
            var output = _APICommon.GetRoles();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDealerClassificationTypes()
        {
            var output = _APICommon.GetDealerClassificationTypes();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBranches()
        {
            var output = _APICommon.GetBranch();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAgency(AgencyDropdownInput InputData)
        {
            var output = _APICommon.GetAgency(InputData);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSFALevels()
        {
            var output = _APICommon.GetSFALevels();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSFAType()
        {
            var output = _APICommon.GetSFAType();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProductCategoryByDivision(string divisionId)
        {
            Common InputParam = new Common();
            InputParam.Value = divisionId;
            var output = _APICommon.GetProductCategoryByDivision(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetDivisionForProductCategory()
        {
            var output = _APICommon.GetDivisionForProductCategory();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidateMaterialCode(string materialCode)
        {
            string Message = "";
            ErrorModel Error = new ErrorModel();
            Common Input = new Common();
            Input.Value = materialCode;
            Error.ErrorCode = _APICommon.ValidateMaterialCode(Input,out Message);
            Error.ErrorMessage = Message;
            return Json(Error, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidateSAPCode(string sapCode)
        {
            Common Input = new Common();
            Input.Value = sapCode;
            var output = _APICommon.ValidateSAPCode(Input);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetReferenceId()
        {
            var output = _APICommon.GetReferenceId();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLevelOfEducation()
        {
            var output = _APICommon.GetLevelOfEducation();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSource()
        {
            var output = _APICommon.GetSource();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGender()
        {
            var output = _APICommon.GetGender();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetOutletType()
        {
            var output = _APICommon.GetOutletType();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllIncentiveCategory()
        {
            var output = _APICommon.GetAllIncentiveCategory();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCompetitors()
        {
            var output = _APICommon.GetCompetitors();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMaterialDataforDD()
        {
            var output = _APICommon.GetMaterialDataforDD();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetMaterialDataforDDByProSub(MaterialDDInput Input)
        {
            var output = _APICommon.GetMaterialDataforDDByProSub(Input);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllModules()
        {
            var output = _APICommon.GetAllModules();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSubModulesByModuleId(SubModuleMasterGet InputParam)
        {
            var output = _APICommon.GetSubModulesByModuleId(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAllFestivalScheme(FestivalIncentiveSchemeParam InputParam)
        {
            var output = _APICommon.GetAllFestivalScheme(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllAttendanceType()
        {
            var output = _APICommon.GetAllAttendanceType();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllDeviationReasons()
        {
            var output = _APICommon.GetAllDeviationReasons();
            return Json(output, JsonRequestBehavior.AllowGet);
        }
		
		[HttpGet]
        public JsonResult GetProductCategoryGroupDropDown()
        {
            var output = _APICommon.GetProductCategoryGroupDropDown();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDisplayOrder()
        {
            var output = _APICommon.GetDisplayOrder();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTrainings(GetTrainingDropdown InputParam)
        {
            var output = _APICommon.GetTrainings(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSFAFromDivisionAndBranchAndRole(SearchSFA objDataForSearch)
        {
            objDataForSearch.UserId = objUserSession.UserId;
            objDataForSearch.EncryptKey = objUserSession.EncryptKey;
            objDataForSearch.RoleName = objUserSession.RoleName;
            var output = _APICommon.GetSFAFromDivisionAndBranchAndRole(objDataForSearch);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSIDFromBranchAndRoleForBroadcast(SearchSID objDataForSearch)
        {
            objDataForSearch.UserId = objUserSession.UserId;
            objDataForSearch.EncryptKey = objUserSession.EncryptKey;
            objDataForSearch.RoleName = objUserSession.RoleName;
            var output = _APICommon.GetSIDFromBranchAndRoleForBroadcast(objDataForSearch);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSubCategoryforMultipleProducts(SubCatgeoryByProduct objDataForSearch)
        {
            var output = _APICommon.GetSubCategoryforMultipleProducts(objDataForSearch);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetShiftTiming()
        {
            var output = _APICommon.GetShiftTiming();
            return Json(output, JsonRequestBehavior.AllowGet);
        }
    }
}