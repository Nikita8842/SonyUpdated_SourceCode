using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Filters
{
    public class SessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["UserId"] == null)
            {
                filterContext.HttpContext.Response.StatusCode = 401;
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    // For AJAX requests, return result as a simple string, 
                    // and inform calling JavaScript code that a user should be redirected.
                    JsonResult result = new JsonResult
                    {
                        Data = new
                        {
                            isRedirect = true
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                    filterContext.Controller.TempData.Add("SessionTimeout", "Session has timed out. Please re-login.");
                    filterContext.Result = result;

                }
                else
                {
                    //filterContext.RouteData.Values.Add("SessionTimeout", "Session has timed out. Please re-login.");
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Login", action = "Index" })
                    );
                    filterContext.Controller.TempData.Add("SessionTimeout", "Session has timed out. Please re-login.");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}