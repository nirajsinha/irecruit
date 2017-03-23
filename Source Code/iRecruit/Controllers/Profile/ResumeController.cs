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

namespace iRecruit.Controllers.Profile
{
    [AllowCrossSiteOrigin]
    public class ResumeController : ApiController
    {
        private readonly IProfileRepository _repo;
        private readonly ITraceWriter _tracer;

        public ResumeController(IProfileRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }
        
        public HttpResponseMessage Get(int id)
        {
            try
            {
                if(id > 0)
                {
                    Resumes resume = this._repo.GetResume(id);
                    string resumeFileName = resume.ResumePath;
                    string resumeFile = "";
                    string resumeVirtualPath ="";

                    if (!string.IsNullOrWhiteSpace(resumeFileName))
                    {
                        resumeFile = new FileInfo(resumeFileName).Name;
                        resumeVirtualPath = "/" + resumeFileName.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty).Replace("\\", "/");
                    }

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new {
                        ResumeFileName = resumeFile,
                        ResumeVirtualPath = resumeVirtualPath
                    });

                    return response;
                }
                else return Request.CreateErrorResponse(HttpStatusCode.NoContent, new Exception("No input found to get data"));
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
        // search
        public HttpResponseMessage Post(ResumeSearchModel model)
        {
            try
            {
                List<ResumeSearchResult> res = this._repo.SearchCandidates(model);
                foreach (ResumeSearchResult obj in res)
                { 
                    string resumeFilePath = obj.ResumeVirtualPath;
                    if (!string.IsNullOrWhiteSpace(resumeFilePath))
                    {
                        obj.ResumeFileName = new FileInfo(resumeFilePath).Name;
                        obj.ResumeVirtualPath = "/" + resumeFilePath.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty).Replace("\\", "/");
                    }
                }
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