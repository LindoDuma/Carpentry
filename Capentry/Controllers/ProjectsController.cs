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
//using X.PagedList;
using PagedList;

namespace Capentry.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index(string query, string filter,string sortOrder, int? page ,int? pageSize )
        {
            var projects = from s in db.Projects
                           select s;


            ViewBag.message = "There are no projects";

            ViewData["Projects"] = projects;

            //Check if the query parameter is null or empty
            if (!String.IsNullOrEmpty(query))
            {
                projects = projects.Where(x => x.ProjectName.Contains(query) || x.ProjectYear.ToString() == query || x.ProjectType.ToString().Contains(query)).OrderBy(y => y.ProjectID);

            }
            else if (!String.IsNullOrEmpty(sortOrder)) {

                switch (sortOrder)
                {
                    case "Year":
                        projects = projects.OrderBy(x => x.ProjectYear);

                        break;
                    case "Year_Desc":
                        projects = projects.OrderByDescending(x => x.ProjectYear);

                        break;
                    case "Name":
                        projects = projects.OrderBy(x => x.ProjectName);

                        break;
                    case "Name_Desc":
                        projects = projects.OrderByDescending(x => x.ProjectName);

                        break;
                    default:
                        projects = projects.OrderBy(x => x.ProjectID);
                        break;
                }

            }
            else if (!String.IsNullOrEmpty(filter))
            {
                projects = projects.Where(x => x.ProjectType.ToString() == filter).OrderBy(x => x.ProjectID);
            }
            //If it is then return a list of all Projects
            else
            {
                projects = projects.OrderBy(x => x.ProjectID);
                
            }

            return View(projects.ToPagedList(page ?? 1, pageSize ?? 10));
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
            int minimumYear = System.DateTime.Now.Year;

            ProjectViewModel projectsViewModel = new ProjectViewModel();
            projectsViewModel.ProjectYear = minimumYear;

            return View(projectsViewModel);
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
