using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CDSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "ApiConfirmEmail",
               url: "Api/confirmemail",
               defaults: new { controller = "Deal", action = "ConfirmEmailJson" }
           );

            routes.MapRoute(
               name: "ApiRegister",
               url: "Api/Register",
               defaults: new { controller = "Deal", action = "RegisterJson" }
           );

            

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
