using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using iRecruit.Repository;
using iRecruit.Repository.Interfaces;
using iRecruit.Helpers;
using System.Web.Http.Tracing;
using iRecruit.Models;
using iRecruit.Extensions;
using iRecruit.Security;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace iRecruit.Controllers
{
    public class HomeController : BaseController
    {
        [System.Web.Mvc.Authorize]
        public ActionResult Index()
        {
            if (Request.Cookies["UserID"] == null) return RedirectToAction("LogOn", "Account");
            return View();
        }
        [System.Web.Mvc.Authorize]
        public ActionResult Main()
        {
            if (Request.Cookies["UserID"] == null) return RedirectToAction("LogOn", "Account");
            return View();
        }
        
        [RequiredRole("Indent")]
        public ActionResult Indent()
        {
            return View();
        }
        [RequiredRole("Indent")]
        public ActionResult IndentTracker()
        {
            return View();
        }
        [RequiredRole("Indent")]
        public ActionResult Timeline()
        {
            return View();
        }
        [System.Web.Mvc.Authorize]
        public ActionResult Profile()
        {
            return View();
        }
        [RequiredRole("ResumeManagement")]
        public ActionResult Candidate()
        {
            return View();
        }
        [RequiredRole("ResumeManagement")]
        public ActionResult ResumeSearch()
        {
            return View();
        }
        [RequiredRole("ResumeManagement")]
        public ActionResult InterviewSchedule()
        {
            return View();
        }
        [RequiredRole("Interviews")]
        public ActionResult InterviewFeedback()
        {
            return View();
        }
        [RequiredRole("Administrator")]
        public ActionResult Users()
        {
            return View();
        }
        [RequiredRole("Administrator")]
        public ActionResult Departments()
        {
            return View();
        }
        [RequiredRole("Administrator")]
        public ActionResult Skills()
        {
            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult CookiePolicy()
        {
            return View();
        }
        public ActionResult AccessDenied()
        {
            return View();
        }
        [System.Web.Mvc.HttpPost]
        public JsonResult PostComments(string comment)
        {

            HttpCookie uCookie = Request.Cookies["UserInfo"];
            string from = null;
            if(uCookie != null)
            {
                UserInfo uinfo = JsonConvert.DeserializeObject<UserInfo>(uCookie.Value);
                if(uinfo != null) from = uinfo.Email;
            }
            
            string to = ConfigurationManager.AppSettings["FeedbackEmail"];
            string fromEmail = string.IsNullOrWhiteSpace(from) ? null : from;

            AntiXssSanitizer.HtmlEncode(comment);

            EmailMessage message = new EmailMessage() { To = to, Subject = "Feedback", From = fromEmail };
            message.Body = Server.UrlDecode(comment);
            new EmailHelper().Send(message, null, null, false);
            return Json("Success");
        }
    }
}