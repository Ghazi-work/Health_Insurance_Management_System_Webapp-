using Health_Insuarance_Management_System_webapp.DataAccess;
using Health_Insuarance_Management_System_webapp.Models;
using Health_Insuarance_Management_System_webapp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
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
        public IActionResult Register()
        {
            ViewData["DepartmentId"] = new SelectList(context.Set<DepartmentModel>(), "DeptId", "DeptName");
            ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName =  model.Email,
                    FirstName = model.FirstName,
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
                    JoinDate = model.JoinDate,
                    Salary = model.Salary,
                    DeptId = model.DeptId,
                    PolicyId = model.PolicyId


                    
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
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
        public IActionResult LogIn(string ReturnUrl)
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, false
                    );
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
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
    }
}
