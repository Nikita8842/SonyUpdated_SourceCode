using AMBOModels.Mappings;
using AMBOWeb.Filters;
using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class UserBranchChannelPCMappingController : SonyBaseController
    {
        private readonly IAPIMappingData _APIMappingData;

        public UserBranchChannelPCMappingController(IAPIMappingData IAPIMappingData)
        {
            _APIMappingData = IAPIMappingData;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Employee, SubModuleId = (Int64)SubModules.UserBranchChannelProCatMapping, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Employee, SubModuleId = (Int64)SubModules.UserBranchChannelProCatMapping, Rights = (int)Right.Create)]
        public ActionResult Create()
        {
            return View("Manage", new UserBranchChannelPCMapping());
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Employee, SubModuleId = (Int64)SubModules.UserBranchChannelProCatMapping, Rights = (int)Right.Edit)]
        public ActionResult Update(Int64 UserIdForMapping)
        {
            //get data on basis of user id from all mapping tables
            UserBranchChannelPCMapping objFormData = new UserBranchChannelPCMapping();
            objFormData = _APIMappingData.GetUserBranchChannelPCMapping(UserIdForMapping);
            objFormData.isUpdate = true;
            return View("Manage", objFormData);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Employee, SubModuleId = (Int64)SubModules.UserBranchChannelProCatMapping, Rights = (int)Right.Create)]
        public ActionResult Manage(UserBranchChannelPCMapping mappingData)
        {
            if (mappingData.BranchIds == null)
            {
                AddErrorNotification("Please Select Branch");
                return View(mappingData);
            }
            if (mappingData.ChannelIds == null)
            {
                AddErrorNotification("Please Select Channel");
                return View(mappingData);
            }

            var output = _APIMappingData.ManageUserBranchChannelPCMapping(mappingData);
            if (output.Data)
            {
                AddSuccessNotification(output.Message);
                //use sony base controller functions
                //TempData["Notification"] = output.Message;
                return RedirectToAction("Index");
            }
            mappingData.SubmitMessage = output.Message;
            return View(mappingData);
        }
    }
}