using SHIVAM_ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.AspNet.Identity;
namespace SHIVAM_ECommerce.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string cookieName = FormsAuthentication.FormsCookieName;

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl);
                return;
            }

            var user = filterContext.HttpContext.User as ClaimsPrincipal;
            var _commonmethods = new CommonMethods();
            var _TokenData = _commonmethods.GetCurrentUser();
            _TokenData.IsSuperAdmin = true;

            if (!user.IsInRole("superadmin"))
            {
                _TokenData.IsSuperAdmin = false;

                if (_TokenData != null)
                {
                    if (_TokenData.PlanEndDate.Date > DateTime.Now.Date)
                    {

                        HttpContext.Current.Session["CurrentUserContext"] = _TokenData;
                        base.OnAuthorization(filterContext);
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "area", "" }, { "controller", "Home" }, { "action", "PlanExpire" } });
                    }
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "area", "" }, { "controller", "Home" }, { "action", "Error" } });
                }
            }
            else
            {
                HttpContext.Current.Session["CurrentUserContext"] = _TokenData;
                base.OnAuthorization(filterContext);
            }



        }
    }
}