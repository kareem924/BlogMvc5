using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BlogMvc5.Models;

namespace BlogMvc5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Models.MailForm form)
        {
            //Send Mail
            MailMessage msg = new MailMessage();
            msg.To.Add("customerService@site.com");
            msg.From = new MailAddress("webmaster@site.com");
            msg.CC.Add("admin@site.com");

            msg.Subject = form.Subject;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Message From Site.com");
            sb.AppendFormat("<b>Customer Name </b>: {0} ", form.Name);
            sb.AppendFormat("Cutomer Mail : {0}", form.Mail);
            sb.AppendFormat("Subject : {0}", form.Subject);
            sb.AppendFormat("Message : {0}", form.Body);
            msg.IsBodyHtml = true;

            msg.Body = sb.ToString();
            //Gmail - Yahoo
            SmtpClient client = new SmtpClient("smtp.site.com", 25);
            client.Credentials = new NetworkCredential("UserName", "password");

            client.Send(msg);

            ViewBag.Message = "Your Message has been sent";

            return View();
        }
    }
}