using Health_Insuarance_Management_System_webapp.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Health_Insuarance_Management_System_webapp.Models
{
    public class ApplicationUser : IdentityUser
    {
        //Personal Information
        public string FirstName  { get; set; }
        public string LastName  { get; set; }
        public string PhotoPath { get; set; }
        public int Age { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime DateOfBirth{ get; set; } 
        public string CNIC { get; set; }
        public string TemporaryAddress { get; set; }
        public string PermenantAddress { get; set; }
        public EducationEnum Education { get; set; }
        public MaritalStatusEnum MartialStatus { get; set; }
        public int PersonalPhoneNumber { get; set; }
        public int HomePhoneNumber { get; set; }
        public int EmergencyPhoneNumber { get; set; }

        //Health Information


        //Admin Enteries
        public DateTime JoinDate { get; set; }
        public int Salary { get; set; }


        [ForeignKey("departments")]
        public int DeptId { get; set; }
        public DepartmentModel departments { get; set; }
        [ForeignKey("Policy")]
        public int PolicyId { get; set; }
        public PolicyModel Policy { get; set; }
        public int ClaimMoney { get; set; }
        


    }
}
