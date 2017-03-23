using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using System.Threading.Tasks;
using iRecruit.Repository;
using iRecruit.Repository.Interfaces;
using System.Web.Http.Tracing;
using iRecruit.Models;
using iRecruit.Extensions;
using iRecruit.Security;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using iRecruit.Logging;
using System.Web.Http.Results;
using iRecruit.Helpers;
using Newtonsoft.Json;
using iRecruit.Entity;

namespace iRecruit.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUsersRepository _repo;
        private readonly ITraceWriter _tracer;
        
        public AccountController(IUsersRepository repo, ITraceWriter logger)
        {
            this._repo = repo;
            this._tracer = logger;
        }
        //
        // GET: /Account
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/LogOn
        [System.Web.Mvc.HttpGet]
        public ActionResult LogOn(string returnUrl)
        {
            //So that the user can be referred back to where they were when they click logon
            if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
                returnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnUrl = returnUrl;
            }
            return PartialView();
        }

        //
        // POST: /Account/LogOn

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LogOn(LogOnModel model, string returnUrl)
        {

            try
            {
                
                if (ModelState.IsValid)
                {
                    //string returnUrl = model.ReturnUrl;
                    string userid = HttpUtility.UrlDecode(model.UserID);
                    //string Domain = ConfigurationManager.AppSettings["Domain"];
                    //bool isValidUser = this._repo.ValidateCredentials(userid, model.Password, Domain);
                    if (Membership.ValidateUser(userid, model.Password))
                    {
                        string ApplicationKey = ConfigurationManager.AppSettings["ApplicationKey"];
                        Users us = new UsersRepository().GetUserDetailFromDatabase(userid);
                        Token token = new Token(userid, ControllerContext.HttpContext.Request.UserHostAddress, ApplicationKey, us.AccessFeatures);
                        string res = token.EncryptKey();
                        if (!string.IsNullOrWhiteSpace(res))
                        {
                            Response.Cookies.Add(new HttpCookie("AuthToken", HttpUtility.UrlEncode(res)) { Expires = DateTime.Now.AddDays(1) });
                            Response.Cookies.Add(new HttpCookie("UserID", userid) { Expires = DateTime.Now.AddDays(1) });

                            MasterRepository rep = new MasterRepository();
                            Company com = rep.GetCompanyInfo(1);// temp company id
                            
                            string userInfo = JsonConvert.SerializeObject(new UserInfo() 
                                                                            { 
                                                                                UserID = userid,
                                                                                CompanyID = com.CompanyID,
                                                                                CompanyName = com.Name,
                                                                                Name = us.Name, 
                                                                                Title = us.Title,
                                                                                Email = us.Email
                                                                            });
                            
                            Response.Cookies.Add(new HttpCookie("UserInfo", userInfo) { Expires = DateTime.Now.AddDays(1) });
                            
                            FormsAuthentication.SetAuthCookie(model.UserID, model.RememberMe);
                            string decodedUrl = "";
                            if (!string.IsNullOrEmpty(returnUrl))  decodedUrl = Server.UrlDecode(returnUrl);
                            if (Url.IsLocalUrl(decodedUrl))
                            {
                                return Redirect(decodedUrl.Replace("/home","/#"));
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid credentials. Please try again!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid input. Please enter correct fields and try again!");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            // If we got this far, something failed, redisplay form
            return PartialView(model);
        }

        //
        // GET: /Account/SignOut
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            // clear all system generated cookies
            Response.Cookies.Clear();

            // Fail safe. Expire all application cookies 
            if (Response.Cookies["UserID"] != null) Response.Cookies["UserID"].Expires = DateTime.Now.AddYears(-30);
            if (Response.Cookies["UserInfo"] != null) Response.Cookies["UserInfo"].Expires = DateTime.Now.AddYears(-30);
            
            this.ShowMessage(Extensions.MessageType.Warning, "You have been successfully logged out from system. Please close the browser to ensure all traces are deleted.");
            return PartialView("LogOn");
        }
    }
    public class LogOnModel
    {
        [Display(Name = "User ID")]
        [Required(ErrorMessage = "User ID is required.")]
        public string UserID { get; set; }
        
        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public string RoleGroup { get; set; }

    }
    public class UserInfo
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string DOJ { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
    }
}