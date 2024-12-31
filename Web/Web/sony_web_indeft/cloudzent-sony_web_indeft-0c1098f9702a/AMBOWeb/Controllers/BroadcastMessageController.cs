using AMBOModels.GlobalAccessible;
using AMBOModels.MasterMaintenance;
using AMBOModels.UserManagement;
using AMBOWeb.Classes;
using AMBOWeb.Filters;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class BroadcastMessageController : SonyBaseController
    {
        private readonly IAPIBroadcastMessage _APIBroadcastMessage;
        private readonly IAPICommon _APICommon;

        public BroadcastMessageController(IAPIBroadcastMessage IAPIBroadcastMessage, IAPICommon IAPICommon)
        {
            _APIBroadcastMessage = IAPIBroadcastMessage;
            _APICommon = IAPICommon;
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Communication, SubModuleId = (Int64)SubModules.BroadcastMessage, Rights = (int)Right.View)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [SessionAuthorize(ModuleId = (Int64)Modules.Communication, SubModuleId = (Int64)SubModules.BroadcastMessage, Rights = (int)Right.Create)]
        public ActionResult Create()
        {
            GetBranch InputParam = new GetBranch();
            CreateBroadcastMessageForm view = new CreateBroadcastMessageForm();
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

        [HttpPost]
        [ValidateInput(false)]
        [SessionAuthorize(ModuleId = (Int64)Modules.Communication, SubModuleId = (Int64)SubModules.BroadcastMessage, Rights = (int)Right.Create)]
        public ActionResult Create(CreateBroadcastMessageForm objFormData, HttpPostedFileBase Attachment)
        {
           
            if(objFormData.Message.Contains("<p>"))
            {
                objFormData.Message = objFormData.Message.Replace("<p>", "");
            }
            if (objFormData.Message.Contains("<br>"))
            {
                objFormData.Message = objFormData.Message.Replace("<br>", "");
                objFormData.Message = objFormData.Message.Remove(objFormData.Message.Length - 4);

            }

            if ((objFormData.SFAIds == null || objFormData.SFAIds.Count == 0) && (objFormData.SIDIds == null || objFormData.SIDIds.Count == 0))
            {
                SearchSFA objDataForSearch = new SearchSFA();
                SearchSID In = new SearchSID();
                objDataForSearch.BranchIds = new Int64[objFormData.BranchIds.Count()];
                if(objFormData.DivisionIds!=null)
                {
                    objDataForSearch.DivisionIds = new Int64[objFormData.DivisionIds.Count()];
                    for (int i = 0; i < objFormData.DivisionIds.Count(); i++)
                    {
                        objDataForSearch.DivisionIds[i] = objFormData.DivisionIds[i];
                    }
                }
                objDataForSearch.RoleIds = new Int64[objFormData.RoleIds.Count()];


                In.BranchIds = new Int64[objFormData.BranchIds.Count()];
                In.RoleIds = new Int64[objFormData.RoleIds.Count()];

              
                for (int i = 0; i < objFormData.BranchIds.Count(); i++)
                {
                    objDataForSearch.BranchIds[i] = objFormData.BranchIds[i];
                    In.BranchIds[i] = objFormData.BranchIds[i];
                }

                for (int i = 0; i < objFormData.RoleIds.Count(); i++)
                {
                    objDataForSearch.RoleIds[i] = objFormData.RoleIds[i];
                    In.RoleIds[i] = objFormData.RoleIds[i];
                }

                objDataForSearch.UserId = objUserSession.UserId;

                objFormData.UserId = objUserSession.UserId;

                objFormData.SFAIds = (from Division in _APICommon.GetSFAFromDivisionAndBranchAndRole(objDataForSearch).AsEnumerable()
                                      select Division.SFAUserId).ToList();

                objFormData.SIDIds = (from Division in _APICommon.GetSIDFromBranchAndRoleForBroadcast(In).AsEnumerable()
                                      select Division.SIDUserId).ToList();

            }

            if ((objFormData.SFAIds != null ) && (objFormData.SIDIds == null || objFormData.SIDIds.Count == 0))
            {
                SearchSFA objDataForSearch = new SearchSFA();
                SearchSID In = new SearchSID();
                objDataForSearch.BranchIds = new Int64[objFormData.BranchIds.Count()];

                if (objFormData.DivisionIds != null)
                {
                    objDataForSearch.DivisionIds = new Int64[objFormData.DivisionIds.Count()];

                    for (int i = 0; i < objFormData.DivisionIds.Count(); i++)
                    {
                        objDataForSearch.DivisionIds[i] = objFormData.DivisionIds[i];
                    }
                }


                objDataForSearch.RoleIds = new Int64[objFormData.RoleIds.Count()];

                In.BranchIds = new Int64[objFormData.BranchIds.Count()];
                In.RoleIds = new Int64[objFormData.RoleIds.Count()];


                for (int i = 0; i < objFormData.BranchIds.Count(); i++)
                {
                    objDataForSearch.BranchIds[i] = objFormData.BranchIds[i];
                    In.BranchIds[i] = objFormData.BranchIds[i];
                }

                for (int i = 0; i < objFormData.RoleIds.Count(); i++)
                {
                    objDataForSearch.RoleIds[i] = objFormData.RoleIds[i];
                    In.RoleIds[i] = objFormData.RoleIds[i];
                }

                objDataForSearch.UserId = objUserSession.UserId;
                objFormData.UserId = objUserSession.UserId;


                objFormData.SIDIds = (from Division in _APICommon.GetSIDFromBranchAndRoleForBroadcast(In).AsEnumerable()
                                      select Division.SIDUserId).ToList();

            }


            if ((objFormData.SFAIds == null || objFormData.SFAIds.Count == 0) && (objFormData.SIDIds != null || objFormData.SIDIds.Count != 0))
            {
                SearchSFA objDataForSearch = new SearchSFA();
                SearchSID In = new SearchSID();
                objDataForSearch.BranchIds = new Int64[objFormData.BranchIds.Count()];

                if (objFormData.DivisionIds != null)
                {
                    objDataForSearch.DivisionIds = new Int64[objFormData.DivisionIds.Count()];
                    for (int i = 0; i < objFormData.DivisionIds.Count(); i++)
                    {
                        objDataForSearch.DivisionIds[i] = objFormData.DivisionIds[i];
                    }
                }

                objDataForSearch.RoleIds = new Int64[objFormData.RoleIds.Count()];

                In.BranchIds = new Int64[objFormData.BranchIds.Count()];
                In.RoleIds = new Int64[objFormData.RoleIds.Count()];

              
                for (int i = 0; i < objFormData.BranchIds.Count(); i++)
                {
                    objDataForSearch.BranchIds[i] = objFormData.BranchIds[i];
                    In.BranchIds[i] = objFormData.BranchIds[i];
                }
                for (int i = 0; i < objFormData.RoleIds.Count(); i++)
                {
                    objDataForSearch.RoleIds[i] = objFormData.RoleIds[i];
                    In.RoleIds[i] = objFormData.RoleIds[i];
                }


                objDataForSearch.UserId = objUserSession.UserId;
                objFormData.UserId = objUserSession.UserId;

                objFormData.SFAIds = (from Division in _APICommon.GetSFAFromDivisionAndBranchAndRole(objDataForSearch).AsEnumerable()
                                      select Division.SFAUserId).ToList();

            }

            if(objFormData.SFAIds.Count==0 && objFormData.SIDIds.Count==0)
            {
                AddErrorNotification("No SFA or Sid is Fetched from Selected Role.");
                return RedirectToAction("Create","BroadcastMessage");
            }


            //}
            //if (objFormData.DivisionIds == null)
            //{

            //}

            if (ModelState.IsValid)
            {
                ViewBag.ApiUrlPath = WebConfigurationManager.AppSettings["APIUrl"].ToString();
                if(Attachment != null && Attachment.ContentLength > 0)
                {
                    if (CheckFileExtension(Path.GetExtension(Attachment.FileName)))
                    {
                        string FilePath = Server.MapPath("~/Uploads/Broadcast/" + Path.GetFileName(Attachment.FileName));
                        if (System.IO.File.Exists(Server.MapPath("~/Uploads/Broadcast/" + Path.GetFileName(Attachment.FileName))))
                        {
                            // delete file from server 
                            System.IO.File.Delete(Server.MapPath("~/Uploads/Broadcast/" + Path.GetFileName(Attachment.FileName)));
                        }
                        Attachment.SaveAs(FilePath);
                        
                        objFormData.FileName = Attachment.FileName;
                    }

                    string theFileName = Path.GetFileName(Attachment.FileName);
                    byte[] thePictureAsBytes = new byte[Attachment.ContentLength];
                    using (BinaryReader theReader = new BinaryReader(Attachment.InputStream))
                    {
                        thePictureAsBytes = theReader.ReadBytes(Attachment.ContentLength);
                    }
                    string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);
                    objFormData.PICTUREDATA = thePictureDataAsString;
                    System.IO.File.WriteAllBytes(Server.MapPath("~/Uploads/Broadcast/" + Attachment.FileName), Convert.FromBase64String(objFormData.PICTUREDATA));
                    objFormData.PictureAsBytes = thePictureAsBytes;
                    objFormData.FileName = Attachment.FileName;

                }

                //if (objFormData.FileName != null)
                //{
                //    ////string theFileName = Path.GetFileName(Attachment.FileName);
                //    ////byte[] thePictureAsBytes = new byte[Attachment.ContentLength];
                //    ////using (BinaryReader theReader = new BinaryReader(Attachment.InputStream))
                //    ////{
                //    ////    thePictureAsBytes = theReader.ReadBytes(Attachment.ContentLength);
                //    ////}
                //    ////string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);
                //    ////objFormData.PICTUREDATA = thePictureDataAsString;
                //    ////objFormData.PictureAsBytes = thePictureAsBytes;

                //}

                var output = _APIBroadcastMessage.CreateBroadcastMessage(objFormData);
                if (output.Data != null && output.Data.MessageId != 0 && output.Data.FCMIds!= null && output.Data.FCMIds.Count > 0 && objFormData.Status == 1)
                {
                    AddSuccessNotification("Message Broadcasted successfully.");
                    FCMNotification.Send(output.Data.FCMIds, output.Data.MessageId, objFormData.Subject, objFormData.Message,
                        objFormData.FileName == null ? null : ResolveServerUrl(VirtualPathUtility.ToAbsolute("~/Uploads/Broadcast/" + objFormData.FileName), false));

                    return new RedirectResult(Url.Action("Index", "BroadcastMessage"));
                    //return Json(new Envelope<bool> { Data = true, Message = "Message Broadcasted successfully.", MessageCode = 1 });
                }
                else if(output.Data != null && output.Data.MessageId != 0 && objFormData.Status == 2)
                {
                    AddSuccessNotification("Message saved successfully.");
                    return new RedirectResult(Url.Action("Index", "BroadcastMessage"));
                }
                return View("Index");
                //return Json(new Envelope<bool> { Data = false, Message = "Could not broadcast message. Please try after some time.", MessageCode = 0 });
            }
            else
            {
                //string errorMessages = "";
                //foreach (ModelState modelState in ModelState.Values)
                //    foreach (ModelError error in modelState.Errors)
                //        errorMessages += error.ErrorMessage + " ";
                //return Json(new Envelope<bool> { Data = false, Message = errorMessages, MessageCode = 0 });
                return View("Index");
            }
        }

        private bool CheckFileExtension(string ext)
        {
            switch(ext)
            {
                
                case ".xls": break; case ".xlsx": break; case ".txt": break; case ".doc": break;
                case ".docx": break; case ".pdf": break; case ".jpg": break; case ".jpeg": break;
                case ".png": break; default: return false;
            }
            return true;
        }


        [HttpPost]
        [ValidateInput(false)]
        [SessionAuthorize(ModuleId = (Int64)Modules.Communication, SubModuleId = (Int64)SubModules.BroadcastMessage, Rights = (int)Right.Create)]
        public JsonResult Delete(CreateBroadcastMessageForm objFormData)
        {
            if (ModelState.IsValid)
            {
                objFormData.UserId = objUserSession.UserId;
                objFormData.EncryptKey = objUserSession.EncryptKey;
                objFormData.RoleName = objUserSession.RoleName;
                var output = _APIBroadcastMessage.InActiveBroadcastMessage(objFormData);
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