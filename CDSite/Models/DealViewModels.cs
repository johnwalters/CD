using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CDSite.Models
{
    public class DealLoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class DealRegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Url")]
        public string Url { get;set; }

        //TODO: Change this to Token
        public string OfferToken { get; set; }

        [Display(Name = "Code")]
        public string OfferCode { get; set; }

        public bool IsCodeAvailable { get; set; }
        
        
    }

}
