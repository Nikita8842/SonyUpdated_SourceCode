using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;
using AMBOModels.MasterMaintenance;
using AMBOWeb.Filters;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;

namespace AMBOWeb.Controllers
{
    public class ProductSubCategoryController : SonyBaseController
    {
        private readonly IAPIProductSubCategoryData _APIProductSubCategoryData;

        public ProductSubCategoryController(IAPIProductSubCategoryData IAPIProductSubCategoryData)
        {
            _APIProductSubCategoryData = IAPIProductSubCategoryData;
        }

        [SessionAuthorize(ModuleId = (Int64)Modules.Product, SubModuleId = (Int64)SubModules.ProductSubCat, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize(ModuleId = (Int64)Modules.Product, SubModuleId = (Int64)SubModules.ProductSubCat, Rights = (int)Right.Create)]
        public ActionResult Create(CreateProductSubCategoryForm objFormData)
        {
            if (ModelState.IsValid)
            {
                var output = _APIProductSubCategoryData.CreateProductSubCategory(objFormData);
                return Json(output);
            }
            else
            {
                string errorMessages = "";
                foreach (ModelState modelState in ModelState.Values)
                    foreach (ModelError error in modelState.Errors)
                        errorMessages += error.ErrorMessage + " ";
                return Json(new Envelope<bool> { Data = false, Message = errorMessages, MessageCode = 0 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize(ModuleId = (Int64)Modules.Product, SubModuleId = (Int64)SubModules.ProductSubCat, Rights = (int)Right.Edit)]
        public ActionResult Update(UpdateProductSubCategoryForm objFormData)
        {
            if (ModelState.IsValid)
            {
                var output = _APIProductSubCategoryData.UpdateProductSubCategory(objFormData);
                return Json(output);
            }
            else
            {
                string errorMessages = "";
                foreach (ModelState modelState in ModelState.Values)
                    foreach (ModelError error in modelState.Errors)
                        errorMessages += error.ErrorMessage + " ";
                return Json(new Envelope<bool> { Data = false, Message = errorMessages, MessageCode = 0 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize(ModuleId = (Int64)Modules.Product, SubModuleId = (Int64)SubModules.ProductSubCat, Rights = (int)Right.Delete)]
        public ActionResult Delete(DeleteProductSubCategoryForm objFormData)
        {
            if (ModelState.IsValid)
            {
                var output = _APIProductSubCategoryData.DeleteProductSubCategory(objFormData);
                return Json(output);
            }
            else
            {
                string errorMessages = "";
                foreach (ModelState modelState in ModelState.Values)
                    foreach (ModelError error in modelState.Errors)
                        errorMessages += error.ErrorMessage + " ";
                return Json(new Envelope<bool> { Data = false, Message = errorMessages, MessageCode = 0 });
            }
        }
    }
}