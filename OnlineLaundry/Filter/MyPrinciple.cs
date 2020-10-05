using OnlineLaundry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace OnlineLaundry.Filter {
    public class MyPrinciple : IPrincipal {
        public customer Customer { get; set; }
        public IIdentity Identity { get; set; }

        public MyPrinciple(customer customer) {
            Customer = customer;
            Identity = new GenericIdentity(customer.username);
        }

        public bool IsInRole(string role) {
            return true;
        }
    }
}