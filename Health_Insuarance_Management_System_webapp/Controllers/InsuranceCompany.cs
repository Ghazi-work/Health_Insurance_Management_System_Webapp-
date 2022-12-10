using Health_Insuarance_Management_System_webapp.ViewModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using Health_Insuarance_Management_System_webapp.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Health_Insuarance_Management_System_webapp.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Health_Insuarance_Management_System_webapp.Controllers
{
    public class InsuranceCompany : Controller
    {
        private readonly IWebHostEnvironment webHost;
        private readonly ApplicationDbContext context;

        public InsuranceCompany(IWebHostEnvironment webHost, ApplicationDbContext context)
        {
           
            this.webHost = webHost;
            this.context = context;
        }





        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(InsuranceCompanyViewModel model, IFormFile file)
        {
            var folder = "";
            if (file != null)
            {
                folder = @"images/InsuranceCompany/";
                folder += Guid.NewGuid().ToString() + "_" + file.FileName;
                var serverFolder = Path.Combine(webHost.WebRootPath, folder);

                file.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }
            if (ModelState.IsValid)
            {
                InsuranceCompanyModel company2 = new InsuranceCompanyModel();
                company2.InsuranceCompanyName = model.InsuranceCompanyName;
                company2.OfficialEmail = model.OfficialEmail;
                company2.HelplineNumber = model.HelplineNumber;
                company2.HeadOfficeAddress = model.HeadOfficeAddress;
                company2.ExtraInformation = model.ExtraInformation;
                company2.WebsiteUrl = model.WebsiteUrl;
                company2.PhotoPath = folder;


  
                context.Add(company2);
                context.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ListCompany()
        {
            var companies = context.Insurance_Companies.ToList();
            return View(companies);
        }

    }
}
