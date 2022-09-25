using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Capentry.Models;

namespace Capentry.ViewModels
{
    public class ProjectViewModel
    {
        [Required(ErrorMessage ="Please enter the project name")]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name ="Project Year")]
        public int ProjectYear { get; set; }

        [Display(Name = "Project Type")]
        public ProjectTypes ProjectType { get; set; }

        //public HttpPostedFileBase[] Files { get; set; }
    }
}