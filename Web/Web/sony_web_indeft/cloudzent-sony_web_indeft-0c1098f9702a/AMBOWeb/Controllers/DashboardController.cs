using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;
using AMBOWeb.Filters;
using AMBOWeb.Models;
using APIAccessLayer.INTERFACE;
using AMBOModels.UserManagement;
using APIAccessLayer.Helper;

namespace AMBOWeb.Controllers
{
    public class DashboardController : SonyBaseController
    {
        private readonly IUserValidation _IUserValidation;
        private readonly IAPIEmployeeData _IAPIEmployeeData;

        public DashboardController(IUserValidation IUserValidation, IAPIEmployeeData IAPIEmployeeData)
        {
            _IUserValidation = IUserValidation;
            _IAPIEmployeeData = IAPIEmployeeData;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(UserUpdatePasswordModel InputParam)
        {
            InputParam.UserId = objUserSession.UserId;
            var output = _IUserValidation.UpdateUserPassword(InputParam);
            if (output.Data)
            {
                AddSuccessNotification(output.Message);
                return View("Index");
            }
            else
            {
                AddErrorNotification(output.Message);
                return View();
            }
        }

        public PartialViewResult GetMenu()
        {
            ModuleRightsByRoleInput Input = new ModuleRightsByRoleInput();
            Input.EncryptKey = objUserSession.EncryptKey;
            Input.RoleId = objUserSession.RoleId;
            Input.RoleName = objUserSession.RoleName;
            Input.UserId = objUserSession.UserId;
            // Envelope<MenusList> MSMList = new Envelope<MenusList>();
            Envelope<MenusList> MSMList = _IAPIEmployeeData.GetModuleRightsByRole(Input);
            if (MSMList.Data != null)
            {
                MSMList.Data.ModuleList = MSMList.Data.ModuleList.ToList();
                MSMList.Data.SubModuleList = MSMList.Data.SubModuleList.ToList();
            }

            return PartialView("_SonyMenus", MSMList.Data);
        }

    }
}