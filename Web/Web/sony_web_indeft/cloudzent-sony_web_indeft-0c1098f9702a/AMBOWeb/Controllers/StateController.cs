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
    public class StateController : SonyBaseController
    {
        private readonly IAPIStateData _APIStateData;

        public StateController(IAPIStateData IAPIStateData)
        {
            _APIStateData = IAPIStateData;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.State, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.State, Rights = (int)Right.Create)]
        public ActionResult Create(CreateStateForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APIStateData.CreateState(objFormData);
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
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.State, Rights = (int)Right.Edit)]
        public ActionResult Update(UpdateStateForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APIStateData.UpdateState(objFormData);
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
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.State, Rights = (int)Right.Delete)]
        public ActionResult Delete(DeleteStateForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APIStateData.DeleteState(objFormData);
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