using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CDSite.Models
{
    public class OfferCodeListViewModel
    {
        public int OfferId { get; set; }
        public string OfferTitle { get; set; }

        public List<OfferCodeViewModel> OfferCodeList { get; set; }
        public string ErrorMessage { get; set; }

    }
}