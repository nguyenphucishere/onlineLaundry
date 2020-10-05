using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLaundry.Models.ViewModel {
    public class CartModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Service_Id { get; set; }
        public int Amount { get; set; }
        public string Comment { get; set; }
        public bool UseSub { get; set; }
        public int Total_Piece { get; set; }
    }
}