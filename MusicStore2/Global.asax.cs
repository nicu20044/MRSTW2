﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MusicStore2
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
           AreaRegistration.RegisterAllAreas();
           RouteConfig.RegisterRoutes(RouteTable.Routes);
           
           //DependencyConfig.RegisterDependencies();
        }
    }
}