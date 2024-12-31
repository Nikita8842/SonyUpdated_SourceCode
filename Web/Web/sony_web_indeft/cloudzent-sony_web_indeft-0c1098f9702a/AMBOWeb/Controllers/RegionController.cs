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
    public class RegionController : SonyBaseController
    {
        private readonly IAPIRegionData _APIRegionData;
        

        public RegionController(IAPIRegionData IAPIRegionData)
        {
            _APIRegionData = IAPIRegionData;
        }
        
        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId =(Int64)SubModules.Region, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.Region, Rights = (int)Right.Create)]
        public ActionResult Create(CreateRegionForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APIRegionData.CreateRegion(objFormData);
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
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.Region, Rights = (int)Right.Edit)]
        public ActionResult Update(UpdateRegionForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APIRegionData.UpdateRegion(objFormData);
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
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.Region, Rights = (int)Right.Delete)]
        public ActionResult Delete(DeleteRegionForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APIRegionData.DeleteRegion(objFormData);
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