using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using iRecruit.Controllers;
using Newtonsoft.Json;
using System.Xml;
using iRecruit.Repository;

namespace iRecruit.Extensions
{
    public class RequiredRoleAttribute : AuthorizeAttribute
    {
        private string _FeatureName = string.Empty;

        public RequiredRoleAttribute(string FeatureName)
        {
            _FeatureName = FeatureName;
        }
        
        //public bool CheckUserAuthorization
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
                        
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                // The user is not authorized => no need to continue
                return false;
            }
            
            // At this stage we know that the user is authorized => we can fetch the username
            string userid = httpContext.User.Identity.Name;
            iRecruit.Entity.Users user = new UsersRepository().GetUserDetailFromDatabase(userid);
            List<string> afs = user.AccessFeatures.Split(',').ToList();
            if (afs.Contains("Administrator")) return true;
            if (afs.Contains(_FeatureName)) return true;
            
            // Redirect to access denied if user is not authorized for the role
            httpContext.Response.Redirect("~/Home/AccessDenied"); //?ReturnUrl=" + httpContext.Server.UrlDecode(returnUrl));

            return false;
        }

    }
    
}
