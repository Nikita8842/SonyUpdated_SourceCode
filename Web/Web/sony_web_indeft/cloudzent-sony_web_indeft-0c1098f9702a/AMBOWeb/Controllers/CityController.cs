using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;
using AMBOModels.GlobalAccessible;
using AMBOModels.MasterMaintenance;
using AMBOWeb.Filters;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;

namespace AMBOWeb.Controllers
{
    /// <summary>
    /// To perform city master realted operations.
    /// </summary>
    
    public class CityController : SonyBaseController
    {
        private readonly IAPICityData _APICityData;
        
        public CityController(IAPICityData IAPICityData)
        {
            _APICityData = IAPICityData;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.City, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.City, Rights = (int)Right.Create)]
        public ActionResult Create(CreateCityForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APICityData.CreateCity(objFormData);
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
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.City, Rights = (int)Right.Edit)]
        public ActionResult Update(UpdateCityForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APICityData.UpdateCity(objFormData);
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
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.City, Rights = (int)Right.Delete)]
        public ActionResult Delete(DeleteCityForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APICityData.DeleteCity(objFormData);
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

        [HttpGet]
        public JsonResult GetAllCities()
        {
            var output = _APICityData.GetAllCities();
            return Json(output, JsonRequestBehavior.AllowGet);
        }
    }
}