using OnlineLaundry.Models;
using OnlineLaundry.Models.Enums;
using OnlineLaundry.Models.ViewModel;
using OnlineLaundry.Utils;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLaundry.Controllers {
    public class OrderController : Controller {
        private const decimal shipping_fee = 10000;
        OnlineLaundryEntities db = new OnlineLaundryEntities();

        public ActionResult PurchaseSuccess() {
            return View();
        }

        [HttpPost]
        public ActionResult ProcessOrder(bool shipping, DateTime deliver_date) {
            List<CartModel> models = (List<CartModel>)Session["cart"];
            order order = new order();
            int id = db.customers.SingleOrDefault(e => e.username == User.Identity.Name).customer_id;
            decimal shippingPrice = shipping == false ? 0 : shipping_fee;

            order.customer_id = id;
            order.shipping = shipping ? (byte)1 : (byte)0;
            order.deliver_date = deliver_date;
            order.status = OrderStatus.Pending.ToString();
            order.total_pieces = models.Sum(e => e.Total_Piece);
            order.total_price = models.Sum(e => e.Price) + shipping_fee;
            db.orders.Add(order);
            db.SaveChanges();

            int newOrderId = db.orders.OrderByDescending(e => e.order_id).First().order_id;
            foreach (var item in models) {
                order_detail detail = new order_detail();
                detail.price = item.Price;
                detail.order_id = newOrderId;
                detail.quantity = item.Amount;
                //detail. = item.Comment;
                detail.service_id = item.Service_Id;
                OrderDetailServiceHelper(detail, item.Service_Id, item.Id);
                db.order_detail.Add(detail);
                db.SaveChanges();
            }

            Session.Remove("cart");
            return View();
        }

        private void OrderDetailServiceHelper(order_detail detail, int service_id, int item_id) {
            switch (service_id) {
                case 1:
                    detail.pieces_id = item_id;
                    detail.weight_id = null;
                    detail.subscription_id = null;
                    detail.unit = OrderDetailUnits.pcs.ToString();
                    break;
                case 2:
                    detail.pieces_id = null;
                    detail.weight_id = item_id;
                    detail.subscription_id = null;
                    detail.unit = OrderDetailUnits.kg.ToString();
                    break;
                case 3:
                    detail.pieces_id = null;
                    detail.weight_id = null;
                    detail.subscription_id = item_id;
                    detail.unit = OrderDetailUnits.package.ToString();
                    detail.price = 0;
                    break;
                default:
                    break;
            }
        }

        [HttpPost]
        public ActionResult CustomerSubscribe(customer_subscription Customer_Subscription) {
            customer_subscription subscription = new customer_subscription();
            subscription.subscription_id = Customer_Subscription.subscription_id;
            subscription.customer_id = Customer_Subscription.customer_id;
            subscription.start_date = DateTime.Now;
            subscription.end_date = DateTime.Now.AddDays(30);
            db.customer_subscription.Add(subscription);
            db.SaveChanges();

            db.customers.SingleOrDefault(e => e.customer_id == Customer_Subscription.customer_id).is_subbed = true;
            db.SaveChanges();

            //var email = new BuildMailTemplate(db.customers.SingleOrDefault(e => e.username == User.Identity.Name).email, "OnlineLaundry - Service");
            //email.BuildEmail();
            ViewBag.msg = "Thank you for subscribing";
            return RedirectToAction("SubsSuccess", "Order");
        }
    }
}