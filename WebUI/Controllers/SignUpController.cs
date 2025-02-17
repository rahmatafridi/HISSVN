﻿using HIS.BLL;
using HIS.Domain.Models;
using HIS.Domain.Models.Common;
using HIS.Domain.Models.Email;
using HIS.Domain.Models.Module;
using HIS.Domain.Models.Organization;
using HIS.Domain.Models.Template;
using HIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIS.Web.Controllers
{
    public class SignUpController : Controller
    {
        #region Intialization
        public IUserBll _user { get; set; }
        public IModuleBll _moduleBll { get; set; }
        public IOrganizationBLL _organizationBll { get; set; }
        public IEmailBll _emailBll { get; set; }
        public ITemplateBLL _templateBll { get; set; }
        public SignUpController()
        {
            _user = new UserBll();
            _moduleBll = new ModuleBLL();
            _organizationBll = new OrganizationBLL();
            _emailBll = new EmailBLL();
            _templateBll = new TemplateBLL();
        }
        #endregion
        //
        // GET: /SignUp/

        public ActionResult Index()
        {
            return View();
        }

        #region Sign Up
        public ActionResult SignUp(int? type)
        {
            if (type == 1)
            {

                int total = 0;
                ViewBag.Modules = _moduleBll.GetModule(new SearchCriteria() { Offset = 0, PageSize = 500, SearchText = "" }, out total)
              .Where(a => a.IsCore == 0).ToList();
                return View("SignUpClinic");
            }
            else if (type == 2)
            {
                return View("SignUpConsultant");
            }
            else if (type == 3)
            {
                return View("SignUpPatient");
            }
            return View();
        }
        #endregion

        #region SignUp Clinic
        [HttpPost]
        public ActionResult SignUpClinic(SignUpClinic signUp, List<int> ModuleIds)
        {

            Organization organization = new Organization();
            organization.vEmail=signUp.ClinicEmailAddress;
            organization.vOrganizationName = signUp.ClinicEmailAddress;
            organization.bIsActive = false;
            organization.dRegistrationDate = System.DateTime.Now;
            organization.iStatus = 0;
            organization.FirstTimeLogin = true;
            AjaxResponse res = new AjaxResponse();


            if (_organizationBll.DoesOrganizationExists(signUp.ClinicEmailAddress))
            {
                res = new AjaxResponse()
                {
                    Message = "Email already in Use",
                    Type = "warning",
                    Heading = "Clinic",
                };
            }
            else
            {

                bool saved = _organizationBll.SaveOrganizationModule(organization, ModuleIds);
                if(saved)
                { 
                    int total = 0;
                    var templateData= _templateBll.GetTemplate(new SearchCriteria() { Offset = 0, PageSize = 500, SearchText = "" }, out total).Where(a => a.TemplateName == "EmailRegistration").ToList();
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary.Add("##CustomerName##", organization.vEmail);
                   
                    EmailData emailData = new EmailData();
                    emailData.ToAddress = organization.vEmail;
                    emailData.IsSent = false;
                    emailData.Subject = "Registration";
                    emailData.ScheduleDateTime = System.DateTime.Now.ToShortDateString();
                    emailData.OrganizationId = Helper.GetLoggedInUserOrganization();
                    emailData.Priority = 1;
                    emailData.CreatedUserId = Helper.GetLoggedInUserId();
                    SmsData smsData = new SmsData();
                    smsData.ToAddress = "03224341025";
                    smsData.IsSent = false;
                    smsData.Subject = "Registration";
                    smsData.ScheduleDateTime = System.DateTime.Now.ToShortDateString();
                    smsData.OrganizationId = Helper.GetLoggedInUserOrganization();
                    smsData.Priority = 1;
                    smsData.CreatedUserId = Helper.GetLoggedInUserId();
                    foreach (var key in dictionary.Keys)
                    {
                        emailData.EmailBody = templateData[0].TemplateBody.Replace(key, dictionary[key]);
                        smsData.SmsBody = templateData[0].TemplateBody.Replace(key, dictionary[key]);

                    }

                    bool sent = _emailBll.SentEmail(emailData);
                    bool SmsSent = _emailBll.SentSms(smsData);
                }

                res = new AjaxResponse()
                {
                    Message = saved ? "Clinic Save successfully" : "Unable to save clinic",
                    Type = saved ? "success" : "error",
                    Heading = "Clinic",
                    RedirectUrl = Url.Action("SuccessMessage", "SignUp")
                };
            }


            return Json(res);

            //saved = _organizationBll.SaveOrganizationModule(organizationId, modules);

            //AjaxResponse res = new AjaxResponse()
            //{
            //    Message = saved ? "Modules assigned successfully" : "Unable to assign modules",
            //    Type = saved ? "success" : "error",
            //    Heading = "Modules"
            //};

            //return Json(res);
        }

        #endregion

        #region Messages

        public ActionResult SuccessMessage()
        {
            return View();
        }

        #endregion

    }
}
