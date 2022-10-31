using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Capentry.Models
{   
    public class Images
    {
        [Key]
        public int ImageID { get; set; }

        [DisplayName("Image Name")]
        public string ImageName { get; set; }
        public string ImagePath{ get; set; }

        [NotMapped]
        public HttpPostedFileBase Files { get; set; }

        public string PublicID { get; set; }

        public int ProjectID { get; set; }

        [ForeignKey("ProjectID")]
        public Projects Project { get; set; }
    }
}