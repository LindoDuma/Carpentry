using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Capentry.Models;

namespace Capentry.ViewModels
{
    public class ImageViewModel
    {
        [Required(ErrorMessage ="Please select an image")]
        public HttpPostedFileBase Files { get; set; }

        public int ProjectID { get; set; }

        public Projects Project { get; set; }
    }
}