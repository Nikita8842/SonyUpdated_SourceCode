using AMBOModels.Mappings;
using AMBOWeb.Filters;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class DealerClassificationMappingController : SonyBaseController
    {
        private readonly IAPIMappingData _APIMappingData;

        public DealerClassificationMappingController(IAPIMappingData IAPIMappingData)
        {
            _APIMappingData = IAPIMappingData;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.DealerClassificationMapping, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.DealerClassificationMapping, Rights = (int)Right.Create)]
        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(DealerClassificationMappingSearch objSearchData)
        {
            objSearchData.UserId = objUserSession.UserId;
            objSearchData.EncryptKey = objUserSession.EncryptKey;
            objSearchData.RoleName = objUserSession.RoleName;
            var output = _APIMappingData.GetDealerClassificationMappingTable(objSearchData);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.DealerClassificationMapping, Rights = (int)Right.Create)]
        public ActionResult Manage(DealerClassificationMappingTable mappingData)
        {
            mappingData.UserId = objUserSession.UserId;
            mappingData.EncryptKey = objUserSession.EncryptKey;
            mappingData.RoleName = objUserSession.RoleName;
            var output = _APIMappingData.CreateDealerClassificationMapping(mappingData);
            if (output.MessageCode == (int)Acceptable.Created)
                AddSuccessNotification(output.Message);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.DealerClassificationMapping, Rights = (int)Right.Edit)]
        public ActionResult Update(DealerClassificationMapping mappingData)
        {
            mappingData.UserId = objUserSession.UserId;
            mappingData.EncryptKey = objUserSession.EncryptKey;
            mappingData.RoleName = objUserSession.RoleName;
            var output = _APIMappingData.UpdateDealerClassificationMapping(mappingData);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.DealerClassificationMapping, Rights = (int)Right.Delete)]
        public ActionResult Delete(DealerClassificationMapping mappingData)
        {
            mappingData.UserId = objUserSession.UserId;
            mappingData.EncryptKey = objUserSession.EncryptKey;
            mappingData.RoleName = objUserSession.RoleName;
            var output = _APIMappingData.DeleteDealerClassificationMapping(mappingData);
            return Json(output);
        }
    }
}