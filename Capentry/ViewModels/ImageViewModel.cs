using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capentry.ViewModels
{
    public class ImageViewModel
    {
        [Required(ErrorMessage ="Please select an image")]
        public HttpPostedFileBase File { get; set; }
    }
}