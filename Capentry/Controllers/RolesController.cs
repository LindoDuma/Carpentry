using Capentry.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capentry.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RolesController : ApplicationBaseController
    {
        ApplicationDbContext context;
        public RolesController() {
            context = new ApplicationDbContext();
        }
        // GET: Roles
        public ActionResult Index()
        {
            var _roles = context.Roles;

            return View(_roles);
        }
        [HttpGet]
        public ActionResult Create() 
        {
            var _roles = new IdentityRole();

            return View(_roles);
        }
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}