using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Health_Insuarance_Management_System_webapp.DataAccess;
using Health_Insuarance_Management_System_webapp.Models;
using Microsoft.AspNetCore.Authorization;

namespace Health_Insuarance_Management_System_webapp.Controllers
{
    [Authorize]
    public class InsuranceCompanyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsuranceCompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InsuranceCompany
        public async Task<IActionResult> Index()
        {
            return View(await _context.Insurance_Companies.ToListAsync());
        }

        // GET: InsuranceCompany/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceCompanyModel = await _context.Insurance_Companies
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (insuranceCompanyModel == null)
            {
                return NotFound();
            }

            return View(insuranceCompanyModel);
        }

        // GET: InsuranceCompany/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InsuranceCompany/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,InsuranceCompanyName,HeadOfficeAddress,HelplineNumber,ExtraInformation,OfficialEmail,WebsiteUrl")] InsuranceCompanyModel insuranceCompanyModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceCompanyModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceCompanyModel);
        }

        // GET: InsuranceCompany/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceCompanyModel = await _context.Insurance_Companies.FindAsync(id);
            if (insuranceCompanyModel == null)
            {
                return NotFound();
            }
            return View(insuranceCompanyModel);
        }

        // POST: InsuranceCompany/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,InsuranceCompanyName,HeadOfficeAddress,HelplineNumber,ExtraInformation,OfficialEmail,WebsiteUrl")] InsuranceCompanyModel insuranceCompanyModel)
        {
            if (id != insuranceCompanyModel.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceCompanyModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceCompanyModelExists(insuranceCompanyModel.CompanyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceCompanyModel);
        }

        // GET: InsuranceCompany/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceCompanyModel = await _context.Insurance_Companies
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (insuranceCompanyModel == null)
            {
                return NotFound();
            }

            return View(insuranceCompanyModel);
        }

        // POST: InsuranceCompany/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuranceCompanyModel = await _context.Insurance_Companies.FindAsync(id);
            _context.Insurance_Companies.Remove(insuranceCompanyModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceCompanyModelExists(int id)
        {
            return _context.Insurance_Companies.Any(e => e.CompanyId == id);
        }
    }
}
