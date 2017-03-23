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
    public class UploadController : ApiController
    {
        private readonly IMasterRepository _repo;
        private readonly ITraceWriter _tracer;

        public UploadController(IMasterRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }
        
        public async Task<HttpResponseMessage> Post(string id)
        {
            try
            {
                if (!this.Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                string root = HttpContext.Current.Server.MapPath("~/App_Data/" + id + "/");
                try
                {
                    Directory.Delete(root, true);
                }
                catch
                {
                    // can throw error if directory not found
                }
                // create root path if does not exists
                new System.IO.FileInfo(root).Directory.Create();

                var provider = new MultipartFormDataStreamProvider(root);
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
                MultipartFileData file = null;
                string fileName = provider.FileData[0].Headers.ContentDisposition.FileName.Replace("\"", "");
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    file = provider.FileData[0];
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                if (file != null)
                {
                    //read file data and delete temporary file
                    string file1 = file.LocalFileName;
                    string fileNameWithExtension = fileName.Substring(fileName.LastIndexOf('\\') + 1);

                    // move temp file into dir
                    File.Copy(file1, root + fileNameWithExtension, true);
                    System.IO.File.Delete(file1);

                    HttpResponseMessage resp = Request.CreateResponse(HttpStatusCode.OK);
                    resp.Content = new StringContent(root + fileNameWithExtension);
                    resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
                    return resp;
                }


            }
            catch (System.Exception e)
            {
                _tracer.Error(Request, this.ControllerContext.ControllerDescriptor.ControllerType.FullName, e.StackTrace);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

    }
}