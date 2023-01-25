using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Net.Configuration;
using System.Web.Mvc;

namespace Capentry.ViewModels
{
    public class ContactViewModel
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public HttpPostedFileBase Attachment { get; set; }
    }
}