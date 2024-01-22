using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capentry.Models;
using Capentry.ViewModels;
using CloudinaryDotNet;
using System.IO;
using CloudinaryDotNet.Actions;
using System.ComponentModel.DataAnnotations;

namespace Capentry.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index(string query)
        {
            var projects = db.Projects.OrderBy(x => x.ProjectID).ToList();

            ViewBag.message = "There are no projects";

            //Check if the query parameter is null or empty
            if (!String.IsNullOrEmpty(query))
            {
                projects = db.Projects.Where(x => x.ProjectName.Contains(query)).ToList();

                return View(projects);
            }
            //If it is then return a list of all Projects
            else
            {
                return View(projects);
            }

            //return View(projects);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel projectsViewModel)
        {
            if (ModelState.IsValid)
            {
                Projects project = new Projects
                {
                    ProjectName = projectsViewModel.ProjectName,
                    ProjectYear = projectsViewModel.ProjectYear,
                    ProjectType = projectsViewModel.ProjectType
                };

                db.Projects.Add(project);
                db.SaveChanges();

                //Redirect to projects list
                return RedirectToAction("Index");
            }
            return View(projectsViewModel);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ProjectName,ProjectType,ProjectYear")] Projects projects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projects);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projects projects = db.Projects.Find(id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projects projects = db.Projects.Find(id);
            db.Projects.Remove(projects);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
