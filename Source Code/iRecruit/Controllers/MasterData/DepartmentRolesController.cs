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
using System.Net.Http.Headers;
using System.IO;

namespace iRecruit.Controllers.MasterData
{
    [AllowCrossSiteOrigin]
    public class DepartmentRolesController : ApiController
    {
        private readonly IMasterRepository _repo;
        private readonly ITraceWriter _tracer;

        public DepartmentRolesController(IMasterRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }
        
        public HttpResponseMessage Get(int id)
        {
            try
            {
                List<DepartmentRoles> obj = _repo.GetDepartmentRoles();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, obj);
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
        
        [FeatureGroupAuthorization("Administrator")]
        public HttpResponseMessage Post(DepartmentRoles role)
        {
            try
            {
                bool res = this._repo.SaveDepartmentRoles(role);
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