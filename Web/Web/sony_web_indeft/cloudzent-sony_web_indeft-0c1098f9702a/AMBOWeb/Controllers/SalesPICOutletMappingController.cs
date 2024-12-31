using AMBOModels.Mappings;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMBOWeb.Controllers
{
    public class SalesPICOutletMappingController : SonyBaseController
    {
        private readonly IAPIMappingData _APIMappingData;

        public SalesPICOutletMappingController(IAPIMappingData IAPIMappingData)
        {
            _APIMappingData = IAPIMappingData;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ManageSalesPICOutletMappingForm());
        }

        [HttpGet]
        public ActionResult Update(Int64 SalesPICId)
        {
            ManageSalesPICOutletMappingForm objFormData = new ManageSalesPICOutletMappingForm();
            objFormData.SalesPICId = SalesPICId;
            var output = _APIMappingData.GetSalesPICOutletMapping(objFormData);
            if(output.MessageCode == (int)Acceptable.Found)
                return View("Create", output.Data);
            else
            {
                AddErrorNotification(output.Message);
                return new RedirectResult(Url.Action("Index", "SalesPICOutletMapping"));
            }
        }

        [HttpPost]
        public ActionResult Manage(ManageSalesPICOutletMappingForm objFormData)
        {
            var output = _APIMappingData.ManageSalesPICOutletMapping(objFormData);
            if (output.MessageCode == (int)Acceptable.Created)
                AddSuccessNotification(output.Message);
            return Json(output);
        }

        [HttpPost]
        public ActionResult Delete(DeleteSalesPICOutletMappingForm objFormData)
        {
            var output = _APIMappingData.DeleteSalesPICOutletMapping(objFormData);
            return Json(output);
        }

    }
}