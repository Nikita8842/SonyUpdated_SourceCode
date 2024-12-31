using AMBOModels.UserValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AMBOWeb.Classes;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Filters
{
    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {
        public Int64 ModuleId { get; set; }
        public Int64 SubModuleId { get; set; }
        public int Rights { get; set; }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var roleRightsList = (List<RoleRights>)(httpContext.Session["RoleRightsList"]);
            RoleRights submodulerights = new RoleRights();
            Int64 roleid=0;
            var userDetails = (BaseSession)(httpContext.Session["BaseSession"]);
            if (userDetails != null)
                roleid = userDetails.RoleId;
            //added by Nikhil Thakral on 18/07/2018
            //checking if values received from session is null
            if (roleRightsList!= null && roleRightsList.Count > 0)
                submodulerights = roleRightsList.Where(r => r.ModuleId == ModuleId && r.SubModuleId == SubModuleId && r.RoleId == roleid).FirstOrDefault();

            if (submodulerights != null)
            {
                switch (Rights)
                {
                    case (int)Right.View:
                        if (submodulerights.ViewPermission == true)
                            return true;
                        break;

                    case (int)Right.Create:
                        if (submodulerights.CreatePermission == true)
                            return true;
                        break;

                    case (int)Right.Edit:
                        if (submodulerights.EditPermission == true)
                            return true;
                        break;

                        case (int)Right.Delete:
                            if (submodulerights.DeletePermission == true)
                                return true;
                            break;
                    }
                }
            return false;
        }
            //var userDetails = (BaseSession)(httpContext.Session["BaseSession"]);
            //if (userDetails.UserId > -1)
            //{
            //    List<string> rolesList = Roles.Split(',').ToList();

            //    if (rolesList.Contains(userDetails.RoleName))
            //    {
            //        return true;
            //    }
            //}

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RouteValueDictionary objRoute=null;
            if(filterContext.HttpContext.Session["BaseSession"] == null)
                objRoute = new RouteValueDictionary(
                    new
                    {
                        controller = "Login",
                        action = "Index"
                    });
            else if (filterContext.HttpContext.Request != null && filterContext.HttpContext.Request.IsAuthenticated)
            {
                objRoute = new RouteValueDictionary(
                    new
                    {
                        controller = "Login",
                        action = "Index"
                    });
            }
            else
            {
                objRoute= new RouteValueDictionary(
                              new
                              {
                                   action = "UnauthorizedAccess",
                                   controller = "Error"
                              });
            }
            filterContext.Result = new RedirectToRouteResult(objRoute);
        }
    }
}