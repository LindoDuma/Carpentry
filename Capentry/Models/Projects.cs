using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capentry.Models
{
    public class Projects
    {
        [Key]
        public int ProjectID { get; set; }
        [Display(Name ="Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Project Year")]
        public int ProjectYear { get; set; }

        [Display(Name = "Project Type")]
        public ProjectTypes ProjectType { get; set; }

        public virtual ICollection<Images>  Images{ get; set; }
    }
}