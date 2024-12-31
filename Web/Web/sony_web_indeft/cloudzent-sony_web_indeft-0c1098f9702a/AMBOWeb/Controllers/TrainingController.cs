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
    public class TrainingController : SonyBaseController
    {
        private readonly IAPIFeedbackData _APIFeedbackData;

        public TrainingController(IAPIFeedbackData IAPIFeedbackData)
        {
            _APIFeedbackData = IAPIFeedbackData;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(CreateTrainingForm objFormData)
        {
            var output = _APIFeedbackData.CreateTrainingForm(objFormData);
            if (output.MessageCode == (int)Acceptable.Created)
                AddSuccessNotification(output.Message);
            return Json(output);
        }


        [HttpPost]
        [ValidateInput(false)]
        [SessionAuthorize(ModuleId = (Int64)Modules.Communication, SubModuleId = (Int64)SubModules.BroadcastMessage, Rights = (int)Right.Create)]
        public JsonResult Delete(CreateTrainingForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APIFeedbackData.InActiveTrainingMessage(objFormData);
                return Json(output);
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

    }
}