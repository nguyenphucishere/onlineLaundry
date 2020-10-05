using OnlineLaundry.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Hosting;
using System.Web.UI.WebControls;

namespace OnlineLaundry.Utils {
    public class BuildMailTemplate {
        //private OnlineLaundryEntities db = new OnlineLaundryEntities();
        public int ConfirmationId { get; set; }
        public int Token { get; set; }
        public string MailTo { get; set; }
        public string Subject { get; set; }

        public BuildMailTemplate(string mailTo, string subject) {
            MailTo = mailTo;
            Subject = subject;
        }
        public BuildMailTemplate(int confirmationId, string mailTo, int token, string subject) {
            ConfirmationId = confirmationId;
            MailTo = mailTo;
            Token = token;
            Subject = subject;
        }
        public void BuildEmail() {
            string body = File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            //var regInfo = db.customers.Where(e => e.customer_id == RegId).FirstOrDefault();
            var url = "https://localhost:44355/customers/Confirm?confirmationid=" + ConfirmationId;
            body = body.Replace("@ViewBag.ConfirmLink", url);
            body = body.Replace("@ViewBag.Code", Token.ToString());
            body = body.ToString();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("hainammailer@gmail.com");
            mail.To.Add(new MailAddress(MailTo));
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Subject = Subject;
            SendEmail(mail);
        }

        private void SendEmail(MailMessage mail) {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("hainammailer@gmail.com", "vklnamhai");
            client.Send(mail);
        }
    }
}