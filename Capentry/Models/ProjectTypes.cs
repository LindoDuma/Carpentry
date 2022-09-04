using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capentry.Models
{
    public enum ProjectTypes
    {
        [Display(Description ="Kitchen")]
        Kitchen,
        [Display(Description = "Wardrobe")]
        Wardrobe,
        [Display(Description = "TV Unit")]
        TVUnit
    }
}