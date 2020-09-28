using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLaundry.Areas.Admin.Controllers {
    public class DashboardController : LoginRequiredController {
        // GET: Admin/Home

        public ActionResult Index() {
            return View();
        }
    }
}