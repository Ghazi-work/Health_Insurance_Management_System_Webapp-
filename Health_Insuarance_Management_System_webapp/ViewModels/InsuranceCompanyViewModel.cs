using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Health_Insuarance_Management_System_webapp.ViewModels
{
    public class InsuranceCompanyViewModel
    {
        public int Id { get; set; }
        public IFormFile Photo { get; set; }
        public string existingPath { get; set; }
        [Required]
        [Display(Name ="Insurance Company Name")]
        public string InsuranceCompanyName { get; set; }
        [Required]
        [Display(Name = "Head office Address")]
        public string HeadOfficeAddress { get; set; }
        [Required]
        [Display(Name = "Helpline Number")]
        public string HelplineNumber { get; set; }
        [Required]
        [Display(Name = "Any other Information")]
        public string ExtraInformation { get; set; }
        [Required]
        [Display(Name = "Official Email")]
        public string OfficialEmail { get; set; }
        [Required]
        [Display(Name = "Website Url")]
        public string WebsiteUrl { get; set; }
    }
}
