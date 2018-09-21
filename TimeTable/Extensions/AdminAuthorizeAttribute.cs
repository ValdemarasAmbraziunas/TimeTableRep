using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;

namespace TimeTable.Extensions
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string redirectUrl = WebConfigurationManager.AppSettings["UnAuthorizedRedirectUrl"];
                filterContext.Result = new RedirectResult(redirectUrl);
            }
        }
    }
}