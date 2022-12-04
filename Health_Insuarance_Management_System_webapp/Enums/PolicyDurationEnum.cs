using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Health_Insuarance_Management_System_webapp.Enums
{
    public enum PolicyDurationEnum
    {
        None,
        [Display(Name = "Six months")]
        Six_months,
        Year,
        Lifetime
    }
}
