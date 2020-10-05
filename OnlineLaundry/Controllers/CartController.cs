using OnlineLaundry.Models;
using OnlineLaundry.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLaundry.Controllers {
    public class CartController : Controller {
        OnlineLaundryEntities db = new OnlineLaundryEntities();
        // GET: Cart
        public ActionResult Index() {
            List<CartModel> models = (List<CartModel>)Session["cart"];
            if (models == null) models = new List<CartModel>();
            return View(models);
        }

        [HttpPost]
        public ActionResult AddCart(CartModel Cart) {
            if (Cart.UseSub) {
                Cart.Service_Id = 3;
                Cart.Price = 0;
            }

            if (Cart.Service_Id == 1) {
                Cart.Name = db.service_pieces.Find(Cart.Id).name;
                Cart.Price = db.service_pieces.Find(Cart.Id).price;
            } else if (Cart.Service_Id == 2) {
                Cart.Name = db.service_weight.Find(Cart.Id).capacity + " kg";
                Cart.Price = db.service_weight.Find(Cart.Id).price;
            }

            List<CartModel> models;
            if (Session["cart"] == null) {
                models = new List<CartModel>();
                models.Add(Cart);
                Session.Add("cart", models);
            } else {
                models = (List<CartModel>)Session["cart"];
                models.Add(Cart);
                Session.Add("cart", models);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteCart(int id) {
            List<CartModel> models = (List<CartModel>)Session["cart"];
            models.RemoveAt(id);
            Session["cart"] = models;
            return RedirectToAction("Index");
        }
    }
}