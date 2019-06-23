using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HIS.BLL.BLL;
using HIS.Domain.Models;
using HIS.Domain.Models.Menu;
using HIS.Web.Models;
using HIS.Domain.Models.Common;


namespace HIS.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Intialization

        public ICommonBll CommonBll { set; get; }


        public HomeController()
        {
            CommonBll = new CommonBll();
        }
        #endregion

        #region Home

        public ActionResult Index()
        {
            
            return View();
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

        public ActionResult GetCity(int Country_id)
        {
            List<City> citylist = CommonBll.Cities.Where(a => a.CountryId == Country_id).ToList();
            ViewBag.city = new SelectList(citylist, "CityId", "CityName");
            return PartialView("CityPartial");
        }
        public ActionResult ListCountry()
        {
            ViewBag.Country = CommonBll.Countries.AsEnumerable().Select(a => new SelectListItem()
            {
                Text = a.CountryName,
                Value = a.CountryId.ToString()
            }).ToList();


            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #endregion


    }
}
