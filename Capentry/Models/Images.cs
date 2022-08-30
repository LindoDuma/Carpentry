using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capentry.Models
{   
    public class Images
    {
        [Key]
        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public string ImagePath{ get; set; }

        public string PublicID { get; set; }    

        //public Project Project { get; set; }
    }
}