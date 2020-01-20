using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes(); 

            /*  routes.MapRoute("MoviesByRealeaseDate",
                  "movies/released/{year}/{month}",
                  new { controller = "Movies", action = "ByRealeaseDate"}, 
                  new { year = @"\d{4}", month = @"\d{2}" });*/ // \d{4} : contrainte pour prendre que 4 chiffre pr l'année et 2 pr le mois.

            //new { year = @"2015|2016", month = @"\d{2}" }); pour limiter le parametre year à prendre que 2015 & 2016.
            //ps: on met un @ car on a un anti-slash \, donc si on veut que ça marche faut mettre @. @ au lieu de \\ c mieux.
            //the reason we add this route(MapRoute) before the default maproute is because the order of these routes matters!

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
