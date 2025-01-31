﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SonyAmbo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MvcHandler.DisableMvcResponseHeader = true;
            //for logging request before controller is created and logging response before being sent to the client
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new APILogger());
        }

        protected void Application_EndRequest()
        {
            //to disable system info in response headers, as requested by client on 28 Aug 2018
            //Added by Nikhil Thakral
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-Powered-By");
            Response.Headers.Remove("X-SourceFiles");
            Response.Headers.Remove("Server");
        }
    }
}
