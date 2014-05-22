using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KimeBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "NotFound",
                url: "404",
                defaults: new { controller = "StaticPage", action = "NotFound" }
            );


            routes.MapRoute(
                name: "Projects",
                url: "projects",
                defaults: new { controller = "StaticPage", action = "Projects" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "NotFound",
            //    url : "{*url}",
            //     defaults: new { controller = "StaticPage", action = "NotFound" }
            //    );
        }
    }
}
