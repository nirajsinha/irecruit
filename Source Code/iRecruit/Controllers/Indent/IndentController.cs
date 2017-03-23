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
    public class IndentController : ApiController
    {
        private readonly IIndentRepository _repo;
        private readonly ITraceWriter _tracer;

        public IndentController(IIndentRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }
        public HttpResponseMessage Get()
        {
            try
            {
                List<iRecruit.Entity.Indent> res = this._repo.GetIndents(1);
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
        public HttpResponseMessage Get(string id)
        {
            try
            {
                iRecruit.Entity.Indent ind = this._repo.GetIndent(id);
                string user = iRecruit.Security.Extensions.FetchUserFromRequestHeader(this.ActionContext);
                DepartmentRoles roles = new MasterRepository().GetDepartmentRoles().Where( f=> f.BranchID.Equals(ind.BranchID) && f.DepartmentID.Equals(ind.DepartmentID)).FirstOrDefault();
                IndentModel model = new IndentModel(){ Indent = ind,
                                                       DisableIndenterAccess = 1,
                                                       DisableFHAccess = 1,
                                                       DisableSVPAccess = 1
                                                    };

                if ((ind.AssignedTo??"").Length == 0 && (ind.CreatedBy??"").Equals(user))
                    model.DisableIndenterAccess = 0;
                if (roles != null)
                {
                    if ((roles.FunctionHead ?? "").Equals(user))
                        model.DisableFHAccess = 0;
                    if ((roles.SVP ?? "").Equals(user))
                        model.DisableSVPAccess = 0;
                }
                // build jd path
                string filePath = ind.UploadFile_Indents??"";
                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    string f = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                    model.JDUrl = "/JobDescriptions/" + ind.IndentNumber + "/" + f; //Request.GetRequestContext().VirtualPathRoot + 
                }
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
        [FeatureGroupAuthorization("Indent")]
        public HttpResponseMessage Post(iRecruit.Entity.Indent indent)
        {
            try
            {
                string user = iRecruit.Security.Extensions.FetchUserFromRequestHeader(this.ActionContext);
                indent.StatusChangedBy = user;
                if (string.IsNullOrWhiteSpace(indent.IndentNumber))
                {
                    indent.CreatedBy = user;
                }
                else
                    indent.ModifiedBy = user;

                int indentId = this._repo.SaveIndent(indent);
                ExecuteIndentWorkFlowResult result = this._repo.ExecuteIndentWorkflow(indentId);
                if (result != null)
                {
                    //move indent to specific folder
                    string path = indent.UploadFile_Indents??"";
                    if(! string.IsNullOrWhiteSpace(path))
                    {
                        string jdPath = HttpContext.Current.Server.MapPath("~/JobDescriptions/" + result.IndentNumber + "/");
                        new System.IO.FileInfo(jdPath).Directory.Create();
                        string fileNameWithExtension = path.Substring(path.LastIndexOf('\\') + 1);
                        try
                        {
                            string jdFile = jdPath + fileNameWithExtension;
                            File.Copy(path, jdFile, true);
                            File.Delete(path);
                            this._repo.UpdateJDFilePath(indentId, jdFile);
                        }
                        catch { }
                    }
                    // send email notifications
                    if (!string.IsNullOrWhiteSpace(result.ToEmailNotifications))
                    {
                        EmailMessage message = new EmailMessage() { To = result.ToEmailNotifications, Cc=result.CcEmailNotifications, Subject = "Indent Notification" };
                        string url = string.Format("{0}://{1}/home/indent/{2}", this.ActionContext.Request.RequestUri.Scheme, this.ActionContext.Request.RequestUri.Authority, result.IndentNumber);
                        string messageBody = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/Templates/IndentInfo.html"));
                        messageBody = messageBody.Replace("##IndentNumber##", result.IndentNumber);
                        messageBody = messageBody.Replace("##Url##", url);
                        message.Body = messageBody;
                        new EmailHelper().Send(message, null, null);
                        
                    }
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