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
        public string ProjectName { get; set; }
        public int ProjectYear { get; set; }

        public ProjectTypes ProjectType { get; set; }

        public virtual ICollection<Images>  Images{ get; set; }
    }
}