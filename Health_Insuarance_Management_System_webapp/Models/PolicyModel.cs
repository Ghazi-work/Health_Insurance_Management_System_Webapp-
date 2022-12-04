using Health_Insuarance_Management_System_webapp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health_Insuarance_Management_System_webapp.Models
{
    public class PolicyModel
    {
        [Key]
        public int PolicyId { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public InsuranceCompanyModel Company { get; set; }
        public string PolicyTitle { get; set; }
        public string PolicyDescription { get; set; }
        public PolicyDurationEnum PolicyDuration { get; set; }
        public PolicyPaymentEnum PolicyPaymentType { get; set; }
        public int Payment { get; set; }
        public int Budget { get;}
    }
}
