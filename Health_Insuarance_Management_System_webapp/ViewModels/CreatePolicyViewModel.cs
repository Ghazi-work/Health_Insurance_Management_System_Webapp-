using Health_Insuarance_Management_System_webapp.Enums;
using Health_Insuarance_Management_System_webapp.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health_Insuarance_Management_System_webapp.ViewModels
{
    public class CreatePolicyViewModel
    {
        public int Id { get; set; }
        [ForeignKey("Company")]
        [Required]
        [Display(Name = "Insurance Company")]
        public int CompanyId { get; set; }
        public InsuranceCompanyModel Company { get; set; }
        [Required]
        [Display(Name ="Policy Title")]
        public string PolicyTitle { get; set; }
        [Required]
        [Display(Name = "Policy Description")]
        public string PolicyDescription { get; set; }
        [Required]
        [Display(Name = "Policy Duration (availablity)")]
        public PolicyDurationEnum PolicyDuration { get; set; }
        [Required]
        [Display(Name = "Policy Payment Type")]
        public PolicyPaymentEnum PolicyPaymentType { get; set; }
        [Display(Name = "Policy profile photo")]
    
        public IFormFile Photo { get; set; }
        public string PhotoPath { get; set; }
        [Required]
        [Display(Name = "Payment")]
        public int Payment { get; set; }
        [Required]
        [Display(Name = "Budget of the policy that it covers")]
        public int Budget { get; set; }
    }
}
