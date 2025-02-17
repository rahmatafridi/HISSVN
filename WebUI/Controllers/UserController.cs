﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HIS.Domain.Models.User;
using HIS.Web.Models;
using HIS.Domain.Models.Common;
using HIS.BLL;
using HIS.BLL.BLL;
using HIS.Domain.Models.Role;
using System.Threading.Tasks;
using HIS.Web.Filters;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace HIS.Web.Controllers
{
    public class UserController : Controller
    {
        #region Initialization

        public IUserBll _user { get; set; }
        public ICommonBll _common { get; set; }
        public IRoleBLL _role { get; set; }


        public UserController()
        {
            _user = new UserBll();
            _common = new CommonBll();
            _role = new RoleBLL();
        }

        #endregion


        #region User Type
        [CheckUserRights]
        public ActionResult UserTypeList(string searchText, int offset = 0, int pageSize = 10)
        {
            List<UserType> types = new List<UserType>();

            SearchCriteria criteria = new SearchCriteria()
            {
                Offset = offset,
                SearchText = searchText ?? "",
                PageSize = pageSize
            };

            int TotalRecords = 0;

            types = _user.GetUserTypes(criteria, out TotalRecords);

            ViewBag.Offset = offset;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = TotalRecords;

            return View(types);
        }

        [CheckUserRights]
        [HttpGet]
        public ActionResult UserTypeForm(int id = 0)
        {
            UserType ut = new UserType();

            if (id > 0)
            {
                ut = _user.GetUserTypeById(id);
                // get previous record for editing
            }

            return View(ut);
        }
        [HttpPost]
        [ValidateAjax]
        [CheckUserRights]
        public ActionResult UserTypeForm(UserType uType)
        {
            // TODO : assign user id from session to created user id..
            bool saved = _user.SaveUserType(uType);

            AjaxResponse response = new AjaxResponse()
            {
                Type = saved ? "success" : "error",
                Message = saved ? "Data has been processed successfully" : "Unable to process data",
                Heading = "User Type",
                RedirectUrl = Url.Action("UserTypeList", "User")


            };

            return Json(response);
        }

        [HttpPost]
        public ActionResult DeleteUserTypeById(int id)
        {
            string message = "";
            string mclass = "";
            int deleted = _user.DeleteUserTypeById(id);
            if (deleted > 0)
            {
                message = "Operation completed successfully";
                mclass = "info";
            }
            else
            {
                message = "Unable to delete User Type";
                mclass = "error";
            }

            AjaxResponse res = new AjaxResponse()
            {
                Message = message,
                Type = mclass,
                Heading = "User Type",
                RedirectUrl = Url.Action("UserTypeList", "User")
            };


            return Json(res);
        }
        #endregion


        #region User
        [CheckUserRights]
        public ActionResult UserList(string searchText, int offset = 0, int pageSize = 10)
        {
            List<User> types = new List<User>();

            SearchCriteria criteria = new SearchCriteria()
            {
                Offset = offset,
                SearchText = searchText ?? "",
                PageSize = pageSize
            };

            int TotalRecords = 0;

            types = _user.GetUsers(criteria, out TotalRecords, Helper.GetLoggedInUserOrganization());

            ViewBag.Offset = offset;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = TotalRecords;

            return View(types);
        }
        [CheckUserRights]
        public ActionResult UserForm(int id = 0)
        {
            User user = new Domain.Models.User.User();

            ViewBag.Titles = _common.Titles.AsEnumerable().Select(a => new SelectListItem()
            {
                Text = a.TitleName,
                Value = a.TitleId.ToString()
            }).ToList();

            ViewBag.Genders = _common.Genders.AsEnumerable().Select(a => new SelectListItem()
            {
                Text = a.GenderName,
                Value = a.GenderId.ToString()
            }).ToList();

            //ViewBag.Countries = _common.Countries.AsEnumerable().Select(a => new SelectListItem()
            //{
            //    Text = a.CountryName,
            //    Value = a.CountryId.ToString()
            //}).ToList();

            int total = 0;
            ViewBag.Roles = _role.GetRoles(new SearchCriteria() { Offset = 0, PageSize = 500, SearchText = "" }, out total)
                .Where(a => a.iOrganizationId == 0 || a.iOrganizationId == Helper.GetLoggedInUserOrganization()).ToList();

            ViewBag.AssignedRoles = _role.GetUserAssignedRoleIds(id);

            ViewBag.Types = _user.GetUserTypes(new SearchCriteria() { Offset = 0, PageSize = 500, SearchText = "" }, out total).ToList();

            ViewBag.AssignedTypes = _user.GetUserAssignedTypeIds(id);


            if (id > 0)
            {
                user = _user.GetUserById(id);
            }

            return View(user);
        }
        [CheckUserRights]
        [ValidateAjax]
        [HttpPost]
        public ActionResult UserForm(User user, List<int> iRoleId, List<int> UserTypeId)
        {
            if (!ModelState.IsValid)
            {
                return new AjaxableViewResult(user);
            }
            // sir why do we need country and city for user.. ;(
            user.CityId = 1;
            user.CountryId = 1;
            user.OrganizationId = Helper.GetLoggedInUserOrganization();
            user.FirstTimeLogin = user.UserId == 0 ? true : false;

            AjaxResponse res = new AjaxResponse();


            if (_user.DoesUsernameOrEmailExists(user.UserName, user.Email, user.UserId))
            {
                res = new AjaxResponse()
                {
                    Message = "Username or Email already in Use",
                    Type = "warning",
                    Heading = "User",
                };
            }
            else
            {
                bool saved = _user.SaveUserData(user, iRoleId, UserTypeId);

                res = new AjaxResponse()
                {
                    Message = saved ? "User Save successfully" : "Unable to save user",
                    Type = saved ? "success" : "error",
                    Heading = "Clinic",
                    RedirectUrl = Url.Action("UserList", "User")
                };
            }


            return Json(res);
        }


        [HttpPost]
        public ActionResult DeleteUserById(int id)
        {
            string message = "";
            string mclass = "";
            bool deleted = _user.DeleteUserById(id);
            if (deleted)
            {
                message = "Operation completed successfully";
                mclass = "info";
            }
            else
            {
                message = "Unable to delete User";
                mclass = "error";
            }

            AjaxResponse res = new AjaxResponse()
            {
                Message = message,
                Type = mclass,
                Heading = "User",
                RedirectUrl = Url.Action("UserList", "User")
            };


            return Json(res);
        }
        #endregion


        #region USerprofile

        public ActionResult UserProfile()
        {
            User user = new User();
            user = _user.GetDataForUserProfile(Helper.GetLoggedInUserId());
            
            return View(user);
        }

        [HttpPost]
        public ActionResult UserProfile(User user)
        {
            user.vUserImage = TempData["UserImage"].ToString();
            var imagepath = ViewBag.Userimage;
            user.vUserImage = imagepath;
            user.UserId = Helper.GetLoggedInUserId();
            _user.UpdateUserProfile(user);
            AjaxResponse res = new AjaxResponse();
            return View(user);
        }

        public JsonResult SaveImageFile(byte[] file)
        {
            var filedata= Request.Files[0];
            string userid=Helper.GetLoggedInUserId().ToString()+"/";
            string relatedPath=null;
            if(filedata !=null && filedata.ContentLength>0)
            {
                string directoryPath= Path.Combine(Server.MapPath("~/Upload/"),userid);
                string filepath=Path.Combine(Server.MapPath("~/Upload/"),userid,"UserImage.jpeg");
                if(!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                if(System.IO.File.Exists(filepath))
                {
                    System.IO.File.Exists(filepath);
                   
                }

                Image img=Image.FromStream(filedata.InputStream,true,true);
                img.Save(filepath,ImageFormat.Jpeg);
                ViewBag.MimeType="image/jpeg";
                TempData["UserImage"]=filepath;
            }

            return Json (new{
            Success = true,
            Title="Success",
            FileName=relatedPath
          
            },JsonRequestBehavior.AllowGet );

        }

        #endregion

    }
}
