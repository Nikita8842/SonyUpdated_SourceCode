using AMBOModels.MasterMaintenance;
using AMBOModels.UserValidation;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMBOWeb.Controllers
{
    public class IncentiveCalculationDateSettingController : SonyBaseController
    {
        private readonly IAPIIncentiveCalculationDateSetting _APIIncentiveCalculationDateSetting;

        public IncentiveCalculationDateSettingController(IAPIIncentiveCalculationDateSetting IAPIIncentiveCalculationDateSetting_)
        {
            _APIIncentiveCalculationDateSetting = IAPIIncentiveCalculationDateSetting_;
        }
        // GET: IncentiveCalculationDateSetting
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetIncentiveCalculationDateSettingReport(IncentiveCalculationDateSettingParam objGridData)
        {
            var userDetails = (BaseSession)(Session["BaseSession"]);
            objGridData.CreatedBy = Convert.ToInt64(userDetails.UserId);
            var output = _APIIncentiveCalculationDateSetting.GetIncentiveCalculationDateSetting(objGridData);
            Session["ICollectionDay"] = output.Data.IncentiveCalculationDateSettingData[0].CollectionDay;
            Session["IMonth"] = output.Data.IncentiveCalculationDateSettingData[0].Month;
            return Json(output.Data.IncentiveCalculationDateSettingData, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UpdateIncentiveCalculationDate()
        {
            ViewBag.ICollectionDay = Session["ICollectionDay"];
            ViewBag.IMonth = Session["IMonth"];
            return View();
        }
        [HttpPost]
        public ActionResult UpdateIncentiveCalculationDate(IncentiveCalculationDateSetting objFormData)
        {
            Envelope<bool> createdoutput = null;
            objFormData.CreatedBy = objUserSession.UserId;

            createdoutput = _APIIncentiveCalculationDateSetting.UpdateIncentiveCalculationDateSetting(objFormData);
            if (createdoutput.MessageCode == (int)Acceptable.Accepted)
                AddSuccessNotification(createdoutput.Message);
            else
                AddErrorNotification(createdoutput.Message);
            return RedirectToAction("Index");
        }
    }
}