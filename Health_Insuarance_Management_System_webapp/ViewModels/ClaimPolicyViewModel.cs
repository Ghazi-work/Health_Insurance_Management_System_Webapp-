using Health_Insuarance_Management_System_webapp.Enums;
using Health_Insuarance_Management_System_webapp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Http;

namespace Health_Insuarance_Management_System_webapp.ViewModels
{
    public class ClaimPolicyViewModel
    {
        
        public int Id { get; set; }
        [Display(Name = "Employee Id")]
        public string UserId { get; set; }
        [Display(Name = "Employee Name")]
        public string FnameLname { get; set; }
        [ForeignKey("Policy")]
        [Display(Name = "Policy Name")]
        public int? PolicyId { get; set; }
        public PolicyModel Policy { get; set; }
        [ForeignKey("Company")]
        [Display(Name = "Insurance Company Name")]
        public int? CompId { get; set; }
        public InsuranceCompanyModel Company { get; set; }
        [Display(Name = "Final Bill Image")]
        [Required]
        public IFormFile Photo{ get; set; }

        public string ExistingPath { get; set; }
        public DateTime PostingDate { get; set; } = DateTime.Now;
        [Display(Name = "Claim Amount")]
        public int ClaimAmount { get; set; }
        [Display(Name = "Information about the claim")]
        [Required]
        public string UserReason { get; set; }
        public string AdminReasons { get; set; }
        public StatusEnum Status { get; set; }

    }
}
