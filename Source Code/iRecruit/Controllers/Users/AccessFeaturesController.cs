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
    public class AccessFeaturesController : ApiController
    {
        private readonly IUsersRepository _repo;
        private readonly ITraceWriter _tracer;

        public AccessFeaturesController(IUsersRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }
        
        public HttpResponseMessage Get(string id)
        {
            try
            {
                MenuModel menu = new MenuModel() { DashboardEnabled = true };
                Users res = this._repo.GetUserDetailFromDatabase(id);
                if (res != null)
                {
                    if(res.AccessFeatures.Contains("Administrator"))
                    {
                        menu.IndentEnabled = true;
                        menu.ReportsEnabled = true;
                        menu.ResumeManagementEnabled = true;
                        menu.SettingsEnabled = true;
                        menu.InterviewsEnabled = true;
                    }
                    if(res.AccessFeatures.Contains("Indent"))
                    {
                        menu.IndentEnabled = true;
                    }
                    if(res.AccessFeatures.Contains("Settings"))
                    {
                        menu.SettingsEnabled = true;
                    }
                    if(res.AccessFeatures.Contains("ResumeManagement"))
                    {
                        menu.ResumeManagementEnabled = true;
                    }
                    if(res.AccessFeatures.Contains("Interviews"))
                    {
                        menu.InterviewsEnabled = true;
                    }
                    
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, menu);

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