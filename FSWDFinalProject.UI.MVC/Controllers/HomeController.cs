using FSWDFinalProject.UI.MVC.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace FSWDFinalProject.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }
            string body = $"You have recieved a message from {cvm.Name} with a subject of {cvm.Subject}. Please respond to {cvm.Email} in regards to the following message: <br><br>{cvm.Message}";

            MailMessage msg = new MailMessage("admin@jamesearp.com", "jamesaearp@outlook.com", "You recieved a Not So Job Bored Message", body);
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient("mail.jamesearp.com");
            client.Credentials = new NetworkCredential("admin@jamesearp.com", "Incorrect1999#");
            client.Port = 8889;
            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                ViewBag.SendEmailError = $"The message has not been sent. Message: {ex.Message}";
                return View(cvm);

            }
            return View("EmailConfirmation", cvm);
        }

    }
}
