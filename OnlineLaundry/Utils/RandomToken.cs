using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineLaundry.Utils {
    public class RandomToken {
        static Random random = new Random();

        public static int GetToken() {
            return random.Next(100000, 1000000);
        }
    }
}