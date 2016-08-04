using System.Web.Http;
using Sunrise.API.Filter;


namespace Sunrise
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new ExceptionFilters());
        }
    }
}
