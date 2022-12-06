using Health_Insuarance_Management_System_webapp.Enums;
using Health_Insuarance_Management_System_webapp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Health_Insuarance_Management_System_webapp.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Required]
        [MaxLength(20,ErrorMessage ="First Name cannot be more than 20 characters")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Last Name cannot be more than 30 characters")]
        public string LastName { get; set; }
        public IFormFile Photo{ get; set; }
        [Required]
        [Range(18,80,ErrorMessage ="Age must be between 18 - 80 ")]
        public int Age { get; set; }
        [Required]
        public GenderEnum Gender { get; set; }
        [Remote(action: "IsEmailInUse", controller: "Account")]

        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [RegularExpression("^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$",ErrorMessage ="Invalid CNIC Format")]
        public string CNIC { get; set; }
        [Required]
        public string TemporaryAddress { get; set; }
        [Required]
        public string PermenantAddress { get; set; }
        [Required]
        public EducationEnum Education { get; set; }
        [Required]
        public MaritalStatusEnum MaritalStatus { get; set; }
        [Required]
       [RegularExpression("^((\\+92)|(0092))-{0,1}\\d{3}-{0,1}\\d{7}$|^\\d{11}$|^\\d{4}-\\d{7}$",ErrorMessage ="Invalid Format")]
        public string PersonalPhoneNumber { get; set; }
        [RegularExpression("^((\\+92)|(0092))-{0,1}\\d{3}-{0,1}\\d{7}$|^\\d{11}$|^\\d{4}-\\d{7}$", ErrorMessage = "Invalid Format")]

        public string HomePhoneNumber { get; set; }
        [Required]
        public string EmergencyPhoneNumber { get; set; }

        //Health Information of the employees
        public BloodGroupEnum BloodGroup { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        public string DetailOfHealthDisease { get; set; }
        public string Medications { get; set; }
        public string StrenghtOfMedication { get; set; }
        public string FrequencyTaken { get; set; }



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
