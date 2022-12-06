using System.ComponentModel.DataAnnotations;

namespace Health_Insuarance_Management_System_webapp.Models
{
    public class InsuranceCompanyModel
    {
        [Key]
        public int CompanyId { get; set; }
        public string InsuranceCompanyName { get; set; }
        public string HeadOfficeAddress { get; set; }
        public string HelplineNumber { get; set;}
        public string ExtraInformation { get; set; }
        public string OfficialEmail { get; set; }
        public string WebsiteUrl { get; set; }
    }
}
