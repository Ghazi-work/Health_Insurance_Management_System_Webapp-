using Health_Insuarance_Management_System_webapp.DataAccess;
using Health_Insuarance_Management_System_webapp.Models;
using Health_Insuarance_Management_System_webapp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Insuarance_Management_System_webapp.Controllers
{
    [Authorize(Roles = "Admin,IT")]
  //  [Authorize(Roles = "IT(support)")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
		private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                         UserManager<ApplicationUser> userManager,
                                         ApplicationDbContext context)
        {
            this.roleManager = roleManager;
			this.userManager = userManager;
            this.context = context;
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


        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                 var result =  await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return View(model);
            }      
        }
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return View("NotFound");
            }
            var model = new DeleteRoleViewModel
            {
                Id = id,
                RoleName = role.Name
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id, DeleteRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return View("NotFound");
            }
            await roleManager.DeleteAsync(role);
           
            return RedirectToAction("RoleList");
        }

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
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
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
                JoinDate = user.JoinDate,
                Salary = user.Salary,
                DeptId  = user.DeptId,
                PolicyId = user.PolicyId,
                ClaimMoney = user.ClaimMoney,
                Roles = userRoles
                


            };
         
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
        
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return View("NotFound");
            }
            else
            {
                user.FirstName = model.FirstName;


                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
           
                foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(" ", error.Description);
                    }

                return View(model);
               
            }

            
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

    }
}
