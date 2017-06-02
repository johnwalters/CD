using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CDSite.Models
{
    public class OfferListViewModel
    {
        public List<OfferViewModel> OfferList {get; set;}
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "URL")]
        public string URL { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }
    }
}