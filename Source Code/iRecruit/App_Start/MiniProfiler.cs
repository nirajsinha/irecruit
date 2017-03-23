using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using StackExchange.Profiling;
using StackExchange.Profiling.Helpers;
using StackExchange.Profiling.EntityFramework6;
using Microsoft.Web.Infrastructure;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using iRecruit.Filters;
using iRecruit.Helpers;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(iRecruit.MiniProfilerPackage), "PreStart")]
//[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(iRecruit.MiniProfilerPackage), "PostStart")]

namespace iRecruit 
{
    public static class MiniProfilerPackage
    {
        public static void PreStart()
        {

            // Be sure to restart you ASP.NET Developement server, this code will not run until you do that. 

            //TODO: See - _MINIPROFILER UPDATED Layout.cshtml
            //      For profiling to display in the UI you will have to include the line @StackExchange.Profiling.MiniProfiler.RenderIncludes() 
            //      in your master layout

            //TODO: Non SQL Server based installs can use other formatters like: new StackExchange.Profiling.SqlFormatters.InlineFormatter()
            MiniProfiler.Settings.SqlFormatter = new StackExchange.Profiling.SqlFormatters.SqlServerFormatter();

			//TODO: To profile a standard DbConnection: 
			// var profiled = new ProfiledDbConnection(cnn, MiniProfiler.Current);

            //TODO: If you are profiling EF code first try: 
			MiniProfilerEF6.Initialize();

            // Make profiling window appear right
            MiniProfiler.Settings.PopupRenderPosition = RenderPosition.Right;

            //Make sure the MiniProfiler handles BeginRequest and EndRequest
            DynamicModuleUtility.RegisterModule(typeof(MiniProfilerStartupModule));

            //Setup profiler for Controllers via a Global ActionFilter
            GlobalFilters.Filters.Add(new ProfilingActionFilter());

			// You can use this to check if a request is allowed to view results
            //MiniProfiler.Settings.Results_Authorize = (request) =>
            //{
                // you should implement this if you need to restrict visibility of profiling on a per request basis 
            //    return !DisableProfilingResults; 
            //};

            // the list of all sessions in the store is restricted by default, you must return true to alllow it
            //MiniProfiler.Settings.Results_List_Authorize = (request) =>
            //{
                // you may implement this if you need to restrict visibility of profiling lists on a per request basis 
                //return true; // all requests are kosher
            //};
            
            var ignored = MiniProfiler.Settings.IgnoredPaths.ToList();

            ignored.Add("WebResource.axd");
            ignored.Add("/Styles/");

            MiniProfiler.Settings.IgnoredPaths = ignored.ToArray();
            
        }

        public static void PostStart()
        {
            // Intercept ViewEngines to profile all partial views and regular views.
            // If you prefer to insert your profiling blocks manually you can comment this out
            var copy = ViewEngines.Engines.ToList();
            ViewEngines.Engines.Clear();
            foreach (var item in copy)
            {
                ViewEngines.Engines.Add(new ProfilingViewEngine(item));
            }
        }
    }

    public class MiniProfilerStartupModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) =>
            {
                var request = ((HttpApplication)sender).Request;
                //TODO: Might want to decide here (or maybe inside the action) whether you want
                // to profile this request - for example, using an "IsSystemAdmin" flag against
                // the user, or similar; this could also all be done in action filters, but this
                // is simple and practical; just return null for most users. For our test, we'll
                // profile only for local requests (seems reasonable)
                if (request.IsLocal)
                {
                    MiniProfiler.Start();
                }
                
            };


            // TODO: You can control who sees the profiling information
            /*
            context.AuthenticateRequest += (sender, e) =>
            {
                if (!CurrentUserIsAllowedToSeeProfiler())
                {
                    StackExchange.Profiling.MiniProfiler.Stop(discardResults: true);
                }
            };
            */

            context.EndRequest += (sender, e) =>
            {
                MiniProfiler.Stop();
            };
        }

        public void Dispose() { }
    }
}

