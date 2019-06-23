using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HIS.Domain.Models.User;
using HIS.Web.Models;
using HIS.Domain.Models.Common;
using HIS.BLL;
using HIS.Domain.Models.Module;

namespace HIS.Web.Controllers
{
    public class SettingsController : Controller
    {
        #region Initialization

        public IModuleBll _module { get; set; }

        public SettingsController()
        {
            _module = new ModuleBLL();
        }

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Language()
        {
            return View();
        }

        public ActionResult SettingsMenu()
        {

            return PartialView("_SettingsMenu");
        }

        //#region Country

        //[CheckUserRights]
        //public ActionResult CountryList(string searchText, int offset = 0, int pageSize = 10)
        //{
        //    List<Country> types = new List<Country>();

        //    SearchCriteria criteria = new SearchCriteria()
        //    {
        //        Offset = offset,
        //        SearchText = searchText ?? "",
        //        PageSize = pageSize
        //    };

        //    int TotalRecords = 0;

        //    types = _module.GetModule(criteria, out TotalRecords);

        //    ViewBag.Offset = offset;
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.TotalRecords = TotalRecords;

        //    return View(types);
        //}
        //[CheckUserRights]
        //[HttpGet]
        //public ActionResult ModuleForm(int id = 0)
        //{
        //    Module module = new Module();

        //    if (id > 0)
        //    {
        //        module = _module.GetModuleById(id);
        //        // get previous record for editing
        //    }

        //    return View(module);
        //}
        //[CheckUserRights]
        //[HttpPost]
        //public ActionResult ModuleForm(Module module)
        //{
        //    // TODO : assign user id from session to created user id..
        //    module.CreatedUserId = Helper.GetLoggedInUserId();
        //    bool saved = _module.SaveModule(module);

        //    AjaxResponse response = new AjaxResponse()
        //    {
        //        Type = saved ? "success" : "error",
        //        Message = saved ? "Data has been processed successfully" : "Unable to process data",
        //        Heading = "Module"

        //    };

        //    return Json(response);
        //}

        //[HttpPost]
        //public ActionResult DeleteModuleById(int id)
        //{
        //    string message = "";
        //    string mclass = "";
        //    int deleted = _module.DeleteModuleById(id);
        //    if (deleted > 0)
        //    {
        //        message = "Operation completed successfully";
        //        mclass = "info";
        //    }
        //    else
        //    {
        //        message = "Unable to delete Module";
        //        mclass = "error";
        //    }

        //    AjaxResponse res = new AjaxResponse()
        //    {
        //        Message = message,
        //        Type = mclass,
        //        Heading = "Module"
        //    };


        //    return Json(res);
        //}

        //#endregion


    }
}
