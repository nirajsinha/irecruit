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
    public class IndentSearchController : ApiController
    {
        private readonly IIndentRepository _repo;
        private readonly ITraceWriter _tracer;

        public IndentSearchController(IIndentRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }
        
        public HttpResponseMessage Get(int id, string search, string type) // type = 3
        {
            try
            {
                List<iRecruit.Entity.Indent> res = this._repo.GetIndents(id);
                
                
                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (!string.IsNullOrWhiteSpace(type))
                    {
                        iRecruit.Entity.Type t = new MasterRepository().GetType(7, type);
                        if (!string.IsNullOrWhiteSpace(search))
                        {
                            res = res.FindAll(f => f.IndentNumber.ToUpper().Contains(search.ToUpper()) && f.Indent_Status == t.TypeID);
                        }
                    }
                    else
                    {
                        res = res.FindAll(f => f.IndentNumber.ToUpper().Contains(search.ToUpper()));
                    }
                }

                List<AutoCompleteResults> dic = new List<AutoCompleteResults>();
                foreach (iRecruit.Entity.Indent result in res)
                {
                    dic.Add(new AutoCompleteResults() { value = result.IndentNumber, label = result.IndentID.ToString() });
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, dic);
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