using AMBOModels.Global;
using AMBOModels.MasterMaintenance;
using AMBOWeb.Filters;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class LocationController : SonyBaseController
    {
        private readonly IAPILocationData _APILocationData;

        public LocationController(IAPILocationData IAPILocationData)
        {
            _APILocationData = IAPILocationData;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.Location, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.Location, Rights = (int)Right.Create)]
        public ActionResult Create(CreateLocationForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APILocationData.CreateLocation(objFormData);
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
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.Location, Rights = (int)Right.Delete)]
        public ActionResult Delete(DeleteLocationForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APILocationData.DeleteLocation(objFormData);
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
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.Location, Rights = (int)Right.Edit)]
        public ActionResult Update(UpdateLocationForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APILocationData.UpdateLocation(objFormData);
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