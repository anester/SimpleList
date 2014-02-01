using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleList
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "UserList",
            //    url: "List/{username}/{userlistid}",
            //    defaults: new { controller = "ListItem", action = "Index", userlistid = 0 }
            //);

            //routes.MapRoute(
            //    name: "Default2",
            //    url: "{controller}/{action}/{username}/{userlistid}/{id}",
            //    defaults: new { controller = "Home", action = "Index", username="", userlistid=0, id = UrlParameter.Optional }
            //);


            routes.MapRoute(
                name: "MyLists",
                url: "MyLists",
                defaults: new
                {
                    controller = "List",
                    action = "MyLists",
                    listid = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Lists",
                url: "Lists/{loginname}/{listid}",
                defaults: new { 
                    controller = "List", 
                    action = "Index", 
                    listid = UrlParameter.Optional 
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default1",
                url: "{controller}/{action}/{username}/{id}",
                defaults: new { controller = "Home", action = "Index", username = "", id = UrlParameter.Optional }
            );

        }
    }
}
