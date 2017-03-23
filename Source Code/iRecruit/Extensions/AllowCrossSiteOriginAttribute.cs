using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace iRecruit.Extensions
{
    public class AllowCrossSiteOriginAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var allowOrigin = "*";
            if (actionExecutedContext.Request != null)
            {
                   var origin = actionExecutedContext.Request.Headers.FirstOrDefault(x => x.Key == "Origin");
                   if (origin.Key != null)
                    allowOrigin = origin.Value.FirstOrDefault();
            }
                
            if (actionExecutedContext.Response != null)
            {
                    actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", allowOrigin);
                    actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                   actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            }

            base.OnActionExecuted(actionExecutedContext);

        }
    }


}