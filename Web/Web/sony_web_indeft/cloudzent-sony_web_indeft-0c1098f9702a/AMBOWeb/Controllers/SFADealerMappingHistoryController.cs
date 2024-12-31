using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;
using AMBOModels.GlobalAccessible;
using AMBOModels.Mappings;
using AMBOModels.MasterMaintenance;
using AMBOModels.UserManagement;
using AMBOWeb.Filters;
using APIAccessLayer.INTERFACE;
namespace AMBOWeb.Controllers
{
    public class SFADealerMappingHistoryController : SonyBaseController
    {
        private readonly IAPIMappingData _APIMappingData;
        private readonly IAPICommon _APICommon;

        public SFADealerMappingHistoryController(IAPIMappingData IAPIMappingData, IAPICommon IAPiCommon)
        {
            _APIMappingData = IAPIMappingData;
            _APICommon = IAPiCommon;
        }

        [HttpGet]
      //  [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.DealerSFAMapping, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            GetBranch InputParam = new GetBranch();
            DealerSFAMapping view = new DealerSFAMapping();
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
            // return View(new DealerSFAMapping());
        }

    }
}