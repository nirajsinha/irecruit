using iRecruit.Filters;
using iRecruit.Helpers;
using Ninject;
using Ninject.Parameters;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace iRecruit
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        
        private static IKernel NinjectKernel { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //GlobalConfiguration.Configuration.Formatters.Insert(0, new JsonpFormatter());
            
            //int formatterCount = GlobalConfiguration.Configuration.Formatters.Count;
            //int formatterIndex = 0;
            //if (formatterCount > 0) formatterIndex = formatterCount;
            //GlobalConfiguration.Configuration.Formatters.Insert(formatterIndex, new JsonpFormatter());
            
            GlobalConfiguration.Configuration.EnsureInitialized();

            // Keep only razor view engine for parsing views
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }


        /// <summary>
        /// Set the kernel used by the MvcApplication
        /// </summary>
        /// <param name="kernel"></param>
        public static void InitializeInjection(IKernel kernel)
        {
            NinjectKernel = kernel;
        }

        /// <summary>
        /// Shortcut to resolve the specified type through the Ninject dependency injection mechanism
        /// </summary>
        /// <typeparam name="T">The type to resolve</typeparam>
        /// <returns>The result of invoking the NinjectKernel's Get with no arguments</returns>
        public static T Inject<T>(params IParameter[] parameters)
        {
            return NinjectKernel.Get<T>(parameters);
        }

        public static object Inject(Type t)
        {
            return NinjectKernel.Get(t);
        }

        // Stops asp.net runtime to keep changing sessionid on every request
        protected void Session_Start(Object sender, EventArgs e)
        {
            Session["init"] = 0;
        }

    }
}
