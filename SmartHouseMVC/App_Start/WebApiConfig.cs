using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SmartHouseMVC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{act}/{name}/{source}",
                defaults: new { act = RouteParameter.Optional, name = RouteParameter.Optional, source = RouteParameter.Optional }
            );
            // Отключаем возможность вывода данных в формате XML
            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}
