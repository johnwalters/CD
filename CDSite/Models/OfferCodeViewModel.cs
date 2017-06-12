﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CDSite.Models
{
    public class OfferCodeViewModel
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Display(Name = "Codes")]
        public string Codes { get; set; }
        

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}