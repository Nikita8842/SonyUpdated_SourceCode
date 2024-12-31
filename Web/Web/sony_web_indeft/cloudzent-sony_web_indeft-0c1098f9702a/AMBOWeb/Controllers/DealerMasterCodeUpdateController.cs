using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMBOModels.Global;
using APIAccessLayer.INTERFACE;
using AMBOModels.Modules;
using APIAccessLayer.Helper;
using static AMBOWeb.Classes.Common;
using AMBOWeb.Filters;

namespace AMBOWeb.Controllers
{
    public class DealerMasterCodeUpdateController : SonyBaseController
    {
        private readonly IAPIDealerMasterCodeUpdate _DealerMasterCodeUpdate;

        public DealerMasterCodeUpdateController(IAPIDealerMasterCodeUpdate _IAPIDealerMasterCodeUpdate)
        {
            _DealerMasterCodeUpdate = _IAPIDealerMasterCodeUpdate;
        }

        // GET: DealerMasterCodeUpdate
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.DealerMasterCodeUpdation, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ValidateDealerMasterCode(string mastercode)
        {
            DealerMasterCodeUpdate Input = new DealerMasterCodeUpdate();            
            Input.MasterCode = mastercode;
            var validateoutput = _DealerMasterCodeUpdate.ValidateDealerMasterCode(Input);
         
            return Json(validateoutput, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.DealerMasterCodeUpdation, Rights = (int)Right.Edit)]
        public ActionResult GetDealerMasterCode(string mastercode)
        {
            DealerMasterCodeUpdate Input = new DealerMasterCodeUpdate();
            Input.MasterCode = mastercode;

            return View(Input);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.DealerMasterCodeUpdation, Rights = (int)Right.Edit)]
        public ActionResult UpdateDealerMasterCode(DealerMasterCodeUpdate InputParam)
        {
            if (ModelState.IsValid)
            {
                Envelope<bool> output = null;
                InputParam.UserId = objUserSession.UserId;
                InputParam.EncryptKey = objUserSession.EncryptKey;
                InputParam.RoleName = objUserSession.RoleName;

                output = _DealerMasterCodeUpdate.UpdateDealerMasterCode(InputParam);
                if (output.MessageCode == (int)Acceptable.Accepted)
                    AddSuccessNotification(output.Message);
                else
                    AddErrorNotification(output.Message);

                return Json(output, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string errorMessages = "";
                foreach (ModelState modelState in ModelState.Values)
                    foreach (ModelError error in modelState.Errors)
                        errorMessages += error.ErrorMessage + " ";
                return Json(new Envelope<bool> { Data = false, Message = errorMessages, MessageCode = 0 });
            }
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.DealerMasterCodeUpdation, Rights = (int)Right.Edit)]
        public ActionResult GetDealerByMasterCode(string mastercode)
        {
            DealerMasterCodeUpdate dealerdetails = new DealerMasterCodeUpdate();
            dealerdetails.MasterCode = mastercode;
            var output = _DealerMasterCodeUpdate.GetDealerByMasterCode(dealerdetails);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDealerMasterCodeList()
        {
            List<string> data = _DealerMasterCodeUpdate.GetDealerMasterCodeList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}