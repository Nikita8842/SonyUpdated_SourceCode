using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIAccessLayer.INTERFACE;
using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using static AMBOWeb.Classes.Common;
using AMBOWeb.Filters;

namespace AMBOWeb.Controllers
{
    public class RoleRightMappingController : SonyBaseController
    {
        private readonly IAPIRoleRightsData _APIRoleRightsData;

        public RoleRightMappingController(IAPIRoleRightsData IAPIRoleRightsData)
        {
            _APIRoleRightsData = IAPIRoleRightsData;
        }

        // GET: RoleRightMapping
        [SessionAuthorize(ModuleId = (Int64)Modules.Security, SubModuleId = (Int64)SubModules.RoleRightMapping, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize(ModuleId = (Int64)Modules.Security, SubModuleId = (Int64)SubModules.RoleRightMapping, Rights = (int)Right.Create)]
        public ActionResult Create(RoleRightsMappingMaster input)
        {
            if (ModelState.IsValid)
            {
                input.UserId = objUserSession.UserId;
                var output = _APIRoleRightsData.CreateRoleRightsMapping(input);
                AddSuccessNotification(output.Message);
                return RedirectToAction("Index");
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
        [SessionAuthorize(ModuleId = (Int64)Modules.Security, SubModuleId = (Int64)SubModules.RoleRightMapping, Rights = (int)Right.Edit)]
        public ActionResult Update(RoleRightsMappingMaster input)
        {
            if (ModelState.IsValid)
            {
                input.UserId = objUserSession.UserId;
                var output = _APIRoleRightsData.UpdateRoleRightsMapping(input);
                AddSuccessNotification(output.Message);
                return RedirectToAction("Index");
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