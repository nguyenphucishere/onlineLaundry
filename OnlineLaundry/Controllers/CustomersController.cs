using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Security;
using OnlineLaundry.Models;
using WebGrease;
using OnlineLaundry.Utils;
using OnlineLaundry.Models.ViewModel;
using OnlineLaundry.Filter;
using Newtonsoft.Json;
using System.Diagnostics.Eventing.Reader;

namespace OnlineLaundry.Controllers {
    public class CustomersController : Controller {

        private OnlineLaundryEntities db = new OnlineLaundryEntities();

        /********** Signin **********/
        public ActionResult Signin() {
            ViewBag.url = HttpContext.Request.UrlReferrer.ToString();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signin(CustomerViewModel cvm, string url) {
            if (!ModelState.IsValid) {
                return View();
            }
            customer loggedCustomer = db.customers
                                        .SingleOrDefault(e => e.username == cvm.Customer.username);
            if (loggedCustomer == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if ((bool)!loggedCustomer.is_verified || loggedCustomer.is_verified == null)
                return RedirectToAction("ErrorNotVerified");

            if(!Hashing.VerifyPassword(cvm.Customer.password, loggedCustomer.password))
            {
                return View();
            }

            MyPrinciple mp = new MyPrinciple(cvm.Customer);
            FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1, "user", DateTime.Now, DateTime.Now.AddHours(1), false, JsonConvert.SerializeObject(mp.Customer));
            HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat));
            httpCookie.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Add(httpCookie);
            checkSub(loggedCustomer);
            return Redirect(url);
        }
        public ActionResult ErrorNotVerified() {
            return View();
        }
        public ActionResult Signout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Signin", "customers");
        }

        private void checkSub(customer customer) {
            if ((bool)!customer.is_subbed)
                return;

            var end_sub = customer.customer_subscription
                                      .Where(e => e.customer_id == customer.customer_id)
                                      .OrderByDescending(e => e.customer_subscription_id)
                                      .First().end_date;
            if (end_sub < DateTime.Now) customer.is_subbed = false;
            db.SaveChanges();
        }

        /********** Register **********/
        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "fullname,phone,email,address,username,password")] customer customer) {
            if (!ModelState.IsValid)
                return View(customer);
            
            customer.date_created = DateTime.Now;
            customer.is_verified = false;
            customer.is_subbed = false;
            customer.password = Hashing.HashPassword(customer.password);
            db.customers.Add(customer);
            db.SaveChanges();

            int randomToken = RandomToken.GetToken();
            customer_confirmation cc = new customer_confirmation()
            {
                customer_id = customer.customer_id,
                date_created = DateTime.Now,
                token = randomToken,
                isValid = 1
            };
            db.customer_confirmation.Add(cc);
            db.SaveChanges();

            string subject = "OnelineLaundry - Verification Info";
            var email = new BuildMailTemplate(cc.confirmation_id, customer.email, randomToken, subject);
            email.BuildEmail();
            return RedirectToAction("RegisterSuccess");
        }

        public ActionResult RegisterSuccess() {
            return View();
        }

        public ActionResult Confirm(int confirmationid) {
            Session.Add("id", confirmationid);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm([Bind(Include = "confirmation_id, token")] customer_confirmation cc) {

            var confirm = db.customer_confirmation.Find(cc.confirmation_id);

            if (confirm.token != cc.token)
            {
                ViewBag.error = "Wrong verification code, please check again";
                return View();
            }

            confirm.isValid = 0;
            db.SaveChanges();

            var account = db.customers.Where(e => e.customer_id == confirm.customer_id).SingleOrDefault();
            account.is_verified = true;
            db.SaveChanges();
            Session.Remove("id");
            return RedirectToAction("VerifySuccess");
        }

        public ActionResult VerifySuccess() {
            return View();
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
