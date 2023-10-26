using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using TRAFFIC2.Models;

namespace TRAFFIC2.Controllers
{
    public class HomePagesController : Controller
    {
        private readonly ModelContext _context;

        public HomePagesController(ModelContext context)
        {
            _context = context;
        }

        // GET: HomePages
        public async Task<IActionResult> Index()
        {
            ViewBag.User = HttpContext.Session.GetInt32("UserId");
            var modelContext = _context.HomePages.Include(h => h.About).Include(h => h.Contact).Include(h => h.Image).Include(h => h.Testimonial);
            return View(await modelContext.ToListAsync());
        }

        public async Task<IActionResult> UserHome()
        {
            ViewBag.User = HttpContext.Session.GetInt32("UserId");
			var homePageId = 1;
			var images = _context.Images
	        .Where(img => img.HomePageId == homePageId)
	        .ToList();
			var acceptedTestimonials = _context.Testimonials
             .Where(t => t.Status == "Pending" || t.Status == "Accepted")
                .ToList();
			var tuple = Tuple.Create<IEnumerable<Image>, IEnumerable<Testimonial>>(images, acceptedTestimonials);

			return View("UserHome", tuple);


		}

		public IActionResult UserAbout()
        {
           
            var aboutUsData = _context.AboutUs.ToList(); 

            
            var aboutUsViewModel = aboutUsData.Select(item => new AboutU
            {
                AboutId = item.AboutId,
                Title = item.Title,
                Content = item.Content
                
            }).ToList();
			var homePageId = 1;
            var AboutU = _context.AboutUs;
			var images = _context.Images
			.Where(img => img.HomePageId == homePageId)
			.ToList();
			
			var tuple = Tuple.Create<IEnumerable<Image>, IEnumerable<AboutU>>(images, AboutU);

			return View("UserAbout", tuple);

			
        }

        //public IActionResult ContactUsPage()
        //{
        //    ContactU contactU = _context.ContactUs.FirstOrDefault();

        //    return View(contactU);
        //}


        // GET: ContactUs1/Create
        

        public IActionResult ContactUsPage([Bind("Firstname", "PhoneNumber", "Address", "Email")] ContactU contactForm)
        {
            if (ModelState.IsValid)
            {
                var contactU = new ContactU
                {
                    Firstname = contactForm.Firstname,
                    Address = contactForm.Address,
                    Email = contactForm.Email,
                    PhoneNumber = contactForm.PhoneNumber


                };
               
                _context.Add(contactU);
                _context.SaveChanges();


            }

            return View("ContactUsPage", contactForm);
        }

        

        // GET: HomePages/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.HomePages == null)
            {
                return NotFound();
            }

            var homePage = await _context.HomePages
                .Include(h => h.About)
                .Include(h => h.Contact)
             
                .Include(h => h.Image)
                .Include(h => h.Testimonial)
                .FirstOrDefaultAsync(m => m.HomeId == id);
            if (homePage == null)
            {
                return NotFound();
            }

            return View(homePage);
        }

        // GET: HomePages/Create
        public IActionResult Create()
        {
            ViewData["AboutId"] = new SelectList(_context.AboutUs, "AboutId", "AboutId");
            ViewData["ContactId"] = new SelectList(_context.ContactUs, "ContactId", "ContactId");
            ViewData["ImageId"] = new SelectList(_context.Images, "ImageId", "ImageId");
            ViewData["TestimonialId"] = new SelectList(_context.Testimonials, "TestimonialId", "TestimonialId");
            return View();
        }

        // POST: HomePages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HomeId,ImageId,FooterId,Title,Content,AboutId,ContactId,TestimonialId")] HomePage homePage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homePage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AboutId"] = new SelectList(_context.AboutUs, "AboutId", "AboutId", homePage.AboutId);
            ViewData["ContactId"] = new SelectList(_context.ContactUs, "ContactId", "ContactId", homePage.ContactId);
            ViewData["ImageId"] = new SelectList(_context.Images, "ImageId", "ImageId", homePage.ImageId);
            ViewData["TestimonialId"] = new SelectList(_context.Testimonials, "TestimonialId", "TestimonialId", homePage.TestimonialId);
            return View(homePage);
        }

        // GET: HomePages/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.HomePages == null)
            {
                return NotFound();
            }

            var homePage = await _context.HomePages.FindAsync(id);
            if (homePage == null)
            {
                return NotFound();
            }
            ViewData["AboutId"] = new SelectList(_context.AboutUs, "AboutId", "AboutId", homePage.AboutId);
            ViewData["ContactId"] = new SelectList(_context.ContactUs, "ContactId", "ContactId", homePage.ContactId);
            
            ViewData["ImageId"] = new SelectList(_context.Images, "ImageId", "ImageId", homePage.ImageId);
            ViewData["TestimonialId"] = new SelectList(_context.Testimonials, "TestimonialId", "TestimonialId", homePage.TestimonialId);
            return View(homePage);
        }

        // POST: HomePages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("HomeId,ImageId,Title,Content,AboutId,ContactId,TestimonialId")] HomePage homePage)
        {
            if (id != homePage.HomeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homePage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomePageExists(homePage.HomeId))
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
            ViewData["AboutId"] = new SelectList(_context.AboutUs, "AboutId", "AboutId", homePage.AboutId);
            ViewData["ContactId"] = new SelectList(_context.ContactUs, "ContactId", "ContactId", homePage.ContactId);
            ViewData["ImageId"] = new SelectList(_context.Images, "ImageId", "ImageId", homePage.ImageId);
            ViewData["TestimonialId"] = new SelectList(_context.Testimonials, "TestimonialId", "TestimonialId", homePage.TestimonialId);
            return View(homePage);
        }

        // GET: HomePages/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.HomePages == null)
            {
                return NotFound();
            }

            var homePage = await _context.HomePages
                .Include(h => h.About)
                .Include(h => h.Contact)
                
                .Include(h => h.Image)
                .Include(h => h.Testimonial)
                .FirstOrDefaultAsync(m => m.HomeId == id);
            if (homePage == null)
            {
                return NotFound();
            }

            return View(homePage);
        }

        // POST: HomePages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.HomePages == null)
            {
                return Problem("Entity set 'ModelContext.HomePages'  is null.");
            }
            var homePage = await _context.HomePages.FindAsync(id);
            if (homePage != null)
            {
                _context.HomePages.Remove(homePage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomePageExists(decimal id)
        {
          return (_context.HomePages?.Any(e => e.HomeId == id)).GetValueOrDefault();
        }
    }
}
