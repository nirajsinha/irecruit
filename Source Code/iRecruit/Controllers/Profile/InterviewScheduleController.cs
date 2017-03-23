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
    public class InterviewScheduleController : ApiController
    {
        private readonly IProfileRepository _repo;
        private readonly ITraceWriter _tracer;

        public InterviewScheduleController(IProfileRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }

        public HttpResponseMessage Get(int id, int round)
        {
            try
            {
                InterviewScheduleResult model = this._repo.GetInterviewSchedule(id, round);
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
        public HttpResponseMessage Post(InterviewSchedule schedule)
        {
            try
            {
                if (schedule != null)
                {
                    string user = iRecruit.Security.Extensions.FetchUserFromRequestHeader(this.ActionContext);
                    if (schedule.InterviewScheduleID == 0) schedule.CreatedBy = user;
                    else schedule.ModifiedBy = user;
                    string emailNotifications = this._repo.SaveInterviewSchedule(schedule);

                    if (!string.IsNullOrWhiteSpace(emailNotifications))
                    {
                        EmailHelper helper = new EmailHelper();
                        string calPath = HttpContext.Current.Server.MapPath("~/Schedules/Interview Schedule.ics");
                        new System.IO.FileInfo(calPath).Directory.Create();
                        try
                        {
                            helper.CreateCalenderEvent("", schedule.Subject, schedule.Description, schedule.StartTime, schedule.EndTime, ref calPath);

                            EmailMessage message = new EmailMessage() { To = emailNotifications, Subject = "Technical Interview - #" + schedule.CandidateID };
                            List<string> mailAttachment = new List<string>();
                            string resumePhysicalPath = "";
                            if (schedule.AttachResume.Equals("1"))
                            {
                                Resumes resume = this._repo.GetResume(schedule.CandidateID);
                                if (!string.IsNullOrWhiteSpace(resume.ResumePath))
                                {
                                    string f = resume.ResumePath.Substring(resume.ResumePath.LastIndexOf("\\") + 1);
                                    resumePhysicalPath = HttpContext.Current.Server.MapPath("~/Resumes/" + resume.CandidateID + "/" + f);
                                    mailAttachment.Add(resumePhysicalPath);
                                }
                            }

                            helper.Send(message, mailAttachment, calPath, false);
                        }
                        finally
                        {
                            if (!string.IsNullOrWhiteSpace(calPath))
                                try
                                {
                                    File.Delete(calPath);
                                }catch{ }
                        }
                        // cleanup temp files
                        
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