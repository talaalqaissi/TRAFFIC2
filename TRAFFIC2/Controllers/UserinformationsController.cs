using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TRAFFIC2.Models;

namespace TRAFFIC2.Controllers
{
    public class UserinformationsController : Controller
    {
        private readonly ModelContext _context;

        public UserinformationsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Userinformations
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Userinformations;
            return View(await modelContext.ToListAsync());
        }

        // GET: Userinformations/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Userinformations == null)
            {
                return NotFound();
            }

            var userinformation = await _context.Userinformations
                //.Include(u => u.UserId)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userinformation == null)
            {
                return NotFound();
            }

            return View(userinformation);
        }

        // GET: Userinformations/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Logins, "UserId", "UserId");
            return View();
        }

        // POST: Userinformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FirstName,LastName,Email,Address,DateOfBirth")] Userinformation userinformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userinformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Logins, "UserId", "UserId", userinformation.UserId);
            return View(userinformation);
        }

        // GET: Userinformations/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Userinformations == null)
            {
                return NotFound();
            }

            var userinformation = await _context.Userinformations.FindAsync(id);
            if (userinformation == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Logins, "UserId", "UserId", userinformation.UserId);
            return View(userinformation);
        }

        // POST: Userinformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("UserId,FirstName,LastName,Email,Address,DateOfBirth")] Userinformation userinformation)
        {
            if (id != userinformation.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userinformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserinformationExists(userinformation.UserId))
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
            ViewData["UserId"] = new SelectList(_context.Logins, "UserId", "UserId", userinformation.UserId);
            return View(userinformation);
        }

        // GET: Userinformations/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Userinformations == null)
            {
                return NotFound();
            }

            var userinformation = await _context.Userinformations
                .Include(u => u.UserId)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userinformation == null)
            {
                return NotFound();
            }

            return View(userinformation);
        }

        // POST: Userinformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Userinformations == null)
            {
                return Problem("Entity set 'ModelContext.Userinformations'  is null.");
            }
            var userinformation = await _context.Userinformations.FindAsync(id);
            if (userinformation != null)
            {
                _context.Userinformations.Remove(userinformation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserinformationExists(decimal id)
        {
          return (_context.Userinformations?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
