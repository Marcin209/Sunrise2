using System.Web.Http;

namespace Sunrise
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "Sunrise API",
                routeTemplate: "api/{controller}");
        }
    }
}
