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
using iRecruit.Entity;
using System.IO;
using System.Net.Http.Headers;
using iRecruit.Helpers;

namespace iRecruit.Controllers
{
    [AllowCrossSiteOrigin]
    public class IndentActivitiesController : ApiController
    {
        private readonly IIndentRepository _repo;
        private readonly ITraceWriter _tracer;

        public IndentActivitiesController(IIndentRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }
        
        public HttpResponseMessage Get(string id)
        {
            try
            {
                ActivityLogModel model = new ActivityLogRepository().GetActivityLogs(id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, model);

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