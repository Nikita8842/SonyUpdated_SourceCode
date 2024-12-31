using AMBOModels.Global;
using AMBOModels.UserValidation;
using AMBOWeb.Models;
using APIAccessLayer.Helper;
using APIAccessLayer.INTERFACE;
using CaptchaMvc.HtmlHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class LoginController : Controller
    {
        public static IUserValidation _User;
        public static EncryptDecrypt _Enc;

        public LoginController(IUserValidation IUserValidation)
        {
            _User = IUserValidation;
            _Enc = new EncryptDecrypt();
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["BaseSession"] != null)
            {
                //Code remaining for Encryption Key Varification.
                return RedirectToAction("Index", "Dashboard");
            }
            UserValidationModel objLoginFormData = new UserValidationModel();
            return View("NewIndex", objLoginFormData);
        }

        [HttpPost]
        public ActionResult Index(UserValidationModel objLoginFormData)
        {
            string errorMessage = string.Empty;

            if (objLoginFormData.UserName.ToLower() == "sony")
            {
                objLoginFormData.IMEI = "";

                MD5 md5Enc = MD5.Create();

                byte[] encodedKey = md5Enc.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Convert.ToString(new Random().Next())));
                objLoginFormData.EncryptKey = _Enc.GetHex(md5Enc.ComputeHash(encodedKey));

                objLoginFormData.SessionTime = System.DateTime.Now;

                UserDetailsModel UserDetails = new UserDetailsModel();
                UserDetails = _User.ValidateLogin(objLoginFormData);

                if (UserDetails != null && UserDetails.Status == 1)
                {
                    BaseSession objUserSession = new BaseSession();
                    objUserSession.EncryptKey = objLoginFormData.EncryptKey;
                    objUserSession.UserId = UserDetails.UserId;
                    objUserSession.RoleId = UserDetails.RoleId;
                    objUserSession.RoleName = UserDetails.RoleName;
                    objUserSession.UserName = UserDetails.Name;
                    objUserSession.LastLoginDate = UserDetails.LastLoginDate;
                    Session["BaseSession"] = objUserSession;

                    List<RoleRights> objRoleRightsList = new List<RoleRights>();
                    objRoleRightsList = UserDetails.listRoleRights;
                    Session["RoleRightsList"] = objRoleRightsList;

                    return RedirectToAction("Index", "Dashboard");
                }

                //BugID: 0001751
                //Nikhil Thakral
                if (UserDetails != null)
                {
                    List<SonyNotification> notification = new List<SonyNotification>();
                    notification.Add(new SonyNotification() { Message = UserDetails.Message, Type = "error" });
                    TempData["Notification"] = notification;
                }
                else
                {
                    List<SonyNotification> notification = new List<SonyNotification>();
                    notification.Add(new SonyNotification() { Message = "Invalid credentials entered. Please try again.", Type = "error" });
                    TempData["Notification"] = notification;
                }
            }

            else
            {
                if (this.IsCaptchaValid("Captcha Authentication Failed."))
                {
                    objLoginFormData.IMEI = "";

                    MD5 md5Enc = MD5.Create();

                    byte[] encodedKey = md5Enc.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Convert.ToString(new Random().Next())));
                    objLoginFormData.EncryptKey = _Enc.GetHex(md5Enc.ComputeHash(encodedKey));

                    objLoginFormData.SessionTime = System.DateTime.Now;

                    UserDetailsModel UserDetails = new UserDetailsModel();
                    UserDetails = _User.ValidateLogin(objLoginFormData);

                    if (UserDetails != null && UserDetails.Status == 1)
                    {
                        BaseSession objUserSession = new BaseSession();
                        objUserSession.EncryptKey = objLoginFormData.EncryptKey;
                        objUserSession.UserId = UserDetails.UserId;
                        objUserSession.RoleId = UserDetails.RoleId;
                        objUserSession.RoleName = UserDetails.RoleName;
                        objUserSession.UserName = UserDetails.Name;
                        objUserSession.LastLoginDate = UserDetails.LastLoginDate;
                        Session["BaseSession"] = objUserSession;

                        List<RoleRights> objRoleRightsList = new List<RoleRights>();
                        objRoleRightsList = UserDetails.listRoleRights;
                        Session["RoleRightsList"] = objRoleRightsList;

                        return RedirectToAction("Index", "Dashboard");
                    }

                    //BugID: 0001751
                    //Nikhil Thakral
                    if (UserDetails != null)
                    {
                        List<SonyNotification> notification = new List<SonyNotification>();
                        notification.Add(new SonyNotification() { Message = UserDetails.Message, Type = "error" });
                        TempData["Notification"] = notification;
                    }
                    else
                    {
                        List<SonyNotification> notification = new List<SonyNotification>();
                        notification.Add(new SonyNotification() { Message = "Invalid credentials entered. Please try again.", Type = "error" });
                        TempData["Notification"] = notification;
                    }

                    //BugID: 0001751
                    //Nikhil Thakral commented this code for above bug ID
                    //if (UserDetails != null)
                    //    ViewBag.ErrorMessage = UserDetails.Message;
                    //else
                    //    ViewBag.ErrorMessage = "Login failed. Please try again.";
                }
                else
                {
                    List<SonyNotification> notification = new List<SonyNotification>();
                    notification.Add(new SonyNotification() { Message = "Captcha Authentication Failed. Please try again.", Type = "error" });
                    TempData["Notification"] = notification;
                }
            }
            return View("NewIndex", objLoginFormData);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session["BaseSession"] = null;
            Session["RoleRightsList"] = null;
            Session.Clear();
            Session.Abandon();
            return new RedirectResult(Url.Action("Index", "Login"));
            //return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult SessionTimeout()
        {
            Session["BaseSession"] = null;
            Session["RoleRightsList"] = null;
            Session.Clear();
            List<SonyNotification> notification = new List<SonyNotification>();
            notification.Add(new SonyNotification() { Message = "Your session has timed-out. Please re-login to continue.", Type = "error" });
            TempData["Notification"] = notification;
            //ViewData.Add("Notification", notification);
            //return View("Index");
            return new RedirectResult(Url.Action("Index", "Login"));
            //return RedirectToAction("Index", "Login");
        }

        public bool ValidateReCaptcha(ref string errorMessage)
        {
            var gresponse = Request["g-recaptcha-response"];
            string secret = "6LemZlgUAAAAAFsOIOf2mgXUSAeJxkinX1I8PMnp";
            string ipAddress = GetIpAddress();

            var client = new WebClient();

            string url = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}&remoteip={2}", secret, gresponse, ipAddress);


            var response = client.DownloadString(url);

            var captchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(response);

            if (captchaResponse.Success)
            {
                return true;
            }
            else
            {
                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        errorMessage = "The secret key parameter is missing.";
                        break;
                    case ("invalid-input-secret"):
                        errorMessage = "The given secret key parameter is invalid.";
                        break;
                    case ("missing-input-response"):
                        errorMessage = "The g-recaptcha-response parameter is missing.";
                        break;
                    case ("invalid-input-response"):
                        errorMessage = "The given g-recaptcha-response parameter is invalid.";
                        break;
                    default:
                        errorMessage = "reCAPTCHA Error. Please try again!";
                        break;
                }

                return false;
            }
        }

        string GetIpAddress()
        {
            var ipAddress = string.Empty;

            if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else if (!string.IsNullOrEmpty(Request.UserHostAddress))
            {
                ipAddress = Request.UserHostAddress;
            }

            return ipAddress;
        }

        public class ReCaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }
    }
}