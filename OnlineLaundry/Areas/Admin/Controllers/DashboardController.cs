using OnlineLaundry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLaundry.Areas.Admin.Controllers {

    [Authorize]
    public class DashboardController : Controller {
        // GET: Admin/Home
        
        public ActionResult Index() {
            ViewBag.Username = User.Identity.Name;
            return View();
        }

        [Authorize(Roles = "MANAGER")]
        public string All()
        {
            return User.IsInRole("MANAGER").ToString();
        }
    }
}