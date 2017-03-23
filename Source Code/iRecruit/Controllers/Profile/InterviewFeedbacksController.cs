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
    public class InterviewFeedbacksController : ApiController
    {
        private readonly IProfileRepository _repo;
        private readonly ITraceWriter _tracer;

        public InterviewFeedbacksController(IProfileRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }

        
        public HttpResponseMessage Get(int id)
        {
            try
            {
                InterviewFeedbackModel model = this._repo.GetInterviewFeedbacks(id);
                if (model != null)
                {
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
        public HttpResponseMessage Post(InterviewFeedbacks feedback)
        {
            try
            {
                if (feedback != null)
                {
                    string user = iRecruit.Security.Extensions.FetchUserFromRequestHeader(this.ActionContext);
                    if(feedback.InterviewFeedbacksID == 0) feedback.CreatedBy = user;
                    else feedback.ModifiedBy = user;
                    int candId = this._repo.SaveInterviewFeedback(feedback);
                    if(candId > 0)
                    {
                        string toNotification = this._repo.ExecuteInterviewWorkflow(candId);
                        if (!string.IsNullOrWhiteSpace(toNotification))
                        {
                            EmailMessage message = new EmailMessage() { To = toNotification, Subject = "Interview Feedback" };
                            string messageBody = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/Templates/InterviewFeedback.html"));
                            messageBody = messageBody.Replace("##CandidateID##", candId.ToString());
                            Candidates cand = _repo.GetCandidate(candId);
                            if (cand != null)
                            {
                                messageBody = messageBody.Replace("##CandidateName##", cand.FirstName + " " + cand.LastName);
                                message.Body = messageBody;
                                new EmailHelper().Send(message, null, null);
                            }
                        }
                    }
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, true);
                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, new Exception("No data found to save"));
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