using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIAccessLayer.INTERFACE;
using AMBOModels.MasterMaintenance;
using AMBOModels.GlobalAccessible;
using APIAccessLayer.Helper;
using static AMBOWeb.Classes.Common;
using AMBOWeb.Filters;

namespace AMBOWeb.Controllers
{
    public class TargetDateSettingMasterController : SonyBaseController
    {
        private readonly IAPITargetDateSettingMaster _APITargetDateSettingMaster;
        public static IAPICommon _Common;
        public readonly IAPIGridData _APIGridData;

        public TargetDateSettingMasterController(IAPITargetDateSettingMaster IAPITargetDateSettingMaster, IAPICommon IAPICommon, IAPIGridData IAPIGridData)
        {
            _APITargetDateSettingMaster = IAPITargetDateSettingMaster;
            _Common = IAPICommon;
            _APIGridData = IAPIGridData;
        }

        // GET: TargetDateSettingMaster
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.TargetDateSettingMaster, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.TargetDateSettingMaster, Rights = (int)Right.Edit)]
        public ActionResult AddUpdateTargetUpdateDate()
        {            
            return View();
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFAScheme, SubModuleId = (Int64)SubModules.TargetDateSettingMaster, Rights = (int)Right.Edit)]
        public ActionResult AddUpdateTargetUpdateDate(TargetDateSettingMaster objFormData)
        {            
            Envelope<bool> createdoutput=null;
            objFormData.UserId = objUserSession.UserId;
            objFormData.EncryptKey = objUserSession.EncryptKey;
            objFormData.RoleName = objUserSession.RoleName;

            createdoutput = _APITargetDateSettingMaster.UpdateTargetDateSettingMaster(objFormData);
            if (createdoutput.MessageCode == (int)Acceptable.Accepted)
                AddSuccessNotification(createdoutput.Message);
            else
                AddErrorNotification(createdoutput.Message);
            return RedirectToAction("Index");
        }

        
    }
}