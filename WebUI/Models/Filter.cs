using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace HIS.Web.Models
{
    public class CheckUserRightsAttribute : ActionFilterAttribute, IActionFilter
    {
        public string PermissionCode { get; set; }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            string ConrollerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string ActionName = filterContext.ActionDescriptor.ActionName;

            string CodeToCheck = string.IsNullOrEmpty(PermissionCode) ? ActionName : PermissionCode;


            if (filterContext.HttpContext.Session == null || filterContext.HttpContext.Session["User"] == null)
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary
                {
                    { "action", "Index" },
                    { "controller", "Login" },
                    { "msg", "Please Login to continue" }
                };
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                base.OnActionExecuting(filterContext);
            }

            else if( !Helper.IsUserAuthorized(CodeToCheck) )
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("action", "UnAuthorized");
                redirectTargetDictionary.Add("controller", "Common");
                redirectTargetDictionary.Add("msg", "This account is not Authorized to use this Feature");

                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                base.OnActionExecuting(filterContext);
            }
                
        }
    }
}