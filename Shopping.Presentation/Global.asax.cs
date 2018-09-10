using Shopping.Presentation.App_Start;
using Shopping.Presentation.CustomModelBinders;
using Shopping.Presentation.Log;
using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Shopping.Presentation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(CustomerDTO), new AddCustomerRole());
            GlobalFilters.Filters.Add(new AuthorizationFilter());
            GlobalFilters.Filters.Add(new CheckForAdmin());
            DbInterception.Add(new CustomerInterceptorLogging());
            DbInterception.Add(new CustomInterceptorTransientErrorspublic());
        }
    }
}
