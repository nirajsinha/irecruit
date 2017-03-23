using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using iRecruit.Security;
using System.Web.Http.Dispatcher;
using System.Net.Http;
using System.Web.Routing;

namespace iRecruit
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Just exclude the users controllers from need to provide valid token, so they could authenticate
			config.Routes.MapHttpRoute(
				name: "Authentication",
				routeTemplate: "api/users/{id}",
				defaults: new { controller = "users" }
			);
			
			// Web API configuration and services
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi", 
                routeTemplate: "api/{controller}/{id}", 
                defaults: new { id = RouteParameter.Optional }
            );//handler: new TokenInspector()

            
			//Global handler - applicable to all the requests
			
            //config.MessageHandlers.Add(new HttpsGuard());
            config.MessageHandlers.Add(new TokenInspector());
        }
    }
}
