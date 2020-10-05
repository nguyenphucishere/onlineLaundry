using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace OnlineLaundry.Filter {
    public class BasicAuthFilter : ActionFilterAttribute, IAuthenticationFilter {
        public string Controller { get; set; }
        public string Action { get; set; }
        public BasicAuthFilter(string controller, string action) {
            Controller = controller;
            Action = action;
        }
        public void OnAuthentication(AuthenticationContext filterContext) {
            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated) {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult) {
                filterContext.Result = new RedirectToRouteResult("Default",
                    new System.Web.Routing.RouteValueDictionary {
                        {"controller", Controller },
                        {"action", Action },
                        {"returnUrl", filterContext.HttpContext.Request.RawUrl }
                    });
            }
        }
    }
}