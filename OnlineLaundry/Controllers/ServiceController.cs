using OnlineLaundry.Filter;
using OnlineLaundry.Models;
using OnlineLaundry.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLaundry.Controllers {
    public class ServiceController : Controller {
        private OnlineLaundryEntities db = new OnlineLaundryEntities();
        // GET: Service
        public ActionResult Index() {
            return View();
        }

        public ActionResult PieceService() {
            return View();
        }

        [Authorize]
        public ActionResult PieceServiceDetail() {
            ServicePieceViewModel viewModel = new ServicePieceViewModel() {
                Service_Pieces = db.service_pieces,
                Order = new order(),
                Cart = new CartModel()
            };
            var customer = db.customers.SingleOrDefault(e => e.username == User.Identity.Name);
            viewModel.Order.customer_id = customer.customer_id;
            ViewBag.issub = customer.is_subbed;
            return View(viewModel);
        }

        public ActionResult WeightService() {
            return View();
        }

        [Authorize]
        public ActionResult WeightServiceDetail() {
            ServiceWeightViewModel viewModel = new ServiceWeightViewModel() {
                Service_Weights = db.service_weight,
                Order = new order(),
                Cart = new CartModel()
            };
            var customer = db.customers.SingleOrDefault(e => e.username == User.Identity.Name);
            viewModel.Order.customer_id = customer.customer_id;
            ViewBag.issub = customer.is_subbed;
            return View(viewModel);
        }

        public ActionResult SubscriptionService() {
            return View(db.service_subscription);
        }

        [Authorize]
        public ActionResult SubscriptionServiceDetail(int subscription_id) {
            ServiceSubsViewModel viewModel = new ServiceSubsViewModel() {
                Service_Subscription = db.service_subscription.Find(subscription_id),
                Customer_Subscription = new customer_subscription()
            };
            viewModel.Customer_Subscription.customer_id = db.customers.SingleOrDefault(e => e.username == User.Identity.Name).customer_id;
            viewModel.Customer_Subscription.subscription_id = subscription_id;
            return View(viewModel);
        }
    }
}