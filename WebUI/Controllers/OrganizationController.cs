using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HIS.Domain.Models.User;
using HIS.Web.Models;
using HIS.Domain.Models.Common;
using HIS.BLL;
using HIS.Domain.Models.Organization;
using HIS.BLL.BLL;
using HIS.Domain.Models.Module;
using HIS.Web.Filters;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace HIS.Web.Controllers
{
    public class OrganizationController : Controller
    {
        #region Initialization

        public IOrganizationBLL _organizationBll { get; set; }
        public ICommonBll _commonBll { get; set; }
        public IModuleBll _moduleBll { get; set; }

        public OrganizationController()
        {
            _organizationBll = new OrganizationBLL();
            _commonBll = new CommonBll();
            _moduleBll = new ModuleBLL();
        }

        #endregion

        #region Organization

        public ActionResult Index()
        {
            return View();
        }

        [CheckUserRights]
        public ActionResult OrganizationList(string searchText, int offset = 0, int pageSize = 10)
        {
            List<Organization> organizations = new List<Organization>();

            SearchCriteria criteria = new SearchCriteria()
            {
                Offset = offset,
                SearchText = searchText ?? "",
                PageSize = pageSize
            };

            int TotalRecords = 0;

            organizations = _organizationBll.GetOrganizations(criteria, out TotalRecords).Where(a=>a.iStatus==1).ToList();

            ViewBag.Offset = offset;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = TotalRecords;

            return View(organizations);
        }

        [CheckUserRights]
        public ActionResult OrganizationForm(int id = 0)
        {
            Organization organization = new Organization();

            #region Dropdown data Intialization

            ViewBag.Cities = _commonBll.Cities.AsEnumerable().Select(a => new SelectListItem()
            {
                Text = a.CityName,
                Value = a.CityId.ToString()
            }).ToList();
            ViewBag.Countries = _commonBll.Countries.AsEnumerable().Select(a => new SelectListItem()
            {
                Text = a.CountryName,
                Value = a.CountryId.ToString()
            }).ToList();

            int total = 0;
            ViewBag.Modules = _moduleBll.GetModule(new SearchCriteria() { Offset = 0, PageSize = 500, SearchText = "" }, out total)
                .Where(a=>a.IsCore==0).ToList();

            ViewBag.AssignedModules = _organizationBll.GetOrganizationModulesAssignedIds(id);

            ViewBag.TimeZones = _commonBll.GetTimeZone().AsEnumerable().Select(a => new SelectListItem()
            {
                Text ="(UTC "+a.WorldTimeZoneOffSet+") "+ a.WorldTimeZoneName,
                Value = a.WorldTimeZoneId.ToString()
            }).ToList();
            ViewBag.Languages = _commonBll.GetLanguages().AsEnumerable().Select(a => new SelectListItem()
            {
                Text = a.LanguageName,
                Value = a.LanguageId.ToString()
            }).ToList();

            ViewBag.Status = _organizationBll.GetOrganizationStatus().AsEnumerable().Select(a => new SelectListItem()
            {
                Text = a.StatusText,
                Value = a.StatusId.ToString()
            }).ToList();
            #endregion
            if (id > 0)
            {
                organization = _organizationBll.GetOrganizationById(id);
                // get saved data
            }

            return View(organization);
        }

        [HttpPost]
        [CheckUserRights]
        [ValidateAjax]
        public ActionResult OrganizationForm(Organization organization, List<int> ModuleIds)
        {
            if (!ModelState.IsValid)
            {
                #region Dropdown data Intialization
                ViewBag.Cities = _commonBll.Cities.AsEnumerable().Select(a => new SelectListItem()
                {
                    Text = a.CityName,
                    Value = a.CityId.ToString()
                }).ToList();
                ViewBag.Countries = _commonBll.Countries.AsEnumerable().Select(a => new SelectListItem()
                {
                    Text = a.CountryName,
                    Value = a.CountryId.ToString()
                }).ToList();

                int total = 0;
                ViewBag.Modules = _moduleBll.GetModule(new SearchCriteria() { Offset = 0, PageSize = 500, SearchText = "" }, out total)
                    .Where(a => a.OrganizationId == 0 || a.OrganizationId == Helper.GetLoggedInUserOrganization()).ToList();
                ViewBag.AssignedModules = _organizationBll.GetOrganizationModulesAssignedIds(0);
                ViewBag.TimeZones = _commonBll.GetTimeZone().AsEnumerable().Select(a => new SelectListItem()
                {
                    Text = "(UTC " + a.WorldTimeZoneOffSet + ") " + a.WorldTimeZoneName,
                    Value = a.WorldTimeZoneId.ToString()
                }).ToList();
                ViewBag.Languages = _commonBll.GetLanguages().AsEnumerable().Select(a => new SelectListItem()
                {
                    Text = a.LanguageName,
                    Value = a.LanguageId.ToString()
                }).ToList();

                ViewBag.Status = _organizationBll.GetOrganizationStatus().AsEnumerable().Select(a => new SelectListItem()
                {
                    Text = a.StatusText,
                    Value = a.StatusId.ToString()
                }).ToList();
                #endregion
                return new AjaxableViewResult(organization);
            }
            // sir why do we need country and city for user.. ;(
            organization.CreatedUserId = Helper.GetLoggedInUserId();
            organization.FirstTimeLogin = false;
            AjaxResponse res = new AjaxResponse();


            if (_organizationBll.DoesOrganizationExists(organization.vEmail,organization.iOrganizationId))
            {
                res = new AjaxResponse()
                {
                    Message = "Email already in Use",
                    Type = "warning",
                    Heading = "User",
                };
            }
            else
            {
                bool saved = _organizationBll.SaveOrganizationModule(organization,ModuleIds);

                 res = new AjaxResponse()
                {
                    Message = saved ? "Clinic Save successfully" : "Unable to save clinic",
                    Type = saved ? "success" : "error",
                    Heading = "Clinic"   ,
                    RedirectUrl= Url.Action("OrganizationList","Organization")
                };

                return Json(res);
            }


            return Json(res);
        }

    
        [HttpPost]
        public ActionResult DeleteOrganizationById(int id)
        {
            string message = "";
            string mclass = "";
            int deleted = _organizationBll.DeleteOrganizationById(id);
            if (deleted > 0)
            {
                message = "Operation completed successfully";
                mclass = "info";
            }
            else
            {
                message = "Unable to delete Organization";
                mclass = "error";
            }

            AjaxResponse res = new AjaxResponse()
            {
                Message = message,
                Type = mclass,
                Heading = "Organization"
            };


            return Json(res);
        }


        #endregion

        #region Pending Clinics

        [CheckUserRights]
        public ActionResult PendingOrganizationList(string searchText, int offset = 0, int pageSize = 10)
        {
            List<Organization> organizations = new List<Organization>();

            SearchCriteria criteria = new SearchCriteria()
            {
                Offset = offset,
                SearchText = searchText ?? "",
                PageSize = pageSize
            };

            int TotalRecords = 0;

            organizations = _organizationBll.GetOrganizations(criteria, out TotalRecords).Where(a=> a.iStatus==0).ToList();

            ViewBag.Offset = offset;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = organizations.Count();

            return View(organizations);
        }

        [HttpPost]
        [ValidateAjax]
        public ActionResult DeclineOrganizationForm(int organizationId, string reasonData)
        {
            if (!ModelState.IsValid)
            {                    
                return new AjaxableViewResult(reasonData);
            }
            int declinedUserId = Helper.GetLoggedInUserId();
            AjaxResponse res = new AjaxResponse(); 
            
         bool saved = _organizationBll.DeclineOrganization(organizationId,reasonData,declinedUserId);

                res = new AjaxResponse()
                {
                    Message = saved ? "Clinic Decline successfully" : "Unable to decline clinic",
                    Type = saved ? "success" : "error",
                    Heading = "Clinic",
                    RedirectUrl = Url.Action("PendingOrganizationList", "Organization")
                };

                return Json(res);   
        }

        #endregion

        #region Rejected Clinics

        [CheckUserRights]
        public ActionResult RejectedOrganizationList(string searchText, int offset = 0, int pageSize = 10)
        {
            List<Organization> organizations = new List<Organization>();

            SearchCriteria criteria = new SearchCriteria()
            {
                Offset = offset,
                SearchText = searchText ?? "",
                PageSize = pageSize
            };

            int TotalRecords = 0;

            organizations = _organizationBll.GetOrganizations(criteria, out TotalRecords).Where(a => a.iStatus == 2).ToList();

            ViewBag.Offset = offset;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = organizations.Count();

            return View(organizations);
        }

        #endregion

        #region Approve Clinics

        [HttpPost]
        [ValidateAjax]
        public ActionResult ApproveOrganizationForm(int organizationId, string activationDate,string expiryDate,string remarks)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxableViewResult(activationDate);
            }
            int approveUserId = Helper.GetLoggedInUserId();
            AjaxResponse res = new AjaxResponse();

            bool saved = _organizationBll.ApproveOrganization(organizationId, activationDate, expiryDate,remarks, approveUserId);

            res = new AjaxResponse()
            {
                Message = saved ? "Clinic Approve successfully" : "Unable to approve clinic",
                Type = saved ? "success" : "error",
                Heading = "Clinic",
                RedirectUrl = Url.Action("PendingOrganizationList", "Organization")
            };

            return Json(res);
        }

        #endregion

        #region Setup Clinic

        public ActionResult SetupClinic()
        {
            OrganizationViewModel organizationViewModel = new OrganizationViewModel();
            organizationViewModel = _organizationBll.GetDataForNewSetUpClinic(Helper.GetLoggedInUserOrganization(), Helper.GetLoggedInUserId());
            ViewBag.Cities = _commonBll.Cities.AsEnumerable().Select(a => new SelectListItem()
            {
                Text = a.CityName,
                Value = a.CityId.ToString()
            }).ToList();
            ViewBag.Countries = _commonBll.Countries.AsEnumerable().Select(a => new SelectListItem()
            {
                Text = a.CountryName,
                Value = a.CountryId.ToString()
            }).ToList();
            ViewBag.TimeZones = _commonBll.GetTimeZone().AsEnumerable().Select(a => new SelectListItem()
            {
                Text = "(UTC " + a.WorldTimeZoneOffSet + ") " + a.WorldTimeZoneName,
                Value = a.WorldTimeZoneId.ToString()
            }).ToList();
            ViewBag.Languages = _commonBll.GetLanguages().AsEnumerable().Select(a => new SelectListItem()
            {
                Text = a.LanguageName,
                Value = a.LanguageId.ToString()
            }).ToList();

            ViewBag.Titles = _commonBll.Titles.AsEnumerable().Select(a => new SelectListItem()
            {
                Text = a.TitleName,
                Value = a.TitleId.ToString()
            }).ToList();

            ViewBag.Genders = _commonBll.Genders.AsEnumerable().Select(a => new SelectListItem()
            {
                Text = a.GenderName,
                Value = a.GenderId.ToString()
            }).ToList();
            return View(organizationViewModel);
        }
        [HttpPost]
       // [ValidateAjax]
        public ActionResult SetupClinic(OrganizationViewModel model)
        {
            model.Organization.OrganizationLogo= TempData["OrganizationLogo"].ToString();
            AjaxResponse res = new AjaxResponse();
            var logoPath = ViewBag.OrganizationLogo;
            model.Organization.OrganizationLogo = logoPath;
            model.User.CountryId = model.Organization.iCountryId.Value;
            model.User.CityId = model.Organization.iCityId.Value;
            model.Organization.iOrganizationId = Helper.GetLoggedInUserOrganization();
            model.User.UserId = Helper.GetLoggedInUserId();
            bool saved = _organizationBll.UpdateClinicInformationForFirstTime(model);
            res = new AjaxResponse()
            {
                Message = saved ? "Clinic Updated successfully" : "Unable to update clinic",
                Type = saved ? "success" : "error",
                Heading = "Clinic",
                RedirectUrl = Url.Action("Index", "Home")
            };

            return Json(res);
        }

       
       // //////////////////////
       
     
      
        public JsonResult SaveImageFile(byte[] file)
        {

            var filesData = Request.Files[0];
            string organizationId = Helper.GetLoggedInUserOrganization().ToString() + "/";
            string relativePath = null;
            if (filesData != null && filesData.ContentLength > 0)
            {
                string directoryPath = Path.Combine(Server.MapPath("~/Upload/"),organizationId);
                string filePath = Path.Combine(Server.MapPath("~/Upload/"), organizationId,"Logo.jpeg");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                if (System.IO.File.Exists(filePath)){
                    System.IO.File.Delete(filePath);
                }             

                Image img = Image.FromStream(filesData.InputStream, true, true);

                img.Save(filePath, ImageFormat.Jpeg);               
                ViewBag.MimeType = "image/pjpeg";
                TempData["OrganizationLogo"] = filePath;
            }



            return Json(new
            {
                Success = true,
                Title = "Success",
                FileName = relativePath
            }, JsonRequestBehavior.AllowGet);
            //return true;
        }

        #endregion

        #region Dashboard

        #region country wise organization ount

        // organization wise user count graph
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult GetCountryWiseOrganizations()
        {

            return Json(_organizationBll.CountryWiseOrganization);
        }

        #endregion

        #region organization wise user count

        // organization wise user count

        public ActionResult UsersCountOrganizationWise()
        {
            return View();
        }

        public ActionResult GetUserCountOrganizationWise()
        {

            return Json(_organizationBll.UserCountOrganizationWise);
        }

        #endregion

        #region organization module

        //organization module information

        public ActionResult ModuleInformationOrganizatiionWise()
        {
            var a = _organizationBll.ModuleInformationOrganizatiionWise;

            return View(a);
        }

        #endregion


        #endregion

        #region Locations


        [CheckUserRights]
        public ActionResult LocationList(string searchText, int offset = 0, int pageSize = 10)
        {
            List<OrganizationLocation> locations = new List<OrganizationLocation>();

            SearchCriteria criteria = new SearchCriteria()
            {
                Offset = offset,
                SearchText = searchText ?? "",
                PageSize = pageSize
            };

            int TotalRecords = 0;

            locations = _organizationBll.GetLocationsAgainstOrganization(Helper.GetLoggedInUserOrganization(), criteria, out TotalRecords);

            ViewBag.Offset = offset;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = TotalRecords;

            return View(locations);
        }

        [CheckUserRights]
        public ActionResult LocationForm(int id = 0)
        {
            OrganizationLocation Location = new OrganizationLocation();

         
            if (id > 0)
            {
                Location = _organizationBll.GetLocationsById(Helper.GetLoggedInUserOrganization(), id);                
            }

            ViewBag.LocationTypes =  new SelectList(_organizationBll.LocationTypes,"Id", "Type", Location.LocationType);

            return View(Location);
        }

        [HttpPost]
        [CheckUserRights]
        [ValidateAjax]
        public ActionResult LocationForm(OrganizationLocation location)
        {
            //if (!ModelState.IsValid)
            //{
            //    return new AjaxableViewResult(location);
            //}

            location.CreatedUserId = Helper.GetLoggedInUserId();
            location.OrganizationId = Helper.GetLoggedInUserOrganization();

            AjaxResponse res = new AjaxResponse();

            int saved = _organizationBll.InsertUpdateOrganizationLocation(location);

            res = new AjaxResponse()
            {
                Message = saved > 0 ? "Location Save successfully" : "Unable to save Location",
                Type = saved > 0 ? "success" : "error",
                Heading = "Location",
                RedirectUrl = Url.Action("LocationList", "Organization")
            };

            return Json(res);
         
        }

        #endregion
    }
}
