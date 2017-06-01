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
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "URL")]
        public string Url { get; set; }

        public string SuccessMessage { get; set; }
    }

   
}