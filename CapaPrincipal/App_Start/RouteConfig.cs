using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web_Application
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                        );*/

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Access", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Create_code",
                url: "Create_code",
                defaults: new { controller = "Access", action = "Create_code" }
            );

            routes.MapRoute(
                name: "Send_pass",
                url: "Send_pass",
                defaults: new { controller = "Access", action = "Send_pass" }
            );

            routes.MapRoute(
                name: "New_pass",
                url: "New_pass",
                defaults: new { controller = "Access", action = "New_pass" }
            );
        }
    }
}
