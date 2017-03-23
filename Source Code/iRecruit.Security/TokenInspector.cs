using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.Web;


namespace iRecruit.Security
{
    public class TokenInspector : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            const string TOKEN_NAME = "Auth-Token";
            // check jsonp request
            var query = HttpUtility.ParseQueryString(request.RequestUri.Query);
            var queryVal = query["callback"];

            // if not jsonp and users request then check security token
            var url = request.RequestUri.AbsoluteUri.ToLower();
            if (string.IsNullOrEmpty(queryVal) && !(url.Contains("authorization") || url.Contains("upload")))
            {
                if (request.Headers.Contains(TOKEN_NAME))
                {
                    string encryptedToken = request.Headers.GetValues(TOKEN_NAME).First();
                    try
                    {
                        //Token token = Token.Decrypt(encryptedToken);
                        Token token = Token.DecryptKey(encryptedToken.Replace("\"", ""));
                        string ApplicationKey = ConfigurationManager.AppSettings["ApplicationKey"];
                        bool isRequestKeyMatchesTokenKey = token.ApplicationKey.Equals(ApplicationKey);
                        bool isRequestAddressMatchesTokenAddress = token.ClientAddress.Equals(request.GetClientIP());

                        if (!(isRequestKeyMatchesTokenKey && isRequestAddressMatchesTokenAddress))
                        {
                            HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid indentity or unauthorized access");
                            return Task.FromResult(reply);
                        }
                    }
                    catch (Exception ex)
                    {
                        HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Anauthorized access");
                        return Task.FromResult(reply);
                    }
                }
                else
                {
                    HttpResponseMessage reply = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Missing authorization");
                    return Task.FromResult(reply);
                }
            }
            return base.SendAsync(request, cancellationToken);
        }

    }
}