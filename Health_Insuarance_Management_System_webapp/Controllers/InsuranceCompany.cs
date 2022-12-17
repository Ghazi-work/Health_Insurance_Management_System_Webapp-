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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient.Server;

namespace Health_Insuarance_Management_System_webapp.Controllers
{
    [Authorize(Roles = "Admin,IT")]
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
                using (var fileStream = new FileStream(serverFolder, FileMode.Create))
                {
                   file.CopyTo(fileStream);
                }
                //file.CopyTo(new FileStream(serverFolder, FileMode.Create));
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
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ListCompany()
        {
            var companies = context.Insurance_Companies.ToList();
            return View(companies);
        }


        [HttpGet]
        public IActionResult EditCompany(int id)
        {
            //var comp = context.Insurance_Companies.Find(id);
            InsuranceCompanyModel comp = context.Insurance_Companies.Find(id);
            if (comp == null)
            {
                return View("NotFound");

            }
            InsuranceCompanyViewModel model = new InsuranceCompanyViewModel
            {
                Id = comp.CompanyId,
                InsuranceCompanyName = comp.InsuranceCompanyName,
                HeadOfficeAddress = comp.HeadOfficeAddress,
                HelplineNumber = comp.HelplineNumber,
                OfficialEmail = comp.OfficialEmail,
                ExtraInformation = comp.ExtraInformation,
                WebsiteUrl = comp.WebsiteUrl,
                existingPath = comp.PhotoPath,
            };
           
           
            return View(model);
        }


        [HttpPost]
        public IActionResult EditCompany(InsuranceCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {


                InsuranceCompanyModel comp = context.Insurance_Companies.Find(model.Id);
               
                var folder = "";
                if (model.Photo != null)
                {

                    folder = @"images/InsuranceCompany/";
                    folder += Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    var serverFolder = Path.Combine(webHost.WebRootPath, folder);
                    using (var fileStream = new FileStream(serverFolder, FileMode.Create))
                    {
                        model.Photo.CopyTo(fileStream);
                    }
                    comp.PhotoPath = folder;

                    if (model.existingPath != null)
                    {
                        var oldDirectory = Path.Combine(webHost.WebRootPath, model.existingPath);
                        if (System.IO.File.Exists(oldDirectory))
                        {
                            System.IO.File.Delete(oldDirectory);
                        }

                    }
                }
                else
                {
                    comp.PhotoPath = model.existingPath;
                }


              
            comp.InsuranceCompanyName = model.InsuranceCompanyName;
            comp.OfficialEmail = model.OfficialEmail;
            comp.HelplineNumber = model.HelplineNumber;
            comp.HeadOfficeAddress = model.HeadOfficeAddress;
            comp.ExtraInformation = model.ExtraInformation;
            comp.WebsiteUrl = model.WebsiteUrl;

                context.Insurance_Companies.Update(comp);
                context.SaveChanges();
               return RedirectToAction("ListCompany");
            
            }
            return View();
        }




    }
}
