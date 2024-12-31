using AMBOModels.MasterMaintenance;
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
    public class ChannelController : SonyBaseController
    {
        private readonly IAPIChannelData _APIChannelData;

        public ChannelController(IAPIChannelData IAPIChannelData)
        {
            _APIChannelData = IAPIChannelData;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.ChannelManager, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.ChannelManager, Rights = (int)Right.Delete)]
        public ActionResult Delete(ChannelMaster objFormData)
        {
            var output = _APIChannelData.DeleteChannel(objFormData);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.ChannelManager, Rights = (int)Right.Edit)]
        public ActionResult Update(ChannelMaster objFormData)
        {
            var output = _APIChannelData.UpdateChannel(objFormData);
            return Json(output);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Dealer, SubModuleId = (Int64)SubModules.ChannelManager, Rights = (int)Right.Create)]
        public ActionResult Create(ChannelMaster objFormData)
        {
            //if (ModelState.IsValid)
            //{
            Envelope<bool> output = null;
            if (objFormData.Id == 0 || objFormData.Id == null)
            {
                output = _APIChannelData.CreateChannel(objFormData);
                //if (output.MessageCode == (int)Acceptable.Created)
                //    AddSuccessNotification(output.Message);
                //else
                //    AddErrorNotification(output.Message);
            }

            else
            {
                output = _APIChannelData.UpdateChannel(objFormData);
                //if (output.MessageCode == (int)Acceptable.Accepted)
                //    AddSuccessNotification(output.Message);
                //else
                //    AddErrorNotification(output.Message);
            }

            return Json(output, JsonRequestBehavior.AllowGet);
        }
    }
}