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
    public class PolicyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PolicyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Policy
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Policies.Include(p => p.Company);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Policy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyModel = await _context.Policies
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.PolicyId == id);
            if (policyModel == null)
            {
                return NotFound();
            }

            return View(policyModel);
        }

        // GET: Policy/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Insurance_Companies, "CompanyId", "InsuranceCompanyName");
            return View();
        }

        // POST: Policy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolicyId,CompanyId,PolicyTitle,PolicyDescription,PolicyDuration,PolicyPaymentType,Payment")] PolicyModel policyModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policyModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Insurance_Companies, "CompanyId", "CompanyId", policyModel.CompanyId);
            return View(policyModel);
        }

        // GET: Policy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyModel = await _context.Policies.FindAsync(id);
            if (policyModel == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Insurance_Companies, "CompanyId", "CompanyId", policyModel.CompanyId);
            return View(policyModel);
        }

        // POST: Policy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PolicyId,CompanyId,PolicyTitle,PolicyDescription,PolicyDuration,PolicyPaymentType,Payment")] PolicyModel policyModel)
        {
            if (id != policyModel.PolicyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policyModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyModelExists(policyModel.PolicyId))
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
            ViewData["CompanyId"] = new SelectList(_context.Insurance_Companies, "CompanyId", "CompanyId", policyModel.CompanyId);
            return View(policyModel);
        }

        // GET: Policy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyModel = await _context.Policies
                .Include(p => p.Company)
                .FirstOrDefaultAsync(m => m.PolicyId == id);
            if (policyModel == null)
            {
                return NotFound();
            }

            return View(policyModel);
        }

        // POST: Policy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policyModel = await _context.Policies.FindAsync(id);
            _context.Policies.Remove(policyModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyModelExists(int id)
        {
            return _context.Policies.Any(e => e.PolicyId == id);
        }
    }
}
