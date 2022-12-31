using Health_Insuarance_Management_System_webapp.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health_Insuarance_Management_System_webapp.Models
{
    public class PolicyRequestModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FnameLname { get; set; }
        [ForeignKey("Policy")]
        [Display(Name ="Policy Title")]
        public int? PolicyId { get; set; }
        public PolicyModel Policy { get; set; }
        [ForeignKey("Company")]
        [Display(Name = "Company Name")]
        public int? CompId { get; set; }
        public InsuranceCompanyModel Company{ get; set; }
        public string Reason{ get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string PhotoPath { get; set; }
        public StatusEnum Status { get; set; }
    }
}
