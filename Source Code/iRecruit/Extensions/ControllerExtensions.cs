using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iRecruit.Helpers;

namespace iRecruit.Extensions
{
    public static class ControllerExtensions
    {
        public static void ShowMessage(this BaseController controller, MessageType messageType, string message, bool showAfterRedirect = true)
        {
            var messageTypeKey = messageType.ToString();
            if (showAfterRedirect)
            {
                controller.TempData[messageTypeKey] = message;
            }
            else
            {
                controller.ViewData[messageTypeKey] = message;
            }
        }
    }
}