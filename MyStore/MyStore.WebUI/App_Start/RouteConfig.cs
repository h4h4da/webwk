using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(null, "", new { controller="Product" ,action="List" ,categoryId=0 , page=1 });

            routes.MapRoute(null, "page{page}", new { controller = "Product", action = "List", categoryId = 0 }, new { page=@"\d+"});

            routes.MapRoute(null, "cid{categoryId}", new { controller = "Product", action = "List", page = 1 }, new { categoryId=@"\d+"});

            routes.MapRoute(null, "cid{categoryId}/page{page}", new { controller = "Product", action = "List" }, new { categoryId=@"\d+",page=@"\d+"});
            routes.MapRoute(
               name: null,
               url: "page{page}",
               defaults: new { controller = "Product", action = "List" }
           );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
