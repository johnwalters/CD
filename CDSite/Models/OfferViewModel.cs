﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CDSite.Models
{
    public class OfferViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")] 
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "URL")]
        public string Url { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        //May not be right
        [Display (Name = "Token")]
        public string Token { get; set; }

        [Display(Name = "Total Codes")]
        public int TotalCodes { get; set; }

        [Display(Name = "Total Claimed")]
        public int TotalClaimed { get; set; }

        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }
    }

    
   
}