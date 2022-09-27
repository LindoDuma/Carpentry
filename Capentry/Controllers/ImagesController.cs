﻿using System;
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

namespace Capentry.Controllers
{
    public class ImagesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: ImageModels
        public ActionResult Index(ProjectTypes? ProjectType)
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");

            ViewBag.ProjectType = new SelectList((ProjectTypes[])Enum.GetValues(typeof (ProjectTypes)), "ProjectTypes");

            var imageList = db.ImageModels.ToList();

            var projectList = db.Projects.Where(h => h.ProjectType == ProjectType).Select(b => b.ProjectID).ToList();

            if (ProjectType != null)
            {
                imageList = db.ImageModels.Where(y => projectList.Contains(y.ProjectID)).ToList();
            }

            return View(imageList);
        }

        public JsonResult GetImages(ProjectTypes? ProjectType)
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");

            ViewBag.ProjectType = new SelectList((ProjectTypes[])Enum.GetValues(typeof(ProjectTypes)), "ProjectTypes");

            var imageList = db.ImageModels.ToList();

            var projectList = db.Projects.Where(h => h.ProjectType == ProjectType).Select(b => b.ProjectID).ToList();

            if (ProjectType != null)
            {
                imageList = db.ImageModels.Where(y => projectList.Contains(y.ProjectID)).ToList();
            }

            return Json(imageList, JsonRequestBehavior.AllowGet);
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
    }
}
