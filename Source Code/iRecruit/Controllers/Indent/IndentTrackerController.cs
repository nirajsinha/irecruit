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

namespace iRecruit.Controllers.Indent
{
    [AllowCrossSiteOrigin]
    public class IndentTrackerController : ApiController
    {
        private readonly IIndentRepository _repo;
        private readonly ITraceWriter _tracer;

        public IndentTrackerController(IIndentRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }

        public HttpResponseMessage Get(string id)
        {
            try
            {
                IndentTrackerInfoModel model = this._repo.GetIndentTrackerInfoModel(id);
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
        public HttpResponseMessage Get(int id, int page, int pageSize)
        {
            try
            {
                List<IndentTrackerData> res = this._repo.GetIndentTrackerData(id, page, pageSize);
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