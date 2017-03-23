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
    public class ADUserController : ApiController
    {
        private readonly IUsersRepository _repo;
        private readonly ITraceWriter _tracer;

        public ADUserController(IUsersRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }
        
        public HttpResponseMessage Get(string id)
        {
            try
            {
                Dictionary<string, string> dic = this._repo.GetUserDetails(id);
                Users res = new Users()
                {
                    UserID = id,
                    Name = dic["cn"],
                    Title = dic["title"],
                    Email = dic["mail"],
                    Photo = dic["thumbnailPhoto"]
                };
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, res);

                return response;
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

        public HttpResponseMessage Post(string id)
        {
            try
            {
                List<AutoCompleteResults> res = this._repo.GetUsersFromAD(id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, res);
                return response;
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