using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMBOModels.Mappings;
using APIAccessLayer.INTERFACE;
using AMBOWeb.Filters;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class ProductDefinitionUnderTargetCategoryController : SonyBaseController
    {
        private readonly IAPIMappingData _APIMappingData;

        public ProductDefinitionUnderTargetCategoryController(IAPIMappingData IAPIMappingData)
        {
            _APIMappingData = IAPIMappingData;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.ProductDefinitionUnderTargetCategory, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.ProductDefinitionUnderTargetCategory, Rights = (int)Right.Create)]
        public ActionResult Search(ProdDefUnderTargetCat objSearchData)
        {
            var output = _APIMappingData.GetProductsByCategorySubCategory(objSearchData);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.ProductDefinitionUnderTargetCategory, Rights = (int)Right.Create)]
        public ActionResult Manage(ProdDefUnderTargetCatGridOutput objGridData)
        {
            //add api call here
            var output = _APIMappingData.ManageProductDefinitionUnderTargetCategory(objGridData);
            return Json(output);
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.ProductDefinitionUnderTargetCategory, Rights = (int)Right.View)]
        public ActionResult AllCategoryMapping()
        {
            return View();
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.ProductDefinitionUnderTargetCategory, Rights = (int)Right.Create)]
        public ActionResult SearchAllMaterial(ProdDefUnderTargetCatforAllMat objSearchData)
        {
            var output = _APIMappingData.GetProductsByCategorySubCategoryforallMat(objSearchData);
            return Json(output);
        }
    }
}