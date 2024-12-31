using AMBOModels.GlobalAccessible;
using AMBOModels.UserManagement;
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
    public class SFABranchChangeHistoryController : SonyBaseController
    {
        private readonly IAPISFAData _APISFAData;
        private readonly IAPICommon _APICommon;

        public SFABranchChangeHistoryController(IAPISFAData IAPISFAData, IAPICommon IAPICommon)
        {
            _APISFAData = IAPISFAData;
            _APICommon = IAPICommon;
        }

        public ActionResult GetSFAMasterGridFilters()
        {
            return PartialView("PartialViews/SFABranchChangeHistoryGridFilters");
        }

        [HttpGet]
        //[SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFAMaster, Rights = (int)Right.View)]

        public ActionResult Index()
        {
            GetBranch InputParam = new GetBranch();
            UserBasicProperties view = new UserBasicProperties();
            InputParam.UserId = objUserSession.UserId;
            string roleName = objUserSession.RoleName;

            if (roleName == "RDI")
            {
                var details = _APICommon.GetBranchByUserId(InputParam);
                view.BranchId = InputParam.BranchId = details.BranchId;
            }
            else
                view.BranchId = 0;

            return View(view);
        }

    }
}