using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capentry.Controllers;
using Capentry.ViewModels;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Net.Configuration;
using System.IO;

namespace Capentry.Controllers
{
    public class HomeController : ApplicationBaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        [HttpGet]
        public ActionResult ContactForm() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactForm(ContactViewModel model) 
        {
            //Read SMTP section from Web.Config.
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            using (MailMessage mm = new MailMessage(smtpSection.From, "admin@aspsnippets.com"))
            {
                mm.Subject = model.Subject;
                mm.Body = "Name: " + model.Name + "<br /><br />Email: " + model.Email + "<br />" + model.Body;
                //if (model.Attachment.ContentLength > 0)
                //{
                //    string fileName = Path.GetFileName(model.Attachment.FileName);
                //    mm.Attachments.Add(new Attachment(model.Attachment.InputStream, fileName));
                //}
                mm.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = smtpSection.Network.Host;
                    smtp.EnableSsl = smtpSection.Network.EnableSsl;
                    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCred;
                    smtp.Port = smtpSection.Network.Port;
                    smtp.Send(mm);
                    ViewBag.Message = "Email sent sucessfully.";
                }
            }

            return View();
        }
    }
}