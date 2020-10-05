using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace OnlineLaundry.Utils {
    public class Hashing {

        public static string HashPassword(string pw) {
            return BCrypt.Net.BCrypt.HashPassword(pw, BCrypt.Net.BCrypt.GenerateSalt(12));
        }

        public static bool VerifyPassword(string pw, string hash) {
            return BCrypt.Net.BCrypt.Verify(pw, hash);
        }
    }
}