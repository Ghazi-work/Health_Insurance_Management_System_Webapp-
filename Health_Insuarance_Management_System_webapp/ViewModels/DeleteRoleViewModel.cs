using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Health_Insuarance_Management_System_webapp.ViewModels
{
    public class DeleteRoleViewModel
    {
        

        public string Id { get; set; }

        [Display(Name = "Role Name")]  
        public string RoleName{ get; set; }

        

    }
}
