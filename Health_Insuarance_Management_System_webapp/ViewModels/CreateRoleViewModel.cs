using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Health_Insuarance_Management_System_webapp.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        //[Remote(action: "IsRoleInUse", controller: "Administration")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
