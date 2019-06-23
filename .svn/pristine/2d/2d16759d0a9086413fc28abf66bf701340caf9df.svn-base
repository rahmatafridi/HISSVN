using HIS.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIS.Web.Models
{
    public class Helper
    {
        public static int GetLoggedInUserOrganization()
        {
            int orgid = -1;

            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["User"] != null)
                {
                    orgid = (HttpContext.Current.Session["User"] as User).OrganizationId;
                }
            }

            return orgid;
        }

        public static int GetLoggedInUserId()
        {
            int orgid = 0;

            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["User"] != null)
                {
                    orgid = (HttpContext.Current.Session["User"] as User).UserId;
                }
            }

            return orgid;
        }

        public static bool IsUserAuthorized(string permissionCode)
        {
            bool allowed = false;

            

            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["UserRights"] != null)
                {
                    List<string> rights = HttpContext.Current.Session["UserRights"] as List<string>;
                    if (rights.Contains(permissionCode))
                    {
                        allowed = true;
                    }
                }
            }

            return allowed;
        }

        public static User GetUserObjectFromSession()
        {
            User user = new User();

            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["User"] != null)
                {
                    user = HttpContext.Current.Session["User"] as User;
                }
            }

            return user;
        }

        public static bool IsUserLoggedIn()
        {
            return ! (HttpContext.Current.Session == null || HttpContext.Current.Session["User"] == null);
            
        }


    }
}