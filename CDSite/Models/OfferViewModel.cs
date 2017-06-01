using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CDSite.Models
{
    public class OfferViewModel
    {
        [Display(Name = "Title")]
        public string OfferTitle { get; set; }

        [Display(Name = "Description")]
        public string OfferDescription { get; set; }

        [Display(Name = "URL")]
        public string OfferUrl { get; set; }
    }
}