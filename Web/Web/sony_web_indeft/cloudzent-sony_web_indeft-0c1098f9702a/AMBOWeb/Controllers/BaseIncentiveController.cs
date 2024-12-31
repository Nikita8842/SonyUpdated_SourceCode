using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMBOModels.IncentiveManagement;
using APIAccessLayer.INTERFACE;
using AMBOWeb.Filters;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class BaseIncentiveController : SonyBaseController
    {
        private readonly IAPIIncentiveData _APIIncentiveData;

        public BaseIncentiveController(IAPIIncentiveData IAPIIncentiveData)
        {
            _APIIncentiveData = IAPIIncentiveData;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.BaseIncentiveDefinition, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.BaseIncentiveDefinition, Rights = (int)Right.Create)]
        public ActionResult Create()
        {
            return View(new CreateBaseIncentiveForm {
                TargetCategoryId = 0,
                Status = true,
                objDefinitionData = null
            });
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.BaseIncentiveDefinition, Rights = (int)Right.Edit)]
        public ActionResult Update(Int64 TargetCategoryId)
        {
            CreateBaseIncentiveForm objOutput;
            GetBaseIncentive objFormData = new GetBaseIncentive();
            objFormData.EncryptKey = objUserSession.EncryptKey;
            objFormData.RoleName = objUserSession.RoleName;
            objFormData.UserId = objUserSession.UserId;
            objFormData.TargetCategoryId = TargetCategoryId; 
            var output = _APIIncentiveData.GetBaseIncentiveDefinitionByTargetCategoryId(objFormData);
            if (output.Data != null)
                objOutput = output.Data;
            else
            {
                AddErrorNotification(output.Message);
                return RedirectToAction("Index");
            }
            return View("Create",objOutput);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.BaseIncentiveDefinition, Rights = (int)Right.Create)]
        public ActionResult Manage(CreateBaseIncentiveForm objFormData)
        {
            //add api call here
            var output = _APIIncentiveData.ManageBaseIncentiveDefinition(objFormData);
            if (output.Data)
                AddSuccessNotification(output.Message);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.BaseIncentiveDefinition, Rights = (int)Right.Delete)]
        public ActionResult Delete(DeleteBaseIncentiveForm objFormData)
        {
            //add api call here
            objFormData.EncryptKey = objUserSession.EncryptKey;
            objFormData.RoleName = objUserSession.RoleName;
            objFormData.UserId = objUserSession.UserId;

            var output = _APIIncentiveData.DeleteBaseIncentiveDefinition(objFormData);
            return Json(output);
        }
    }
}