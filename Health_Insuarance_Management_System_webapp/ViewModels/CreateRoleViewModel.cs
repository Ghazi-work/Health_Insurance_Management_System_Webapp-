using System.ComponentModel.DataAnnotations;

namespace Health_Insuarance_Management_System_webapp.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
