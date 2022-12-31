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
	[Authorize(Roles = "Admin, Finance Manager")]
	public class ClaimAmountRequestController : Controller
	{


		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly IWebHostEnvironment webHost;
		private readonly ApplicationDbContext context;

		public ClaimAmountRequestController(UserManager<ApplicationUser> userManager,
		 SignInManager<ApplicationUser> signInManager,
		 IWebHostEnvironment webHost, ApplicationDbContext context)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.webHost = webHost;
			this.context = context;
		}

		public IActionResult ClaimsListForFinance()
		{
			ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
			ViewData["CompanyId"] = new SelectList(context.Set<InsuranceCompanyModel>(), "CompanyId", "InsuranceCompanyName");
			var claims = context.Claim_Policy.ToList();
			return View(claims);
		}


	}
}
