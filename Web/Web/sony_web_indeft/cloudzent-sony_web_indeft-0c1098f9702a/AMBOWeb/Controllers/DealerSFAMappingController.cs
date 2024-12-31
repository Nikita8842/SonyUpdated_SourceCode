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
    public class DealerSFAMappingController : SonyBaseController
    {
        private readonly IAPIMappingData _APIMappingData;
        private readonly IAPICommon _APICommon;

        public DealerSFAMappingController(IAPIMappingData IAPIMappingData, IAPICommon IAPiCommon)
        {
            _APIMappingData = IAPIMappingData;
            _APICommon = IAPiCommon;
        }


        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.DealerSFAMapping, Rights = (int)Right.View)]
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

        public ActionResult NavigationIndex(string LoginId)
        {
            DealerSFAMapping param = new DealerSFAMapping();
            var details = _APIMappingData.GetSFADetails(LoginId);

            if (details != null)
            {                
                param.BranchId = details.BranchId;
                param.EmployeeId = details.EmployeeId;
                param.StateId = details.StateId;
                param.CityId = details.CityId;
                param.IsNavigated = true;
            }
            return View(param);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.DealerSFAMapping, Rights = (int)Right.Create)]
        public ActionResult Create(DealerSFAMapping mappingData)
        {
            var output = _APIMappingData.CreateDealerSFAMapping(mappingData);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.DealerSFAMapping, Rights = (int)Right.Edit)]
        public ActionResult Update(DealerSFAMapping mappingData)
        {
            var output = _APIMappingData.UpdateDealerSFAMapping(mappingData);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.DealerSFAMapping, Rights = (int)Right.Delete)]
        public ActionResult Delete(DealerSFAMapping mappingData)
        {
            var output = _APIMappingData.DeleteDealerSFAMapping(mappingData);
            return Json(output);
        }
    }
}