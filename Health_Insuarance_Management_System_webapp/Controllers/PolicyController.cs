using Health_Insuarance_Management_System_webapp.DataAccess;
using Health_Insuarance_Management_System_webapp.Models;
using Health_Insuarance_Management_System_webapp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Health_Insuarance_Management_System_webapp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PolicyController : Controller
    {
        private readonly IWebHostEnvironment webHost;
        private readonly ApplicationDbContext context;
        public PolicyController(IWebHostEnvironment webHost, ApplicationDbContext context )
        {
            this.webHost = webHost;
            this.context = context;
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(context.Set<InsuranceCompanyModel>(), "CompanyId", "InsuranceCompanyName");

            return View();
        }


        [HttpPost]
        public IActionResult Create(CreatePolicyViewModel model, IFormFile file)
        {
            var folder = "";
            if (file != null)
            {
                folder = @"images/Policies/";
                folder += Guid.NewGuid().ToString() + "_" + file.FileName;
                var serverFolder = Path.Combine(webHost.WebRootPath, folder);
                using (var fileStream = new FileStream(serverFolder, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                //file.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }
            if (ModelState.IsValid)
            {
                var policy = new PolicyModel
                {
                    PolicyTitle = model.PolicyTitle,
                    PolicyDescription = model.PolicyDescription,
                    PolicyDuration = model.PolicyDuration,
                    PolicyPaymentType = model.PolicyPaymentType,
                    Payment = model.Payment,
                    Budget = model.Budget,
                    CompanyId = model.CompanyId,
                    PhotoPath = folder
                };
                context.Add(policy);
                context.SaveChanges();
                TempData["success"] = "Policy Created Successfully";
                return RedirectToAction("ListPolicy", "Policy");

            }

            return View(model);
        }

        [HttpGet]
       
        public IActionResult ListPolicy()
        {
            var policies = context.Policies.ToList();

            return View(policies);
        }
        [HttpGet]

        public IActionResult RequestList()
        {
            ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
            ViewData["CompanyId"] = new SelectList(context.Set<InsuranceCompanyModel>(), "CompanyId", "InsuranceCompanyName");
            var requests = context.Policy_Requests.ToList();

            return View(requests);
        }

        [HttpGet]
        public IActionResult EditPolicyRequest(int id)
        {
            ViewData["PolicyId"] = new SelectList(context.Set<PolicyModel>(), "PolicyId", "PolicyTitle");
            ViewData["CompanyId"] = new SelectList(context.Set<InsuranceCompanyModel>(), "CompanyId", "InsuranceCompanyName");
            var request = context.Policy_Requests.Find(id);
            if (request == null)
            {
                return View("NotFound");
            }
            PolicyRequestModel model = new PolicyRequestModel
            {
                UserName = request.UserName,
                FnameLname = request.FnameLname,
                PolicyId = request.PolicyId,
                CompId = request.CompId,
                Reason = request.Reason,
                PhotoPath = request.PhotoPath,
                Date = request.Date,
                Status = request.Status


            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditPolicyRequest(PolicyRequestModel model)
        {

            PolicyRequestModel request = context.Policy_Requests.Find(model.Id);
            if (request == null)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {

                request.UserName = model.UserName;
                request.FnameLname = model.FnameLname;
                request.PolicyId = model.PolicyId;
                request.CompId = model.CompId;
                request.Reason = model.Reason;
                request.PhotoPath = model.PhotoPath;
                request.Date = model.Date;
                request.Status = model.Status;

                context.Policy_Requests.Update(request);
                context.SaveChanges();
                TempData["success"] = "Request Status updated";
                return RedirectToAction("RequestList");



            }
           
            return View(model);
        }





        [HttpGet]
    
        public IActionResult EditPolicy(int id)
        {
            ViewData["CompanyId"] = new SelectList(context.Set<InsuranceCompanyModel>(), "CompanyId", "InsuranceCompanyName");
            var policy = context.Policies.Find(id);
            if (policy == null)
            {
                return View("NotFound");
            }
            CreatePolicyViewModel model = new CreatePolicyViewModel
            {
                Id = policy.PolicyId,
                PolicyTitle = policy.PolicyTitle,
                PolicyDescription = policy.PolicyDescription,
                PolicyDuration = policy.PolicyDuration,
                PolicyPaymentType = policy.PolicyPaymentType,
                Payment = policy.Payment,
                Budget = policy.Budget,
                CompanyId = policy.CompanyId,
                PhotoPath = policy.PhotoPath
            };


            return View(model);
        }


        [HttpPost]
        public IActionResult EditPolicy(CreatePolicyViewModel model)
        {
            if (ModelState.IsValid)
            {


                PolicyModel policy = context.Policies.Find(model.Id);

                var folder = "";
                if (model.Photo != null)
                {

                    folder = @"images/Policies/";
                    folder += Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    var serverFolder = Path.Combine(webHost.WebRootPath, folder);
                    using (var fileStream = new FileStream(serverFolder, FileMode.Create))
                    {
                        model.Photo.CopyTo(fileStream);
                    }
                    policy.PhotoPath = folder;

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
                    policy.PhotoPath = model.PhotoPath;
                }



                //comp.InsuranceCompanyName = model.InsuranceCompanyName;
                policy.PolicyTitle = model.PolicyTitle;
                policy.PolicyDescription = model.PolicyDescription;
                policy.PolicyDuration = model.PolicyDuration;
                policy.PolicyPaymentType = model.PolicyPaymentType;
                policy.Payment = model.Payment;
                policy.Budget = model.Budget;
                policy.CompanyId = model.CompanyId;


                context.Policies.Update(policy);
                context.SaveChanges();
                TempData["success"] = "Policy Updated Successfully";
                return RedirectToAction("ListPolicy");

            }
            return View(model);
        }


    }
}
