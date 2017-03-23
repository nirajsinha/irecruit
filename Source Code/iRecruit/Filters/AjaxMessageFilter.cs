﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iRecruit.Filters
{
    /// <summary>
    /// If we're dealing with ajax requests, any message that is in the view data goes to
    /// the http header.
    /// </summary>
    public class AjaxMessagesFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var viewData = filterContext.Controller.ViewData;
                var response = filterContext.HttpContext.Response;

                foreach (var messageType in Enum.GetNames(typeof(iRecruit.Extensions.MessageType)))
                {
                    var message = viewData.ContainsKey(messageType)
                                    ? viewData[messageType]
                                    : null;
                    if (message != null) // We store only one message in the http header. First message that comes wins.
                    {
                        response.AddHeader("X-Message-Type", messageType);
                        response.AddHeader("X-Message", message.ToString());
                        return;
                    }
                }
            }
        }
    }
}