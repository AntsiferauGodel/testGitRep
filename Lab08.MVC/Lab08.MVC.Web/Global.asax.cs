using Lab08.MVC.Web.App_Start;
using Lab08.MVC.Web.AutofacConfigs;
using Lab08.MVC.Web.Filters;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Lab08.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalFilters.Filters.Add(new ExceptionLoggerAttribute());
            AutofacConfig.ConfigureContainer();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
