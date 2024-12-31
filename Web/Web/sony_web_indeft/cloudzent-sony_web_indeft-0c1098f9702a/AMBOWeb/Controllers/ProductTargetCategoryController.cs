using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.INTERFACE;
using AMBOWeb.Filters;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class ProductTargetCategoryController : SonyBaseController
    {
        private readonly IAPIProductTargetCategoryData _APIProductTargetCategoryData;

        public ProductTargetCategoryController(IAPIProductTargetCategoryData IAPIProductTargetCategoryData)
        {
            _APIProductTargetCategoryData = IAPIProductTargetCategoryData;
        }

        public ActionResult GetProductTargetCategoryGridFilters()
        {
            return PartialView("PartialViews/ProductTargetCategoryGridFilters");
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.ProductTargetCategoryMapping, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.ProductTargetCategoryMapping, Rights = (int)Right.Delete)]
        public ActionResult Delete(ProductTargetCategoryMaster objFormData)
        {
            var output = _APIProductTargetCategoryData.DeleteProductTargetCategoryMapping(objFormData);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.ProductTargetCategoryMapping, Rights = (int)Right.Create)]
        public ActionResult Create(ProductTargetCategoryMaster objFormData)
        {
            var output = _APIProductTargetCategoryData.CreateProductTargetCategoryMapping(objFormData);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.ProductTargetCategoryMapping, Rights = (int)Right.Edit)]
        public ActionResult Update(ProductTargetCategoryMaster objFormData)
        {
            var output = _APIProductTargetCategoryData.UpdateProductTargetCategoryMapping(objFormData);
            return Json(output);
        }


    }
}