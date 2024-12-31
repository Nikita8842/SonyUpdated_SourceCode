using AMBOModels.GlobalAccessible;
using AMBOModels.Mappings;
using AMBOWeb.Filters;
using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class ProductCategorySFAMappingController : SonyBaseController
    {
        private readonly IAPIMappingData _APIMappingData;
        private readonly IAPICommon _APICommon;

        public ProductCategorySFAMappingController(IAPIMappingData IAPIMappingData, IAPICommon IAPiCommon)
        {
            _APIMappingData = IAPIMappingData;
            _APICommon = IAPiCommon;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.ProductCategorySFAMapping, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            GetBranch InputParam = new GetBranch();
            ProductCategorySFAMapping view = new ProductCategorySFAMapping();
            InputParam.UserId = objUserSession.UserId;
            string roleName = objUserSession.RoleName;

            if (roleName == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;
            return View(view);
        }

        public ActionResult GetProductCategorySFAMapFilters()
        {
            return PartialView("PartialViews/ProdCatSFAMapGridFilters");
        }

        [HttpPost]
        public ActionResult NavigationIndex(NavigationDealerSFAMapping param)
        {
            NavigationProductCategorySFAMapping modelData = new NavigationProductCategorySFAMapping();
            if (param != null)
            {
                modelData.BranchId = param.BranchId;
                modelData.DealerId = param.DealerId;
                modelData.DealerName = param.DealerName;
                modelData.EmployeeId = param.EmployeeId;
            }
            return PartialView("NavigationIndexProSFA", modelData);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.ProductCategorySFAMapping, Rights = (int)Right.View)]
        public ActionResult Create(ProductCategorySFAMapping mappingData)
        {
            var output = _APIMappingData.CreateProductCategorySFAMapping(mappingData);
            return Json(output);
        }

        
        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.ProductCategorySFAMapping, Rights = (int)Right.View)]
        public ActionResult Delete(ProductCategorySFAMapping mappingData)
        {
            var output = _APIMappingData.DeleteProductCategorySFAMapping(mappingData);
            return Json(output);
        }

        [HttpPost]
        public ActionResult NavigationCreateIncentiveSFAMapping(NavigationIncentiveCategorySFAMapping InputParam)
        {
            InputParam.UserId = objUserSession.UserId;
            var output = _APIMappingData.CreateIncentiveCategorySFAMapping(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult NavigationCreateProductSFAMapping(NavigationProductCategorySFAMapping InputParam)
        {
            ProductCategorySFAMapping mappingdata = new ProductCategorySFAMapping();
            mappingdata.UserId = objUserSession.UserId;
            mappingdata.ProductCategoryIds = InputParam.ProductCategoryIds;
            mappingdata.EmployeeId = InputParam.EmployeeId;
            var output = _APIMappingData.CreateProductCategorySFAMapping(mappingdata);
            return Json(output, JsonRequestBehavior.AllowGet);
        }


    }
}