using AMBOModels.Global;
using AMBOModels.MasterMaintenance;
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
    /// <summary>
    /// To perform operations related to 
    /// </summary>
    public class SFALevelMasterController : SonyBaseController
    {
        #region Global Parameters
        public static ISFALevelMaster _LevelMaster;
        public static IAPIGridData _Grid;
        #endregion

        public SFALevelMasterController(ISFALevelMaster ISFALevelMaster, IAPIGridData IAPIGridData)
        {
            _LevelMaster = ISFALevelMaster;
            _Grid = IAPIGridData;
        }

        #region SFA Level Master
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFALevel, Rights = (int)Right.View)]
        public ActionResult SFALevelMaster()
        {
            return View();
        }

        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFALevel, Rights = (int)Right.Create)]
        public JsonResult CreateSFALevelMaster(string LevelName, string Id)
        {
            string ErrorMessage = "";
            SFALevelMasterData Data = new SFALevelMasterData();
            Data.SFALevelName = LevelName;
            Data.CreatedBy = Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;

            ErrorModel Error = new ErrorModel();
            Error = _LevelMaster.CreateSFALevel(Data);
            if (Error.ErrorCode == 1)
                AddSuccessNotification(Error.ErrorMessage);
            ErrorMessage = Error.ErrorMessage;
            return Json(Error, JsonRequestBehavior.AllowGet);
        }

        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFALevel, Rights = (int)Right.Delete)]
        public JsonResult DeleteSFALevelMaster(string LevelId)
        {
            string ErrorMessage = "";
            SFALevelInput Data = new SFALevelInput();
            Data.SFALevelId = Convert.ToInt64(LevelId);
            Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;

            ErrorModel Error = new ErrorModel();
            Error = _LevelMaster.DeleteSFALevel(Data);
            if (Error.ErrorCode == 1)
                AddSuccessNotification(Error.ErrorMessage);
            ErrorMessage = Error.ErrorMessage;
            return Json(Error, JsonRequestBehavior.AllowGet);
        }

        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFALevel, Rights = (int)Right.Edit)]
        public JsonResult GetSFALevelById(string LevelId)
        {
            SFALevelInput Data = new SFALevelInput();
            Data.SFALevelId = Convert.ToInt64(LevelId);
            Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;

            SFALevel SFALevel = new SFALevel();
            SFALevel = _LevelMaster.GetSFALevelById(Data);
            return Json(Data,JsonRequestBehavior.AllowGet);
        }

        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFALevel, Rights = (int)Right.Edit)]
        public JsonResult UpdateSFALevelMaster(string LevelName, string Id)
        {
            string ErrorMessage = "";
            SFALevelMasterData Data = new SFALevelMasterData();
            Data.Id = Convert.ToInt64(Id);
            Data.SFALevelName = LevelName;
            Data.CreatedBy = Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;

            ErrorModel Error = new ErrorModel();
            Error = _LevelMaster.UpdateSFALevel(Data);
            if (Error.ErrorCode == 1)
                AddSuccessNotification(Error.ErrorMessage);
            ErrorMessage = Error.ErrorMessage;
            return Json(Error, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SFA SubLevel Master
        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFASubLevel, Rights = (int)Right.View)]
        public ActionResult SFASubLevelMaster()
        {
            //SFALevelFilter obj = new SFALevelFilter();
            //obj = null;
            //List<SFALevel> SFAList = new List<SFALevel>();
            //SFAList = _Grid.GetSFALevelMasterData(obj);
            //ViewBag.SFAList = SFAList;
            return View();
        }

        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFASubLevel, Rights = (int)Right.Create)]
        public JsonResult CreateSFASubLevelMaster(string SubLevelName, string Id, string LevelId, string Description)
        {
            string ErrorMessage = "";
            SFASubLevelMaster Data = new SFASubLevelMaster();
            Data.SFALevelId = Convert.ToInt64(LevelId);
            Data.SFASubLevelName = SubLevelName;
            Data.Description = Description;
            Data.CreatedBy = Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;

            ErrorModel Error = new ErrorModel();
            Error = _LevelMaster.CreateSFASubLevel(Data);
            if (Error.ErrorCode == 1)
                AddSuccessNotification(Error.ErrorMessage);
            ErrorMessage = Error.ErrorMessage;
            return Json(Error,JsonRequestBehavior.AllowGet);
        }

        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFASubLevel, Rights = (int)Right.Delete)]
        public JsonResult DeleteSFASubLevelMaster(string SubLevelId)
        {
            string ErrorMessage = "";
            SFASubLevelInput Data = new SFASubLevelInput();
            Data.SFASubLevelId = Convert.ToInt64(SubLevelId);
            Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;

            ErrorModel Error = new ErrorModel();
            Error = _LevelMaster.DeleteSFASubLevel(Data);
            if (Error.ErrorCode == 1)
                AddSuccessNotification(Error.ErrorMessage);
            ErrorMessage = Error.ErrorMessage;
            return Json(Error, JsonRequestBehavior.AllowGet);
        }

        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFASubLevel, Rights = (int)Right.Edit)]
        public JsonResult GetSFASubLevelById(string SubLevelId)
        {
            SFASubLevelInput Data = new SFASubLevelInput();
            Data.SFASubLevelId = Convert.ToInt64(SubLevelId);
            Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;

            SFASubLevel SFALevel = new SFASubLevel();
            SFALevel = _LevelMaster.GetSFASubLevelMasterDataById(Data);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [SessionAuthorize(ModuleId = (Int64)Modules.SFA, SubModuleId = (Int64)SubModules.SFASubLevel, Rights = (int)Right.Edit)]
        public JsonResult UpdateSFASubLevelMaster(string SubLevelName, string Id, string LevelId, string Description)
        {
            string ErrorMessage = "";
            SFASubLevelMaster Data = new SFASubLevelMaster();
            Data.SFALevelId = Convert.ToInt64(LevelId);
            Data.Id = Convert.ToInt64(Id);
            Data.SFASubLevelName = SubLevelName;
            Data.Description = Description;
            Data.CreatedBy = Data.UserId = objUserSession.UserId;
            Data.EncryptKey = objUserSession.EncryptKey;
            Data.RoleName = objUserSession.RoleName;

            ErrorModel Error = new ErrorModel();
            Error = _LevelMaster.UpdateSFASubLevel(Data);
            if (Error.ErrorCode == 1)
                AddSuccessNotification(Error.ErrorMessage);
            ErrorMessage = Error.ErrorMessage;
            return Json(Error, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}