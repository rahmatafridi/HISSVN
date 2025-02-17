﻿using HIS.BLL;
using HIS.Domain.Models.Common;
using HIS.Domain.Models.Module;
using HIS.Domain.Models.Organization;
using HIS.Domain.Models.User;
using HIS.Web.Filters;
using HIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIS.Web.Controllers
{
    public class LoginController : Controller
    {
        #region Intialization
        public IUserBll _user { get; set; }
        public IModuleBll _moduleBll { get; set; }
        public IOrganizationBLL _organizationBll { get; set; }
        public ILoginBll _loginBll { get; set; }
        public LoginController()
        {
            _user = new UserBll();
            _moduleBll = new ModuleBLL();
            _organizationBll = new OrganizationBLL();
            _loginBll = new LoginBll();
        }
        #endregion

        #region Login/LogOut

        public ActionResult Index(string msg)
        {
            ViewBag.Message = msg;

            return View();
        }

        public ActionResult LoginUser(string username, string password)
        {

            User user = new User();
            user = _user.LoginUser(username, password);
            user = user ?? new User();


            if (user.UserId > 0 && user.IsActive)
            {
                Session["User"] = user;
                Session["UserRights"] = _user.GetUserRights(user.UserId);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                string msg = "";

                if (user.UserId > 0 && !user.IsActive)
                {
                    msg = "User doesnt seem to be active at the moment";
                }
                else
                {
                    msg = "Invalid credentials";
                }

                return RedirectToAction("Index", new { msg = msg });
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();

            return RedirectToAction("Index");
        }

        #endregion

        #region Change Password

        public ActionResult ChangePasswordForm()
        {
            return View();
        }

        [HttpPost]          
        [ValidateAjax]
        public ActionResult ChangePasswordForm(string oldPassword,string newPassword)
        {
            if (!ModelState.IsValid)
            {
                
                return new AjaxableViewResult(oldPassword);
            }
            
            
            AjaxResponse res = new AjaxResponse();

            int loginId = Helper.GetLoggedInUserId();
            if (!_loginBll.DoesOldPasswordExists(loginId, oldPassword))
            {
                res = new AjaxResponse()
                {
                    Message = "Old Password is not Correct",
                    Type = "error",
                    Heading = "Change Password",
                };
            }
            else
            {
                bool saved  =_loginBll.ChangePassword(loginId, newPassword);

                res = new AjaxResponse()
                {
                    Message = saved ? "Password successfully" : "Unable to change Password",
                    Type = saved ? "success" : "error",
                    Heading = "Change Password",
                    RedirectUrl = Url.Action("Logout", "Login")
                };

                return Json(res);
            }


            return Json(res);
        }

        #endregion

       

    }
}
