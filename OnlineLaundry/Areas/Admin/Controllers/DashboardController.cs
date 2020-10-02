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
            return View();
        }

        [Authorize(Roles = "MANAGER")]
        public string All()
        {
            return "all";
        }
    }
}