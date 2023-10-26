using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TRAFFIC2.Models;

namespace TRAFFIC2.Controllers
{
    public class ViolationsinquiriesController : Controller
    {
        private readonly ModelContext _context;

        public ViolationsinquiriesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Violationsinquiries
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Violationsinquiries.Include(v => v.Car);
            return View(await modelContext.ToListAsync());
        }

        // GET: Violationsinquiries/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Violationsinquiries == null)
            {
                return NotFound();
            }

            var violationsinquiry = await _context.Violationsinquiries
                .Include(v => v.Car)
                .FirstOrDefaultAsync(m => m.EnquiryId == id);
            if (violationsinquiry == null)
            {
                return NotFound();
            }

            return View(violationsinquiry);
        }

        // GET: Violationsinquiries/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId");
            return View();
        }

        // POST: Violationsinquiries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnquiryId,CarId")] Violationsinquiry violationsinquiry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(violationsinquiry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", violationsinquiry.CarId);
            return View(violationsinquiry);
        }

        // GET: Violationsinquiries/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Violationsinquiries == null)
            {
                return NotFound();
            }

            var violationsinquiry = await _context.Violationsinquiries.FindAsync(id);
            if (violationsinquiry == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", violationsinquiry.CarId);
            return View(violationsinquiry);
        }

        // POST: Violationsinquiries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("EnquiryId,CarId")] Violationsinquiry violationsinquiry)
        {
            if (id != violationsinquiry.EnquiryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(violationsinquiry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViolationsinquiryExists(violationsinquiry.EnquiryId))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", violationsinquiry.CarId);
            return View(violationsinquiry);
        }

        // GET: Violationsinquiries/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Violationsinquiries == null)
            {
                return NotFound();
            }

            var violationsinquiry = await _context.Violationsinquiries
                .Include(v => v.Car)
                .FirstOrDefaultAsync(m => m.EnquiryId == id);
            if (violationsinquiry == null)
            {
                return NotFound();
            }

            return View(violationsinquiry);
        }

        // POST: Violationsinquiries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Violationsinquiries == null)
            {
                return Problem("Entity set 'ModelContext.Violationsinquiries'  is null.");
            }
            var violationsinquiry = await _context.Violationsinquiries.FindAsync(id);
            if (violationsinquiry != null)
            {
                _context.Violationsinquiries.Remove(violationsinquiry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViolationsinquiryExists(decimal id)
        {
          return (_context.Violationsinquiries?.Any(e => e.EnquiryId == id)).GetValueOrDefault();
        }
    }
}
