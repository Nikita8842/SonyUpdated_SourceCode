using AMBOModels.MasterMaintenance;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMBOWeb.Controllers
{
    public class FeedbackController : SonyBaseController
    {
        private readonly IAPIFeedbackData _APIFeedbackData;

        public FeedbackController(IAPIFeedbackData IAPIFeedbackData)
        {
            _APIFeedbackData = IAPIFeedbackData;
        }

        [HttpGet]
        public ActionResult Forms()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ViewFeedbackFormDesign(ViewFeedbackForm objFormData)
        {
            var output = _APIFeedbackData.ViewFeedbackFormDesign(objFormData);
            return View("CreateForm", output.Data);
        }

        [HttpGet]
        public ActionResult CreateForm()
        {
            return View("CreateForm", null);
        }

        [HttpPost]
        public JsonResult CreateForm(CreateFeedbackForm objFormData)
        {
            objFormData.EncryptKey = objUserSession.EncryptKey;
            objFormData.UserId = objUserSession.UserId;
            objFormData.RoleName = objUserSession.RoleName;
            var output = _APIFeedbackData.CreateFeedbackForm(objFormData);
            return Json(output);
        }

        [HttpPost]
        public JsonResult DeleteForm(DeleteFeedbackForm objFormData)
        {
            objFormData.EncryptKey = objUserSession.EncryptKey;
            objFormData.UserId = objUserSession.UserId;
            objFormData.RoleName = objUserSession.RoleName;
            var output = _APIFeedbackData.DeleteFeedbackForm(objFormData);
            return Json(output);
        }
    }
}