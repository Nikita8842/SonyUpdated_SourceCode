using System.Web.Mvc;
using APIAccessLayer.INTERFACE;
using System;
using APIAccessLayer.Helper;
using AMBOWeb.Filters;
using static AMBOWeb.Classes.Common;
using AMBOModels.MasterMaintenance;

namespace AMBOWeb.Controllers
{
    public class BranchController : SonyBaseController
    {

        private readonly IAPIBranchData _APIBranchData;

        public BranchController(IAPIBranchData IAPIBranchData)
        {
            _APIBranchData = IAPIBranchData;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.Branch, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.Branch, Rights = (int)Right.Create)]
        public ActionResult Create(CreateBranchForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APIBranchData.CreateBranch(objFormData);
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
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.Branch, Rights = (int)Right.Edit)]
        public ActionResult Update(UpdateBranchForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APIBranchData.UpdateBranch(objFormData);
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
        [SessionAuthorize(ModuleId = (Int64)Modules.Location, SubModuleId = (Int64)SubModules.Branch, Rights = (int)Right.Delete)]
        public ActionResult Delete(DeleteBranchForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APIBranchData.DeleteBranch(objFormData);
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