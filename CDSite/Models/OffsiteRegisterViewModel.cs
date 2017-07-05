using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CDSite.Models
{
    public class OffsiteRegisterViewModel
    {
        [Display(Name = "Offer Token")]
        public string OfferToken { get; set; }
    }
}