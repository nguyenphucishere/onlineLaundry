using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VoKhoiNamHai_Web_SaleDb.Areas.Admin.Controllers {
    public class DashboardController : Controller {
        // GET: Admin/Home

        public ActionResult Index() {
            return View();
        }
    }
}