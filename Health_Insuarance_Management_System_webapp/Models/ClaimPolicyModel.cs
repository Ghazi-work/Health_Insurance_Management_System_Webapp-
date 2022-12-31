using Health_Insuarance_Management_System_webapp.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health_Insuarance_Management_System_webapp.Models
{
    public class ClaimPolicyModel
    {
        [Key]
        public int ClaimId { get; set; }
        public string UserId  { get; set; }
        public string FnameLname { get; set; }
        [ForeignKey("Policy")]
        [Display(Name = "Policy Title")]
        public int? PolicyId { get; set; }
        public PolicyModel Policy { get; set; }
        [ForeignKey("Company")]
        [Display(Name = "Insurance Company Name")]
        public int? CompId { get; set; }
        public InsuranceCompanyModel Company { get; set; }
        public string Photopath { get; set; }
        public DateTime PostingDate { get; set; } = DateTime.Now;
        public int ClaimAmount { get; set; }
        public string UserReason { get; set; }
        public string AdminReasons { get; set; }
        public StatusEnum Status{ get; set;}

    }
}
