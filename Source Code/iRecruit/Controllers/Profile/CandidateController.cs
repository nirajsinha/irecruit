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
    public class CandidateController : ApiController
    {
        private readonly IProfileRepository _repo;
        private readonly ITraceWriter _tracer;

        public CandidateController(IProfileRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }

        
        public HttpResponseMessage Get(int id)
        {
            try
            {
                if (id > 0)
                {
                    Candidates cand = this._repo.GetCandidate(id);
                    Resumes resume = this._repo.GetResume(cand.CandidateID);
                    CandidateModel model = new CandidateModel()
                    {
                        Candidate = cand,
                        Resume = resume
                    };
                    if (!string.IsNullOrWhiteSpace(resume.ResumePath))
                    {
                        string f = resume.ResumePath.Substring(resume.ResumePath.LastIndexOf("\\") + 1);
                        model.ResumeVirtualPath = "/Resumes/" + resume.CandidateID + "/" + f; //Request.GetRequestContext().VirtualPathRoot + 
                    }
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, model);

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
        
        [FeatureGroupAuthorization("ResumeManagement")]
        public HttpResponseMessage Post(CandidateModel model)
        {
            try
            {
                string user = iRecruit.Security.Extensions.FetchUserFromRequestHeader(this.ActionContext);
                if (model.Candidate.CandidateID == 0) model.Candidate.CreatedBy = user;
                else model.Candidate.ModifiedBy = user;
                string path = model.Resume.ResumePath ?? "";
                if(! string.IsNullOrWhiteSpace(path)) model.Resume.FileType = new FileInfo(model.Resume.ResumePath).Extension;
                
                int candId = this._repo.SaveCandidate(model);
                //move indent to specific folder
                
                if (!string.IsNullOrWhiteSpace(path))
                {
                    string resumePath = HttpContext.Current.Server.MapPath("~/Resumes/" + candId + "/");
                    new System.IO.FileInfo(resumePath).Directory.Create();
                    
                    try
                    {
                        string fileName = model.Candidate.FirstName + " " + (model.Candidate.LastName ?? "") + model.Resume.FileType;
                        string jdFile = resumePath + fileName;
                        File.Copy(path, jdFile, true);
                        File.Delete(path);
                        this._repo.UpdateResumePath(candId, jdFile, model.Resume.FileType);

                    }
                    catch { }
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, true);
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