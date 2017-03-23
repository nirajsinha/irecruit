using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;

namespace iRecruit.Security
{
    public static class Extensions
    {
        public static string GetClientIP(this HttpRequestMessage request)
        {
            return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
        }

        public static Dictionary<string, string> ToDictionary(this string keyValue)
        {
            return keyValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                          .Select(part => part.Split('='))
                          .ToDictionary(split => split[0], split => split[1]);    
        }

        public static string FetchUserFromRequestHeader(HttpActionContext actionContext)
        {

            string encryptedToken = actionContext.Request.Headers.GetValues("Auth-Token").First();
            if (string.IsNullOrWhiteSpace(encryptedToken)) return "";
            Token token = Token.DecryptKey(encryptedToken.Replace("\"", ""));
            if (token != null)
            {
                return token.User;
            }
            return "";
            
        }
    }
}