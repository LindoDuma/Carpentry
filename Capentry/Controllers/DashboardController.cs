using Microsoft.AspNet.Identity;
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

            ViewBag.projectList = db.Projects.OrderBy(x => x.ProjectID).Take(5).ToList();
            ViewBag.usersList = db.Users.OrderBy(y => y.Id).ToList();

            //var currentUser = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();

            //var fullname = User.Identity.Name;
            //ViewBag.fullname = fullname;

            return View(ViewBag);
        }
    }
}