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
        public string ProjectName { get; set; }

        
        public int ProjectYear { get; set; }
        
        public ProjectTypes ProjectType { get; set; }

        //public HttpPostedFileBase[] Files { get; set; }
    }
}