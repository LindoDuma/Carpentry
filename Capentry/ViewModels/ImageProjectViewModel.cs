using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Capentry.Models;

namespace Capentry.ViewModels
{
    public class ImageProjectViewModel
    {
        public Projects projects { get; set; }

        public Images images { get; set; }  

    }
}