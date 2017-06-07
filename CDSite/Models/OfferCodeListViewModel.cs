using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CDSite.Models
{
    public class OfferCodeListViewModel
    {
        public List<OfferCodeViewModel> OfferCodeList { get; set; }
        [Display(Name = "Code")]
        public string Title { get; set; }

    }
}