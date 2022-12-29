using Health_Insuarance_Management_System_webapp.DataAccess;
using Health_Insuarance_Management_System_webapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Health_Insuarance_Management_System_webapp.Controllers
{
    [Authorize(Roles ="Admin,Manager")]
    public class ClaimRequestController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment webHost;
        private readonly ApplicationDbContext context;

        public ClaimRequestController(UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager,
         IWebHostEnvironment webHost, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHost = webHost;
            this.context = context;
        }

        public IActionResult ClaimsList()
        {
            ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
            ViewData["CompanyId"] = new SelectList(context.Set<InsuranceCompanyModel>(), "CompanyId", "InsuranceCompanyName");
            var claims = context.Claim_Policy.ToList();
            return View(claims);
        }


        [HttpGet]
        public IActionResult EditClaimRequest(int id)
        {
            ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
            ViewData["CompanyId"] = new SelectList(context.Set<InsuranceCompanyModel>(), "CompanyId", "InsuranceCompanyName");
            var request = context.Claim_Policy.Find(id);
            if (request == null)
            {
                return View("NotFound");
            }
            ClaimPolicyModel model = new ClaimPolicyModel
            {
                UserId = request.UserId,
                FnameLname = request.FnameLname,
                CompId = request.CompId,
                PolicyId = request.PolicyId,
                Photopath = request.Photopath,
                PostingDate = request.PostingDate,
                ClaimAmount = request.ClaimAmount,
                UserReason = request.UserReason,
                Status = request.Status,
                ClaimId = request.ClaimId

               


            };
            return View(model);
        }


        [HttpPost]
        public IActionResult EditClaimRequest(ClaimPolicyModel model)
        {

            ClaimPolicyModel request = context.Claim_Policy.Find(model.ClaimId);
            if (request == null)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                request.UserId = model.UserId;
                request.FnameLname = model.FnameLname;
                request.PolicyId = model.PolicyId;
                request.CompId = model.CompId;
                request.UserReason = model.UserReason;
                request.Photopath = model.Photopath;
                request.PostingDate = model.PostingDate;
                request.Status = model.Status;
                request.ClaimAmount = model.ClaimAmount;
                request.AdminReasons = model.AdminReasons;

                context.Claim_Policy.Update(request);
                context.SaveChanges();
                if (User.IsInRole("Admin"))
                {
                    TempData["success"] = "Information Updated Successfully";
                    return RedirectToAction("ClaimsListForAdmin", "Administration");
                }
                else
                {
                    TempData["success"] = "Information Updated Successfully";
                    return RedirectToAction("ClaimsList");

                }



            }

            return View(model);
        }




    }
}
