using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;

namespace OnlineLaundry.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private OnlineLaundryEntities onlineLaundryEntities = new OnlineLaundryEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost()]
        public ActionResult Login(string username, string password)
        {
            int staffID = isAuthenticate(username, password);
            if (staffID <= 0)
            {
                ModelState.AddModelError("", "Incorrect email or password, please try again");
                return View("Login");
            }

            Session[KeyStorage.LOGIN_SESSION_KEY] = staffID;
            return Redirect("/Admin");
        }
        private int isAuthenticate(string username, string password)
        {
            staff findStaff = onlineLaundryEntities.staffs.Where(staffInfo => staffInfo.username == username && staffInfo.password == password).FirstOrDefault();

            return findStaff == null ? 0 : findStaff.staff_id;
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return Redirect("/");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session[KeyStorage.LOGIN_SESSION_KEY] != null)
            {
                filterContext.Result = Redirect("/Admin");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}