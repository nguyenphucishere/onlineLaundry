using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLaundry.Areas.Admin.Controllers
{
    public class LoginRequiredController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session[KeyStorage.LOGIN_SESSION_KEY] == null)
            {
                filterContext.Result = Redirect("/Admin/Account/Login");
            }
            base.OnActionExecuting(filterContext);
        }

    }
}