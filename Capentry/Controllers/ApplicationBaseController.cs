﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capentry.Models;

namespace Capentry.Controllers
{
    public class ApplicationBaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var context = new ApplicationDbContext();
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {
                    var user = context.Users.SingleOrDefault(u => u.UserName == username);
                    string fullName = string.Concat(new string[] { user.FirstName, " ", user.LastName });
                    ViewData.Add("FullName", fullName);
                }
                else 
                {
                    //get the usernam/email address if no full name
                    ViewData.Add("FullName",username.ToString());
                }
            }
            base.OnActionExecuted(filterContext);
        }
        public ApplicationBaseController()
        { }
    }
}