using Health_Insuarance_Management_System_webapp.DataAccess;
using Health_Insuarance_Management_System_webapp.Models;
using Health_Insuarance_Management_System_webapp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Insuarance_Management_System_webapp.Controllers
{
    [Authorize]
    public class PolicyRequestController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment webHost;
        private readonly ApplicationDbContext context;

        public PolicyRequestController(UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager,
         IWebHostEnvironment webHost, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHost = webHost;
            this.context = context;
        }


        [HttpGet]
        public IActionResult ListPolicyUser()
        {
            var policies = context.Policies.ToList();

            return View(policies);
        }

        [HttpGet]
        public async Task<IActionResult> PolicyRequestUser(int id, string name)
        {
            var policy = context.Policies.Find(id);
            ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
            ViewData["CompanyId"] = new SelectList(context.Set<InsuranceCompanyModel>(), "CompanyId", "InsuranceCompanyName");

            var user = await userManager.FindByNameAsync(name);

            if (user == null && policy == null)
            {
                return View("NotFound");
            }
            PolicyRequestUserViewModel policyRequest = new PolicyRequestUserViewModel
            {
                UserName = user.Id,
                FnameLname = user.FirstName + " " + user.LastName,
                PolicyId = policy.PolicyId,
                CompId = policy.CompanyId,
                PhotoPath = policy.PhotoPath
                

            };
            
            return View(policyRequest);
        }

        [HttpPost]

        public IActionResult PolicyRequestUser(PolicyRequestUserViewModel model)
        {

            if (ModelState.IsValid)
            {
                PolicyRequestModel policy = new PolicyRequestModel
                {
                    UserName = model.UserName,
                    FnameLname = model.FnameLname,
                    PolicyId = model.PolicyId,
                    CompId = model.CompId,
                    Reason = model.Reason,
                    PhotoPath = model.PhotoPath,
                    Status = 0,
                    
                };
                context.Add(policy);
                context.SaveChanges();
                TempData["success"] = "Policy Request send Successfully";
                return RedirectToAction("Index","Home");

            }
          

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PolicyRequestStatus(string name)
        {
            ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
            ViewData["CompanyId"] = new SelectList(context.Set<InsuranceCompanyModel>(), "CompanyId", "InsuranceCompanyName");
            var user = await userManager.FindByNameAsync(name);
            if (user == null)
            {
                return View("NotFound");    
            }
            var policyreq = context.Policy_Requests.Where(a => a.UserName == user.Id).ToList();

            return View(policyreq);
        }

        [HttpGet]
        public async Task<IActionResult> ClaimPolicy(string name)
        {
            ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
            ViewData["CompanyId"] = new SelectList(context.Set<InsuranceCompanyModel>(), "CompanyId", "InsuranceCompanyName");

            var user = await userManager.FindByNameAsync(name);
            var policy = context.Policies.Find(user.PolicyId);
            if (user == null)
            {
                return View("NotFound");
            }
            ClaimPolicyViewModel claim = new ClaimPolicyViewModel
            {
                UserId  = user.Id,
                FnameLname = user.FirstName + " " + user.LastName,
                PolicyId = user.PolicyId,
                CompId = policy.CompanyId
                
                
                
            };

            return View(claim);
        }

        [HttpPost]
        public IActionResult ClaimPolicy(ClaimPolicyModel model, IFormFile file)
        {
            var folder = "";
            if (file != null)
            {
                folder = @"images/Claims/";
                folder += Guid.NewGuid().ToString() + "_" + file.FileName;
                var serverFolder = Path.Combine(webHost.WebRootPath, folder);
                using (var fileStream = new FileStream(serverFolder, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                
            }
            if (ModelState.IsValid)
            {
                var claim = new ClaimPolicyModel
                {
                    UserId = model.UserId,
                    FnameLname = model.FnameLname,
                    PolicyId = model.PolicyId,
                    CompId = model.CompId, 
                    Photopath = folder,
                    ClaimAmount = model.ClaimAmount,
                    UserReason = model.UserReason,
                    Status = 0

                };
                context.Add(claim);
                context.SaveChanges();
                TempData["success"] = "Claim Request send Successfully";
                return RedirectToAction("Index", "Home");

            }

            return View(model);


            
        }

        [HttpGet]
        public async Task<IActionResult> ClaimRequestStatus(string name)
        {
            ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
            ViewData["CompanyId"] = new SelectList(context.Set<InsuranceCompanyModel>(), "CompanyId", "InsuranceCompanyName");
            var user = await userManager.FindByNameAsync(name);
            if (user == null)
            {
                return View("NotFound");
            }
            var claimreq = context.Claim_Policy.Where(a => a.UserId == user.Id).ToList();

            return View(claimreq);
        }



    }
}
