using AMBOModels.Global;
using AMBOModels.MasterMaintenance;
using AMBOWeb.Filters;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class ShiftController : SonyBaseController
    {
        private readonly IAPIShiftData _APIShiftData;
        public static IAPIGridData _Grid;

        public ShiftController(IAPIShiftData APIShiftData, IAPIGridData IAPIGridData)
        {
            _APIShiftData = APIShiftData;
            _Grid = IAPIGridData;
        }
        // GET: Shift
        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.ShiftMaster, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetShiftMaster(ShiftFilter InputParam)
        {
            var output = _Grid.GetShiftMaster(InputParam);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

       

        //[HttpPost]
        //[SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.ShiftMaster, Rights = (int)Right.Edit)]
        //public ActionResult UpdateShift(ShiftMaster objFormData)
        //{
        //    var output = _APIShiftData.UpdateShiftTiming(objFormData);
        //    return Json(output);
        //}

        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.ShiftMaster, Rights = (int)Right.Create)]
        public JsonResult CreateShift(string ShiftName, string Id)
        {
            string ErrorMessage = "";
            ShiftMaster Data = new ShiftMaster();
            Data.ShiftName = ShiftName;
            Data.CreatedBy = Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;

            ErrorModel Error = new ErrorModel();
            Error = _APIShiftData.CreateShiftTiming(Data);
            if (Error.ErrorCode == 1)
                AddSuccessNotification(Error.ErrorMessage);
            ErrorMessage = Error.ErrorMessage;
            return Json(Error, JsonRequestBehavior.AllowGet);
        }

        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.ShiftMaster, Rights = (int)Right.Edit)]
        public JsonResult GetShiftById(string ShiftId)
        {
            ShiftMaster Data = new ShiftMaster();
            Data.Id = Convert.ToInt64(ShiftId);
            Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;

            ShiftMaster shiftMaster = new ShiftMaster();
            shiftMaster = _APIShiftData.GetShiftById(Data);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.ShiftMaster, Rights = (int)Right.Edit)]
        public JsonResult UpdateShift(string ShiftName, string Id)
        {
            string ErrorMessage = "";
            ShiftMaster Data = new ShiftMaster();
            Data.Id = Convert.ToInt64(Id);
            Data.ShiftName = ShiftName;
            Data.CreatedBy = Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;

            ErrorModel Error = new ErrorModel();
            Error = _APIShiftData.UpdateShiftTiming(Data);
            if (Error.ErrorCode == 1)
                AddSuccessNotification(Error.ErrorMessage);
            ErrorMessage = Error.ErrorMessage;
            return Json(Error, JsonRequestBehavior.AllowGet);
        }

        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.ShiftMaster, Rights = (int)Right.Delete)]
        public JsonResult DeleteShift(string ShiftId)
        {
            string ErrorMessage = "";
            ShiftMaster Data = new ShiftMaster();
            Data.Id = Convert.ToInt64(ShiftId);
            Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;

            ErrorModel Error = new ErrorModel();
            Error = _APIShiftData.DeleteShift(Data);
            if (Error.ErrorCode == 1)
                AddSuccessNotification(Error.ErrorMessage);
            ErrorMessage = Error.ErrorMessage;
            return Json(Error, JsonRequestBehavior.AllowGet);
        }
    }
}