using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capentry.Models
{
    public class DashboardController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.projects = db.Projects.Count();
            ViewBag.images = db.ImageModels.Count();
            ViewBag.users = db.Users.Count();

            return View();
        }
    }
}