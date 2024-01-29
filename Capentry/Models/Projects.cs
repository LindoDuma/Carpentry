using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;

namespace Capentry.Models
{
    public class Projects
    {
        [Key]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Please enter the name of the project")]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [Range(2020, 3000, ErrorMessage = "Please input a value between 2020 and the current year")]
        [Display(Name = "Project Year")]
        public int ProjectYear { get; set; }

        [Required(ErrorMessage = "Please select the type of project")]
        [Display(Name = "Project Type")]
        public ProjectTypes ProjectType { get; set; }

        public virtual ICollection<Images>  Images{ get; set; }
    }
}