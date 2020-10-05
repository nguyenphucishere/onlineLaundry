using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLaundry.Models.ViewModel {
    public class ServiceWeightViewModel {
        public order Order { get; set; }
        public order_detail Order_Detail { get; set; }
        public CartModel Cart { get; set; }
        public IEnumerable<service_weight> Service_Weights { get; set; }
    }
}