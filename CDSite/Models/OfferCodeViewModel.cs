using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CDSite.Models
{
    public class OfferCodeViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}