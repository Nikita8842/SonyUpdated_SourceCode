using AMBOModels.MasterMaintenance;
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

    public class AssetController : SonyBaseController
    {
        private readonly IAPIAssetData _APIAssetData;

        public AssetController(IAPIAssetData IAPIAssetData)
        {
            _APIAssetData = IAPIAssetData;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Asset, SubModuleId = (Int64)SubModules.AssetManager, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Asset, SubModuleId = (Int64)SubModules.AssetManager, Rights = (int)Right.Create)]
        public ActionResult Create(AssetMaster objFormData)
        {
            var output = _APIAssetData.CreateAsset(objFormData);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Asset, SubModuleId = (Int64)SubModules.AssetManager, Rights = (int)Right.Edit)]
        public ActionResult Update(AssetMaster objFormData)
        {
            var output = _APIAssetData.UpdateAsset(objFormData);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Asset, SubModuleId = (Int64)SubModules.AssetManager, Rights = (int)Right.Delete)]
        public ActionResult Delete(AssetMaster objFormData)
        {
            var output = _APIAssetData.DeleteAsset(objFormData);
            return Json(output);
        }
    }
}