using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMBOModels.UserManagement;
using APIAccessLayer.INTERFACE;
using static AMBOWeb.Classes.Common;
using AMBOWeb.Filters;
using APIAccessLayer.Helper;
using AMBOModels.GlobalAccessible;

namespace AMBOWeb.Controllers
{
    public class EmployeeController : SonyBaseController
    {
        private readonly IAPIEmployeeData _APIEmployeeData;
        private readonly IAPICommon _APICommon;

        public EmployeeController(IAPIEmployeeData IAPIEmployeeData, IAPICommon IAPICommon)
        {
            _APIEmployeeData = IAPIEmployeeData;
            _APICommon = IAPICommon;
        }

        public ActionResult GetEmployeeMasterGridFilters()
        {
            return PartialView("PartialViews/EmployeeMasterGridFilters");
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Employee, SubModuleId = (Int64)SubModules.EmployeeManager, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            GetBranch InputParam = new GetBranch();
            UserBasicProperties view = new UserBasicProperties();
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
        [SessionAuthorize(ModuleId = (Int64)Modules.Employee, SubModuleId = (Int64)SubModules.EmployeeManager, Rights = (int)Right.Create)]
        public ActionResult Create()
        {
            return View("AddUpdate", new EmployeeFormData());
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Employee, SubModuleId = (Int64)SubModules.EmployeeManager, Rights = (int)Right.Edit)]
        public ActionResult Update(Int64 ID)
        {
            EmployeeFormData objFormData = _APIEmployeeData.GetEmployeeById(ID);
            objFormData.isUpdate = true;
            objFormData.OldPassword = objFormData.Password;
            return View("AddUpdate", objFormData);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Employee, SubModuleId = (Int64)SubModules.EmployeeManager, Rights = (int)Right.Create)]
        public ActionResult AddUpdate(EmployeeFormData objFormData)
        {
            //added by Bela on 6th Dec'18
            //error while updating employee details as no session values were passed so adding session values like UserId
            objFormData.UserId = objUserSession.UserId;
            //objFormData.RoleName = objUserSession.RoleName;
            //objFormData.RoleId = objUserSession.RoleId;
            objFormData.EncryptKey = objUserSession.EncryptKey;

            if (ModelState.IsValid)
            {

                if (objFormData.EmployeeId != null && objFormData.EmployeeId != 0)//update
                {
                    if (objFormData.Password != objFormData.OldPassword)
                        objFormData.isPasswordChange = true;
                    var output = _APIEmployeeData.UpdateEmployee(objFormData);
                    if (output.Data)
                    {
                        AddSuccessNotification(output.Message);
                        return RedirectToAction("Index");
                    }
                    objFormData.SubmitMessage = output.Message;
                }
                else//create
                {
                    var output = _APIEmployeeData.CreateEmployee(objFormData);
                    if (output.Data)
                    {
                        AddSuccessNotification(output.Message);
                        return RedirectToAction("Index");
                    }
                    objFormData.SubmitMessage = output.Message;
                }
                
            }
            else
            {
                objFormData.SubmitMessage = "Invalid data entered.";
            }
            return View(objFormData);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.Employee, SubModuleId = (Int64)SubModules.EmployeeManager, Rights = (int)Right.Delete)]
        public ActionResult Delete(EmployeeFormData objFormData)
        {
            var output = _APIEmployeeData.DeleteEmployee(objFormData);
            return Json(output);
        }

        #region Offered Employee
        [HttpPost]
        public ActionResult GetOfferedEmployeeGrid(OfferedEmployeeGridSearchFilters Input)
        {
            Input.UserId = objUserSession.UserId;
            Input.RoleName = objUserSession.RoleName;
            var output = _APIEmployeeData.GetOfferedEmployeeMasterGrid(Input);
            return PartialView("OfferedEmployeesGrid", output);            

        }
        #endregion Offered Employee
    }
}