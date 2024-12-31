using System;
using System.Linq;
using System.Web.Mvc;
using AMBOModels.MasterMaintenance;
using System.Collections.Generic;
using APIAccessLayer.INTERFACE;
using AMBOModels.GlobalAccessible;
using APIAccessLayer.Helper;
using static AMBOWeb.Classes.Common;
using AMBOWeb.Filters;

namespace AMBOWeb.Controllers
{
    public class DealerController : SonyBaseController
    {
        private readonly IAPIDealerData _APIDealerData;
        private readonly IAPICommon _APICommon;

        public DealerController(IAPIDealerData IAPiDealerData, IAPICommon IAPiCommon)
        {
            _APIDealerData = IAPiDealerData;
            _APICommon = IAPiCommon;
        }

        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.DealerMaster, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            GetBranch InputParam = new GetBranch();
            DealerMaster view = new DealerMaster();
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

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.DealerMaster, Rights = (int)Right.Create)]
        public ActionResult AddUpdateDealer(string DealerId, string Operation,string SAPCode,int? OutletStatus)
        {
            DealerMaster Data = new DealerMaster();
            if (Operation == "Update")
            {
                DealerMaster InputData = new DealerMaster();
                InputData.ID = Convert.ToInt32(DealerId);
                Data = _APIDealerData.GetDealerById(InputData);
                //ViewBag.OutletStatus = 0;
            }
            else
            {
                PayerDetails input = new PayerDetails();
                input.SAPCode = SAPCode;
                var PayerDetails = _APIDealerData.GetDealerCode(input);
                if (PayerDetails.Data != null)
                {
                    Data.DealerCode = PayerDetails.Data.DealerCode;                    
                    Data.PayerCode = PayerDetails.Data.PayerCode;
                    Data.PayerName = PayerDetails.Data.PayerName;
                }
                Data.SAPCode = SAPCode;
                Data.MasterCode1 = SAPCode;
                Data.MasterCode2 = SAPCode;
                //ViewBag.OutletStatus = OutletStatus;
            }
            return View(Data);
        }

        [HttpPost]
        public ActionResult GetPayerDetails(string sapcode)
        {
            Envelope<PayerDetails> output = null;
            PayerDetails input = new PayerDetails();
            input.SAPCode = sapcode;
            output = _APIDealerData.GetDealerCode(input);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.DealerMaster, Rights = (int)Right.Create)]
        public ActionResult AddUpdateDealer(DealerMaster InputParam)
        {
            InputParam.UserId = objUserSession.UserId;
            Envelope<bool> output;
            if (ModelState.IsValid)
            {
                if (InputParam.ID == 0)
                {
                    output = new Envelope<bool>();
                    output = _APIDealerData.InsertDealer(InputParam);
                    if (output.Data)
                        AddSuccessNotification(output.Message);
                    return Json(output, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    output = new Envelope<bool>();
                    output = _APIDealerData.UpdateDealer(InputParam);
                    if (output.Data)
                        AddSuccessNotification(output.Message);
                    return Json(output, JsonRequestBehavior.AllowGet);
                }
                //if (output.Data)
                //{ 
                //    TempData["Message"] = output.Message;
                //    return Redirect("Index");
                //}
                //ViewBag.Status = output.MessageCode.ToString();
                //ViewBag.Message = output.Message;
            }
            else
            {
                string errorMessages = "";
                int i = 0;

                foreach (ModelState modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        if (i == 0)
                        {
                            i = i + 1;
                            errorMessages += error.ErrorMessage + " ";
                        }
                    }
                }
                return Json(new Envelope<bool> { Data = false, Message = errorMessages, MessageCode = 0 });
            }
            
        }

        [HttpPost]
        public JsonResult CheckPSIOutlet(bool PSIOutlet1, bool PSIOutlet2, string DealerId, string Code)
        {
            DealerMaster InputParam = new DealerMaster();
            InputParam.DealerCode = DealerId;
            InputParam.SAPCode = Code;
            InputParam.PSIOutlet1 = PSIOutlet1;
            InputParam.PSIOutlet2 = PSIOutlet2;
            var output = _APIDealerData.CheckPSIOutlet(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateInput(false)]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.DealerMaster, Rights = (int)Right.Delete)]
        public ActionResult DeleteDealer(DealerMaster InputParam)
        {

            InputParam.UserId = Convert.ToInt64(Session["UserId"]);
            var Output = _APIDealerData.DeleteDealer(InputParam);
            return Json(Output);
        }

        [HttpGet]
        public ActionResult GetDealerCodeList()
        {
            List<string> data = _APICommon.GetDealerCodeList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }       
           
    }
}