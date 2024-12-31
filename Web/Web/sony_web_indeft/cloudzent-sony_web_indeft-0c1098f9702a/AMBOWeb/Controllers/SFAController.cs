using AMBOModels.GlobalAccessible;
using AMBOModels.Mappings;
using AMBOModels.MasterMaintenance;
using AMBOModels.UserManagement;
using AMBOWeb.Filters;
using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class SFAController : SonyBaseController
    {
        private readonly IAPISFAData _APISFAData;
        private readonly IAPICommon _APICommon;

        public SFAController(IAPISFAData IAPISFAData, IAPICommon IAPICommon)
        {
            _APISFAData = IAPISFAData;
            _APICommon = IAPICommon;
        }

        public ActionResult GetSFAMasterGridFilters()
        {
            return PartialView("PartialViews/SFAMasterGridFilters");
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFAMaster, Rights = (int)Right.View)]
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
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFAMaster, Rights = (int)Right.Create)]
        public ActionResult Create()
        {
            SFAFormData objformdata = new SFAFormData();
            var sfacode = _APISFAData.GetSFACode();
            objformdata.EmployeeCode = sfacode.Data.SFACode;
            objformdata.LoginId = sfacode.Data.SFACode;
            return View("AddUpdate", objformdata);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFAMaster, Rights = (int)Right.Create)]
        public ActionResult Create(SFAFormData objFormData)
        {
            return View("AddUpdate", objFormData);
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFAMaster, Rights = (int)Right.Edit)]
        public ActionResult Update(Int64 ID, string IsOffered)
        {
            string role = objUserSession.RoleName;
            ViewBag.rName = role;
            if (IsOffered == "No")
            {
                SFAFormData objFormData = _APISFAData.GetSFAById(ID);
                objFormData.isUpdate = true;
                objFormData.OldPassword = objFormData.Password;
                return View("AddUpdate", objFormData);
            }
            else
            {
                
                SFAFormData objFormData = _APISFAData.GetOfferedEmployeeById(ID);
                objFormData.isUpdate = true;
                objFormData.OldPassword = objFormData.Password;
                objFormData.IsSFAOffered = true;
                return View("AddUpdate", objFormData);
            }
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFAMaster, Rights = (int)Right.Create)]
        public ActionResult AddUpdate(SFAFormData objFormData)
        {
            //if(!ModelState.IsValid)
            //{
            //    //AddErrorNotification("Age: " + objFormData.IsUser18Plus());
            //    AddErrorNotification("Invalid Data Entered. Please revalidate data entered and try again.");
            //    Create(objFormData);
            //}

            //if (ModelState.IsValid)
            //{
            if (objFormData.IsSFAOffered)
            {
                if (objFormData.Password != objFormData.OldPassword)
                    objFormData.isPasswordChange = true;
                objFormData.UserId = objUserSession.UserId;
                var output = _APISFAData.ManageOfferedEmployee(objFormData);
                if (output.Data)
                {
                    AddSuccessNotification(output.Message);
                    return RedirectToAction("NavigationIndex", "DealerSFAMapping", new { LoginId = objFormData.EmployeeCode });
                }
                objFormData.SubmitMessage = output.Message;
            }

            else
            {
                if (objFormData.EmployeeId != null && objFormData.EmployeeId != 0)//update
                {
                    if (objFormData.Password != objFormData.OldPassword)
                        objFormData.isPasswordChange = true;
                    objFormData.UserId = objUserSession.UserId;
                    var output = _APISFAData.UpdateSFA(objFormData);
                    if (output.Data)
                    {
                        AddSuccessNotification(output.Message);
                        //use sony base controller functions
                        //TempData["Notification"] = output.Message;
                        return RedirectToAction("Index");
                    }
                    objFormData.SubmitMessage = output.Message;
                }
                else//create
                {
                    objFormData.UserId = objUserSession.UserId;
                    var output = _APISFAData.CreateSFA(objFormData);
                    if (output.Data)
                    {
                        AddSuccessNotification(output.Message);
                        //this chunk of code is added to implement navigation from sfa creation to dealer sf mapping
                        //added by bela on 21/5/2018
                        return RedirectToAction("NavigationIndex", "DealerSFAMapping", new { LoginId = objFormData.EmployeeCode });

                    }
                    objFormData.SubmitMessage = output.Message;
                }
            }
            //}
            //else
            //{
            //    objFormData.SubmitMessage = "Invalid data entered.";
            //}
            return View(objFormData);
        }

        [HttpPost]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFAMaster, Rights = (int)Right.Delete)]
        public ActionResult Delete(SFAFormData objFormData)
        {
            var output = _APISFAData.DeleteSFA(objFormData);
            return Json(output);
        }
    }
}