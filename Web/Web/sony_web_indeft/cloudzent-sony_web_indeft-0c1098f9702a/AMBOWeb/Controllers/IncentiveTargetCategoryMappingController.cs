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
    public class IncentiveTargetCategoryMappingController : SonyBaseController
    {
        private readonly IAPIMappingData _APIMappingData;

        public IncentiveTargetCategoryMappingController(IAPIMappingData IAPIMappingData)
        {
            _APIMappingData = IAPIMappingData;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.IncentiveTargetCategoryMapping, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.IncentiveTargetCategoryMapping, Rights = (int)Right.Create)]
        public ActionResult Create()
        {
            return View("Manage", new IncentiveTargetCategoryMapping{ Status = true});
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.IncentiveTargetCategoryMapping, Rights = (int)Right.Create)]
        public ActionResult Update(Int64 IncentiveCatId)
        {
            //get data on basis of id from mapping tables
            IncentiveTargetCategoryMapping objFormData = new IncentiveTargetCategoryMapping();
            objFormData = _APIMappingData.GetIncentiveTargetCategoryMapping(IncentiveCatId);
            objFormData.isUpdate = true;
            return View("Manage", objFormData);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.IncentiveTargetCategoryMapping, Rights = (int)Right.Create)]
        public ActionResult Manage(IncentiveTargetCategoryMapping mappingData)
        {
            var output = _APIMappingData.ManageIncentiveTargetCategoryMapping(mappingData);
            if(output.Data)
                AddSuccessNotification(output.Message);
            //use sony base controller functions
            //TempData["Notification"] = output.Message;
            return Json(output);
        }
    }
}