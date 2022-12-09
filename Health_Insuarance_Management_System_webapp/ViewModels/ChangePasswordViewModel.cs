using System.ComponentModel.DataAnnotations;

namespace Health_Insuarance_Management_System_webapp.ViewModels
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="The New Password and Confirm Password donot match!")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
