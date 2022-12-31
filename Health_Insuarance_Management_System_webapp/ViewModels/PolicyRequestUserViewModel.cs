using Health_Insuarance_Management_System_webapp.Enums;
using Health_Insuarance_Management_System_webapp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Health_Insuarance_Management_System_webapp.ViewModels
{
    public class PolicyRequestUserViewModel
    {
        public int Id { get; set; }
        public string PhotoPath { get; set; }
        public string UserName { get; set; }
        public string  FnameLname{ get; set; }
        public string Reason{ get; set; }
        public StatusEnum Status { get; set; }
        [ForeignKey("Policy")]
       
        [Display(Name = "Policy")]
        public int PolicyId { get; set; }
        public PolicyModel Policy { get; set; }
      
        [ForeignKey("Company")]
 
        [Display(Name = "Insurance Company")]
        public int CompId { get; set; }
        public InsuranceCompanyModel Company { get; set; }
        


    }
}
