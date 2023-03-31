using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capentry.Models;
using CloudinaryDotNet;
using System.IO;
using CloudinaryDotNet.Actions;
using Capentry.ViewModels;
using System.Runtime.CompilerServices;
using Microsoft.Ajax.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Capentry.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ImagesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: ImageModels
        public ActionResult Index(int? ProjectId, ProjectTypes? ProjectType)
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");

            ViewBag.ProjectType = new SelectList((ProjectTypes[])Enum.GetValues(typeof (ProjectTypes)), "ProjectTypes");

            ViewBag.CurrentTab = ProjectType ?? null;

            var imageList = db.ImageModels.ToList();

            var projectList = db.Projects.Where(h => h.ProjectType == ProjectType).Select(b => b.ProjectID).ToList();

            if (ProjectType != null)
            {
                imageList = db.ImageModels.Where(y => projectList.Contains(y.ProjectID)).ToList();
            }

            if (ProjectId != null)
            {
                imageList = db.ImageModels.Where(y => y.ProjectID == ProjectId).ToList();
            }

            return View(imageList);
        }

        [AllowAnonymous]
        public ActionResult Gallery(ProjectTypes? ProjectType)
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");

            ViewBag.ProjectType = new SelectList((ProjectTypes[])Enum.GetValues(typeof(ProjectTypes)), "ProjectTypes");

            ViewBag.CurrentTab = ProjectType ?? null;

            var imageList = db.ImageModels.ToList();

            var projectList = db.Projects.Where(h => h.ProjectType == ProjectType).Select(b => b.ProjectID).ToList();

            if (ProjectType != null)
            {
                imageList = db.ImageModels.Where(y => projectList.Contains(y.ProjectID)).ToList();
            }

            return View(imageList);
        }

        // GET: ImageModels/Create
        [HttpGet]
        public ActionResult UploadImage()
        {
            ImageViewModel imageViewModel = new ImageViewModel();
            ViewBag.Projects = new SelectList (db.Projects,"ProjectID","ProjectName");
            return View(imageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(ImageViewModel imagesVM)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account(
                              "dfi0awyos",
                              "593298893595769",
                              "QLDO8_T6OQ1vwqvajZMb9m39OGw");

                Cloudinary cloudinary = new Cloudinary(account);
                if (imagesVM.Files.ContentLength > 0)
                {

                    string fileName = Path.GetFullPath(imagesVM.Files.FileName);
                    
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(fileName,imagesVM.Files.InputStream),

                        Folder = "CarpentryImages/",

                        UseFilename = false,
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);

                    Images imageModel = new Images
                    {
                        ImageName = fileName,
                        ImagePath = uploadResult.SecureUrl.AbsolutePath,
                        PublicID = uploadResult.PublicId,
                        ProjectID = imagesVM.ProjectID,
                    };

                    db.ImageModels.Add(imageModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(imagesVM);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ImageProjectViewModel imagesVM = new ImageProjectViewModel();   

            Images images = db.ImageModels.Find(id);

            Projects projects = db.Projects.Where(y => y.ProjectID == images.ProjectID).FirstOrDefault();

            imagesVM.images = images;
            imagesVM.projects = projects;

            if (images == null)
            {
                return HttpNotFound();
            }
            return View(imagesVM);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.ImageModels.Find(id);

            ViewBag.Projects = new SelectList(db.Projects, "ProjectID", "ProjectName");

            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageID,ImageName,ImagePath,Project,ProjectID,PublicID")] Images images)
        {
            if (ModelState.IsValid)
            {
                //Images original = db.ImageModels.Where(i => i.ImageID == images.ImageID).FirstOrDefault();

                //images = new Images { ImageID = images.ImageID,
                //ImageName = images.ImageName,
                //ImagePath = original.ImagePath,
                //PublicID = original.PublicID,
                //Project = images.Project,
                //ProjectID = images.ProjectID};

                db.Entry(images).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(images);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = db.ImageModels.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = new Account(
                              "dfi0awyos",
                              "593298893595769",
                              "QLDO8_T6OQ1vwqvajZMb9m39OGw");

            Cloudinary cloudinary = new Cloudinary(account);

            Images images = db.ImageModels.Find(id);

            var deletionParams = new DeletionParams(images.PublicID)
            {
                PublicId = images.PublicID
            };

            cloudinary.Destroy(deletionParams);

            db.ImageModels.Remove(images);
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
