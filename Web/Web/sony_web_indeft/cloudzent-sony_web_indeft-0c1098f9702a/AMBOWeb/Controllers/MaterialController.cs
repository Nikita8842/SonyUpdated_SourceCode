using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIAccessLayer.INTERFACE;
using AMBOModels.GlobalAccessible;
using AMBOModels.Global;
using APIAccessLayer.Helper;
using AMBOModels.Mappings;
using AMBOWeb.Filters;
using static AMBOWeb.Classes.Common;
using AMBOModels.MasterMaintenance;

namespace AMBOWeb.Controllers
{
    public class MaterialController : SonyBaseController
    {

        private readonly IAPIMaterialData _APIMaterialData;
        private readonly IAPICommon _APICommon;

        public MaterialController(IAPIMaterialData IAPiMaterialData, IAPICommon IAPiCommon)
        {
            _APIMaterialData = IAPiMaterialData;
            _APICommon = IAPiCommon;
        }

        // GET: Material
        [SessionAuthorize(ModuleId = (Int64)Modules.Product, SubModuleId = (Int64)SubModules.Material, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Product, SubModuleId = (Int64)SubModules.Material, Rights = (int)Right.Create)]
        public ActionResult AddUpdateMaterial(string Operation, string MaterialId, string MaterialCode)
        {
            MaterialMaster Data = new MaterialMaster();
            if (Operation == "Update")
            {
                MaterialMaster InputData = new MaterialMaster();
                InputData.Id = Convert.ToInt64(MaterialId);
                Data = _APIMaterialData.GetMaterialById(InputData);
            }
            else
            {

                MaterialMaster InputData = new MaterialMaster();
                InputData.MaterialCode = MaterialCode;
                Data = _APIMaterialData.GetMaterialByMaterialCode(InputData);
                Data.MaterialCode = MaterialCode;
            }

            return View(Data);
        }


        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Product, SubModuleId = (Int64)SubModules.Material, Rights = (int)Right.Create)]
        public ActionResult AddUpdateMaterial(MaterialMaster InputParam)
        {
            //bug No 2404 : giving required message even if no data annotations have been applied on Size, Segment, 3D, Iternet, tv type an resolution
            //if (ModelState.IsValid)
            //{
            InputParam.CreatedBy = InputParam.UserId = objUserSession.UserId;
            InputParam.EncryptKey = objUserSession.EncryptKey;
            InputParam.RoleName = objUserSession.RoleName;
            Envelope<bool> output = new Envelope<bool>();
            if (InputParam.Id == 0)
            {
                output = _APIMaterialData.CreateMaterial(InputParam);
                if (output.Data)
                    AddSuccessNotification(output.Message);
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            else
            {
                output = _APIMaterialData.UpdateMaterial(InputParam);
                if (output.Data)
                    AddSuccessNotification(output.Message);
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            //}
            //else
            //{
            //    string errorMessages = "";
            //    foreach (ModelState modelState in ModelState.Values)
            //        foreach (ModelError error in modelState.Errors)
            //            errorMessages += error.ErrorMessage + " ";
            //    return Json(new Envelope<bool> { Data = false, Message = errorMessages, MessageCode = 0 });
            //}
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Product, SubModuleId = (Int64)SubModules.Material, Rights = (int)Right.Delete)]
        public JsonResult DeleteMaterial(string MaterialId = "0")
        {
            MaterialMaster InputParam = new MaterialMaster();
            InputParam.CreatedBy = Convert.ToInt64(Session["UserId"]);

            InputParam.Id = Convert.ToInt64(MaterialId);
            InputParam.CreatedBy = InputParam.UserId = objUserSession.UserId;
            InputParam.EncryptKey = objUserSession.EncryptKey;
            InputParam.RoleName = objUserSession.RoleName;

            var output = _APIMaterialData.DeleteMaterial(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMaterialCodeList()
        {
            List<string> data = _APICommon.GetMaterialMasterCodeList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult NavigationProDef(MaterialMaster param)
        {
            ProdDefUnderTargetCatGridData modelData = new ProdDefUnderTargetCatGridData();

            if (param != null)
            {
                modelData.MaterialCode = param.MaterialCode;
                modelData.MaterialName = param.Name;
                modelData.ProductCategoryId = param.ProductCategoryId;
                modelData.ProductSubCategoryId = param.ProductSubCategoryId;
                modelData.MOP = Convert.ToString(param.MOP);
            }

            return PartialView("NavigationProDef", modelData);
        }

    }
}