using Health_Insuarance_Management_System_webapp.DataAccess;
using Health_Insuarance_Management_System_webapp.Models;
using Health_Insuarance_Management_System_webapp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Insuarance_Management_System_webapp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment webHost;
        private readonly ApplicationDbContext context;

        public AccountController(UserManager<ApplicationUser> userManager, 
               SignInManager<ApplicationUser> signInManager,
               IWebHostEnvironment webHost, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHost = webHost;
            this.context = context;
        }


        [HttpGet]
        public async  Task<IActionResult> ChangeDetails(string name)
        {
            ViewData["DepartmentId"] = new SelectList(context.Set<DepartmentModel>(), "DeptId", "DeptName");
            ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
            var user = await userManager.FindByNameAsync(name);
            if (user == null)
            {
                return View("NotFound");
            }
            var model = new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Gender = user.Gender,
                //DateOfBirth = user.DateOfBirth,
                CNIC = user.CNIC,
                TemporaryAddress = user.TemporaryAddress,
                PermenantAddress = user.PermenantAddress,
                Education = user.Education,
                MaritalStatus = user.MaritalStatus,
                PersonalPhoneNumber = user.PersonalPhoneNumber,
                HomePhoneNumber = user.HomePhoneNumber,
                EmergencyPhoneNumber = user.EmergencyPhoneNumber,
                BloodGroup = user.BloodGroup,
                Height = user.Height,
                Weight = user.Weight,
                DetailOfHealthDisease = user.DetailOfHealthDisease,
                Medications = user.Medications,
                StrenghtOfMedication = user.StrenghtOfMedication,
                FrequencyTaken = user.FrequencyTaken,

                Salary = user.Salary,
                DeptId = user.DeptId,
                PolicyId = user.PolicyId,
                ClaimMoney = user.ClaimMoney,

            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ChangeDetails(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return View("NotFound");
            }
            else
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Age = model.Age;
                user.Gender = model.Gender;
                //user.DateOfBirth = model.DateOfBirth;
                user.CNIC = model.CNIC;
                user.TemporaryAddress = model.TemporaryAddress;
                user.PermenantAddress = model.PermenantAddress;
                user.Education = model.Education;
                user.MaritalStatus = model.MaritalStatus;
                user.PersonalPhoneNumber = model.PersonalPhoneNumber;
                user.HomePhoneNumber = model.HomePhoneNumber;
                user.EmergencyPhoneNumber = model.EmergencyPhoneNumber;
                user.BloodGroup = model.BloodGroup;
                user.Height = model.Height;
                user.Weight = model.Weight;
                user.DetailOfHealthDisease = model.DetailOfHealthDisease;
                user.Medications = model.Medications;
                user.StrenghtOfMedication = model.StrenghtOfMedication;
                user.FrequencyTaken = model.FrequencyTaken;
                user.Salary = model.Salary;
                user.DeptId = model.DeptId;
                user.PolicyId = model.PolicyId;
                user.ClaimMoney = model.ClaimMoney;



                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(" ", error.Description);
                }

                return View(model);
            }

      
        }




        [HttpGet]
        public IActionResult ChangePasswordUser()
        {

            return View();
        }

        [HttpPost]  
        public async Task<IActionResult> ChangePasswordUser(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("LogIn");
                }
                var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }
                await signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Index","Home");
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewData["DepartmentId"] = new SelectList(context.Set<DepartmentModel>(), "DeptId", "DeptName");
            ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel model, IFormFile file)
        {
            var folder = "";
            if (file != null)
            {
                folder = @"images/employees/";
                folder += Guid.NewGuid().ToString() + "_" + file.FileName ;
                var serverFolder = Path.Combine(webHost.WebRootPath, folder);
               
                file.CopyTo(new FileStream(serverFolder, FileMode.Create));



            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName =  model.Email,
                    FirstName = model.FirstName,
                    PhotoPath = folder,
                    LastName = model.LastName,
                    Email = model.Email,
                    Age = model.Age,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    CNIC  = model.CNIC,
                    TemporaryAddress = model.TemporaryAddress,
                    PermenantAddress = model.PermenantAddress,
                    Education = model.Education,
                    MaritalStatus = model.MaritalStatus,
                    PersonalPhoneNumber = model.PersonalPhoneNumber,
                    HomePhoneNumber = model.HomePhoneNumber,
                    EmergencyPhoneNumber = model.EmergencyPhoneNumber,
                    BloodGroup = model.BloodGroup,
                    Height = model.Height,
                    Weight = model.Weight,
                    DetailOfHealthDisease = model.DetailOfHealthDisease,
                    Medications = model.Medications,
                    StrenghtOfMedication = model.StrenghtOfMedication,
                    FrequencyTaken  = model.FrequencyTaken,
                 
                    Salary = model.Salary,
                    DeptId = model.DeptId,
                    PolicyId = model.PolicyId


                    
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult LogIn()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, false
                    );
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                       return RedirectToAction("Index", "Home");

                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }

            return View(model);
        }


      

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            }


        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
