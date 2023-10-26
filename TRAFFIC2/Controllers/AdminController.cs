using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TRAFFIC2.Models;

namespace TRAFFIC2.Controllers
{
    public class AdminController : Controller
    {
        private readonly Models.ModelContext _context;
        public AdminController(Models.ModelContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Name = HttpContext.Session.GetString("AdminName" );
            ViewBag.UserinformationsCount = _context.Userinformations.Count();
            ViewBag.CarsCount = _context.Cars.Count();

            return View();
        }

        public IActionResult AdminReviewPage()
        {
            var pendingTestimonials = _context.Testimonials.Where(t => t.Status == "Pending").ToList();
            return View(pendingTestimonials);
        }

        public IActionResult Done()
        {
            return View();
        }

        public IActionResult Accept(int id)
        {
            var testimonial = _context.Testimonials.FirstOrDefault(t => t.TestimonialId == id);
            if (testimonial != null)
            {
                testimonial.Status = "Accepted";
                _context.SaveChanges();
            }
            return RedirectToAction("Done", "Admin");
        }

        public IActionResult Reject(int id)
        {
            var testimonial = _context.Testimonials.FirstOrDefault(t => t.TestimonialId == id);
            if (testimonial != null)
            {
                testimonial.Status = "Rejected";
                _context.SaveChanges();
            }
            return RedirectToAction("Done", "Admin");
        }

       

        [HttpGet]
        public IActionResult Search()
        {
            var modelContext = _context.Violations.Include(u => u.User).ToList();
            return View(modelContext);
        }

        [HttpPost]
        public IActionResult Search(DateTime? startDate, DateTime? endDate)
        {
            var modelContext = _context.Violations.Include(u => u.User).AsQueryable(); // Ensure modelContext is IQueryable

            if (startDate != null)
            {
                modelContext = modelContext.Where(x => x.ViolationDate >= startDate);
            }

            if (endDate != null)
            {
                modelContext = modelContext.Where(x => x.ViolationDate <= endDate);
            }

            var result = modelContext.ToList();

            return View(result);
        }







        //public IActionResult MonthlyReport(int year, int month)
        //{
        //    // Filter events for the specified year and month
        //    var violation = _context.Violations
        //        .Where(e => e.ViolationDate.Year == year && e.ViolationDate.Month == month)
        //        .ToList();

        //    // Calculate the total for the month
        //    decimal total = violation.Sum(e => e.ViolationsPrice);

        //    // Pass the data to a view for display
        //    ViewBag.Year = year;
        //    ViewBag.Month = month;
        //    ViewBag.Total = total;

        //    return View();
        //}

        //public IActionResult AnnualReport(int year)
        //{
        //    // Filter events for the specified year
        //    var events = _context.Violations
        //        .Where(e => e.ViolationDate.Year == year)
        //        .ToList();

        //    // Calculate the total for the year
        //    decimal total = events.Sum(e => e.ViolationsPrice);

        //    // Pass the data to a view for display
        //    ViewBag.Year = year;
        //    ViewBag.Total = total;

        //    return View();
        //}




    }
}

