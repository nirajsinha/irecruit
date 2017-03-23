using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using iRecruit.Repository;
using iRecruit.Repository.Interfaces;
using System.Web.Http.Tracing;
using iRecruit.Models;
using iRecruit.Extensions;
using iRecruit.Security;
using System.Configuration;
using System.Web;
using Newtonsoft.Json;
using iRecruit.Entity;

namespace iRecruit.Controllers.User
{
    [AllowCrossSiteOrigin]
    public class AuthorizationController : ApiController
    {
        private readonly IUsersRepository _repo;
        private readonly ITraceWriter _tracer;

        public AuthorizationController(IUsersRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }
        
        public HttpResponseMessage Get(string id, string pwd, string appkey)
        {
            try
            {
                string userid = HttpUtility.UrlDecode(id);
                string Domain = ConfigurationManager.AppSettings["Domain"];

                bool isValidUser = this._repo.ValidateCredentials(userid, pwd, Domain);
                if (isValidUser)
                {
                    string ApplicationKey = ConfigurationManager.AppSettings["ApplicationKey"];
                    Users us = new UsersRepository().GetUserDetailFromDatabase(userid);
                    if (!ApplicationKey.Equals(appkey)) return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid application key");

                    Token token = new Token(userid, Request.GetClientIP(), appkey, us.AccessFeatures);
                    string res = token.EncryptKey();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, res);

                    return response;
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent("Invalid credentials!")
                    };
                }


            }
            catch (Exception ex)
            {
                _tracer.Error(Request, this.ControllerContext.ControllerDescriptor.ControllerType.FullName, ex.StackTrace);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }
            
        }

    }
}