using Health_Insuarance_Management_System_webapp.DataAccess;
using Health_Insuarance_Management_System_webapp.Models;
using Health_Insuarance_Management_System_webapp.ViewModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Health_Insuarance_Management_System_webapp.Controllers
{
    [Authorize(Roles = "Admin,IT, Finance Manager")]
  //  [Authorize(Roles = "IT(support)")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
		private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHost;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                         UserManager<ApplicationUser> userManager,
                                         ApplicationDbContext context,
                                         IWebHostEnvironment webHost)
        {
            this.roleManager = roleManager;
			this.userManager = userManager;
            this.context = context;
            this.webHost = webHost;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel formdata)
        {
            if (ModelState.IsValid)
            {

                IdentityRole identityRole = new IdentityRole
                {
                    Name = formdata.RoleName,
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    TempData["success"] = "Role Created Successfully";

                    return RedirectToAction("RoleList", "Administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
            }

            return View(formdata);
        }


        [HttpGet]
        public IActionResult RoleList()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name


            };
            foreach (var user in userManager.Users.ToList())
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.FirstName+" "+user.LastName+ " "+user.UserName);
                }

            }
            return View(model);
        }


        //[HttpPost]
        //public async Task<IActionResult> EditRole(EditRoleViewModel model)
        //{
        //    var role = await roleManager.FindByIdAsync(model.Id);
        //    if (role == null)
        //    {
        //        ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
        //        return View("NotFound");
        //    }
        //    else
        //    {
        //        role.Name = model.RoleName;
        //         var result =  await roleManager.UpdateAsync(role);

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("RoleList");
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error.Description);
        //            }
        //        }
        //        return View(model);
        //    }      
        //}
        //[HttpGet]
        //public async Task<IActionResult> DeleteRole(string id)
        //{
        //    var role = await roleManager.FindByIdAsync(id);
        //    if (role == null)
        //    {
        //        return View("NotFound");
        //    }
        //    var model = new DeleteRoleViewModel
        //    {
        //        Id = id,
        //        RoleName = role.Name
        //    };
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> DeleteRole(string id, DeleteRoleViewModel model)
        //{
        //    var role = await roleManager.FindByIdAsync(id);
        //    if (role == null)
        //    {
        //        return View("NotFound");
        //    }
        //    await roleManager.DeleteAsync(role);
           
        //    return RedirectToAction("RoleList");
        //}

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users.ToList())
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserRealName = user.FirstName +" " + user.LastName
                };
                if (await userManager.IsInRoleAsync(user,role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model,string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return View("NotFound");
            }
            for (int i = 0; i < model.Count(); i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user,role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                } 
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        TempData["success"] = "Role assigned Successfully";
                        return RedirectToAction("EditRole", new { Id = roleId });
                    }
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }

        public IActionResult ListUsers()
        {
            var userList = userManager.Users;
            return View(userList);

        }
        [HttpGet]
        [Authorize(Roles = "Admin,IT,Finance Manager")]
        public async Task<IActionResult> EditUser(string id)
        {
            ViewData["DepartmentId"] = new SelectList(context.Set<DepartmentModel>(), "DeptId", "DeptName");
            ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("NotFound");
            }
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                PhotoPath = user.PhotoPath,
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
                DeptId  = user.DeptId,
                PolicyId = user.PolicyId,
                ClaimMoney = user.ClaimMoney,
                Roles = userRoles
                


            };
         
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,IT,Finance Manager")]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    return View("NotFound");
                }
                else
                {

                    var folder = "";
                    if (model.Photo != null)
                    {

                        folder = @"images/employees/";
                        folder += Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                        var serverFolder = Path.Combine(webHost.WebRootPath, folder);
                        using (var fileStream = new FileStream(serverFolder, FileMode.Create))
                        {
                            model.Photo.CopyTo(fileStream);
                        }
                        user.PhotoPath = folder;

                        if (model.PhotoPath != null)
                        {
                            var oldDirectory = Path.Combine(webHost.WebRootPath, model.PhotoPath);
                            if (System.IO.File.Exists(oldDirectory))
                            {
                                System.IO.File.Delete(oldDirectory);
                            }

                        }
                    }
                    else
                    {
                        user.PhotoPath = model.PhotoPath;
                    }

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Age = model.Age;
                    user.Gender = model.Gender;
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
                        if (User.IsInRole("Admin"))
                        {
                            TempData["success"] = "User information Updated";
                            return RedirectToAction("ListUsers");
                        }
                        else
                        {
                            TempData["success"] = "Amount updated successfully";
                            return RedirectToAction("ClaimsListForFinance", "ClaimAmountRequest");
                        }
                        
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(" ", error.Description);
                    }

                    return View(model);

                }
            }
            else
            {
                return View(model);
            }
        
           

            
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == "b66dfac2-d3b0-4872-8f9c-07d80c025aaf")
            {
                return View("NotFound");
            }
            else
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.DeleteAsync(user);
                TempData["success"] = "User Deleted successfully";
                return RedirectToAction("ListUsers");
            }
          
        }

        public IActionResult ClaimsListForAdmin()
        {
            ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
            ViewData["CompanyId"] = new SelectList(context.Set<InsuranceCompanyModel>(), "CompanyId", "InsuranceCompanyName");
            var claims = context.Claim_Policy.ToList();
            return View(claims);
        }


        //[HttpGet]
        //[HttpPost]
        //public async Task<IActionResult> IsRoleInUse(string role)
        //{
        //    var user = await roleManager.FindByNameAsync(role);

        //    if (user == null)
        //    {
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json($"Role {role} is already in use");
        //    }


        //}


        #region API CALLS
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = userManager.Users;
            return Json(new { data = users });
        }
        #endregion

    }


}
