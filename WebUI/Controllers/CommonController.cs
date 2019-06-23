using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HIS.Web.Models;
using HIS.Domain.Models.Menu;
using HIS.BLL.BLL;
using HIS.Domain.Models.Common;

namespace HIS.Web.Controllers
{
    public class CommonController : Controller
    {
        
         public ICommonBll CommonBll { set; get; }


        public CommonController()
        {
            CommonBll = new CommonBll();
        }

        // GET: /Common/
        #region Common

        public ActionResult PagingFooter(int pageSize, int totalRecords, string url)
        {
            Pagination page = new Pagination()
            {
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Url = url
            };
            return PartialView("_PagingFooter", page);
        }

        
        public ActionResult GetMenus()
        {
            int userId = Helper.GetLoggedInUserId(), organizationId = Helper.GetLoggedInUserOrganization();
            List<Menu> menus = new MenuBll().GetUserMenus(userId, organizationId);

            return View("Sidebar", menus);
        }

        public ActionResult UnAuthorized(string msg)
        {
            ViewBag.Message = msg;

            return View();
        }

        #endregion

        #region Country 

        public ActionResult ListCountry()
        {
            //ViewBag.Country = CommonBll.Countries.AsEnumerable().Select(a => new SelectListItem()
            //{
            //    Text = a.CountryName,
            //    Value = a.CountryId.ToString()
            //}).ToList();
            List<Country> country = new List<Country>();
            country = CommonBll.GetCountries();
           
            return View(country);
        }


        public ActionResult SaveCountry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveCountry(Country country)
        {
            CommonBll.SaveCountry(country);
            return RedirectToAction("ListCountry");
        }
        #endregion

    }
}
