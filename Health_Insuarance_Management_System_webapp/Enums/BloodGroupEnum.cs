using System.ComponentModel.DataAnnotations;

namespace Health_Insuarance_Management_System_webapp.Enums
{
    public enum BloodGroupEnum
    {
        None,
        [Display(Name ="O-")]
        O_negative,
        [Display(Name = "O+")]
        O_positive,
        [Display(Name = "A-")]
        A_negative,
        [Display(Name = "A+")]
        A_positive,
        [Display(Name = "B-")]
        B_negative,
        [Display(Name = "B+")]
        B_positive,
        [Display(Name = "AB-")]
        Ab_Negative,
        [Display(Name = "AB+")]
        Ab_Positive




    }
}
