using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

using System.Web.Http;
using iRecruit.Security;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;

namespace iRecruit.Security
{
    public class FeatureGroupAuthorization : AuthorizeAttribute
    {
        private string _FeatureName = string.Empty;

        public FeatureGroupAuthorization(string FeatureName)
        {
            _FeatureName = FeatureName;
        }

        //public bool CheckUserAuthorization
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            const string TOKEN_NAME = "Auth-Token";
            string encryptedToken = actionContext.Request.Headers.GetValues(TOKEN_NAME).First();
            try
            {
                if (string.IsNullOrWhiteSpace(encryptedToken)) return false;
                Token token = Token.DecryptKey(encryptedToken.Replace("\"", ""));
                List<string> afs = token.AccessFeatures.Split(',').ToList();
                if (afs.Contains("Administrator")) return true;
                if (afs.Contains(_FeatureName)) return true;
            
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
                
        }

    }

}
