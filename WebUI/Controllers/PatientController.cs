using HIS.BLL.BLL;
using HIS.Domain.Models;
using HIS.Domain.Models.Patient;
using HIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HIS.Web.Controllers
{
    public class PatientController : Controller
    {
        public ICommonBll CommonBll { set; get; }
        public IPatientBll _patient { get; set; }

        public PatientController()
        {
            CommonBll = new CommonBll();
            _patient = new PatientBLL();
        }
        

        [CheckUserRights]
        public ActionResult PatientForm(int id = 0)
        {

            Patient pat = new Patient();
            if (id > 0)
            {
                // get saved record.. 
            }
            
            ViewBag.Gender = new SelectList(CommonBll.Genders, "GenderId", "GenderName",pat.GenderId);
            ViewBag.Titles = CommonBll.Titles;

            ViewBag.Country = new SelectList(CommonBll.Countries, "CountryId", "CountryName", pat.CountryId);

            ViewBag.MaritalStatus = new SelectList(CommonBll.MaritalStatus, "Id","Value", pat.MaritalStatus);

            ViewBag.Religion = new SelectList(CommonBll.Religion, "Id", "Value", pat.ReligionId);

            ViewBag.Languages = new SelectList(CommonBll.GetLanguages(), "LanguageId", "LanguageName", pat.PreferredLanguage);

            ViewBag.Ethnicities = new SelectList(CommonBll.Ethnicities, "Id", "Value", pat.EthnicGroup);

            return View(pat);
        }

        [CheckUserRights]
        [HttpPost]
        public ActionResult PatientForm(Patient patient)
        {
            patient.CreatedUserId = Helper.GetLoggedInUserId();
            _patient.SavePatient(patient);

            AjaxResponse res = new AjaxResponse() { 
             Heading = "Success",
              Message = "Data has been received",
             Type = "success"
            };



            return Json(res, JsonRequestBehavior.DenyGet);
        }
        
        [CheckUserRights]
        public ActionResult PatientList(int offset = 0, int pageSize = 10)
        {
            ViewBag.TotalRecords = 10;
            ViewBag.Offset = offset;
            ViewBag.PageSize = pageSize;

            return View();
        }
    }
}
