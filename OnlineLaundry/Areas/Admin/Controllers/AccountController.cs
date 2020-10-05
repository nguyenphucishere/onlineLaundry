using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Protocols;
using OnlineLaundry.Models;

namespace OnlineLaundry.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private OnlineLaundryEntities onlineLaundryEntities = new OnlineLaundryEntities();

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin");
            }
            return View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(string username, string password)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin");
            }
            if(!isAuthenticate(username, password))
            {
                ModelState.AddModelError("", "Incorrect email or password, please try again");
                return View("Login");
            }

            FormsAuthentication.SetAuthCookie(username, false);
            return Redirect("/Admin");
        }
        private bool isAuthenticate(string username, string password)
        {
            staff findStaff = onlineLaundryEntities.staffs.Where(staffInfo => staffInfo.username == username && staffInfo.password == password).FirstOrDefault();

            return findStaff != null;
        }

        public ActionResult LogOut()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin/Login");
            }
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}