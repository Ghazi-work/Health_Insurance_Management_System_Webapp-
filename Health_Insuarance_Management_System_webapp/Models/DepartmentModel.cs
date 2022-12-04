using System.ComponentModel.DataAnnotations;

namespace Health_Insuarance_Management_System_webapp.Models
{
    public class Departments
    {
        [Key]
        public int DeptId { get; set; }
        public string DeptName { get; set;}
        public string DeptDescription { get; set;}
        public string DeptLocation { get; set;}
    }
}
