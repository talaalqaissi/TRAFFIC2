using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Oracle.ManagedDataAccess.Client;
using SelectPdf;
using System.Net;
using System.Security.Claims;
using TRAFFIC2.Models;

namespace TRAFFIC2.Controllers
{
    //[Route("pdf")]
    public class UserController : Controller
    {


        private readonly Models.ModelContext _context;
        public UserController(Models.ModelContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            ViewBag.User = HttpContext.Session.GetInt32("UserId");
            ViewBag.ViolationsCount = _context.Violations.Count();

            return View();


        }
        //return ControllerContext.MyDisplayRouteInfo(id);
        public IActionResult userindex()
        {
            ViewBag.ViolationsCount = _context.Violations.Count();

            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult SubmitTestimonial()
        {
            var model = new Testimonial(); 
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitTestimonialAsync(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                testimonial.Status = "Pending"; 
              
                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction("Success","User" );
            }
            return View(testimonial);
        }



        public IActionResult AddCar(Car car)
    {
            ViewData["UserId"] = new SelectList(_context.Logins, "UserId", "UserId", car.UserId);

            if (ModelState.IsValid)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                car.UserId = userId;
            }

            _context.Cars.Add(car);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

            return View(car);

    }

        



        public IActionResult EditeProfile()
        {
            Userinformation userinformation = GetUserInformation();
            return View(userinformation);
        }

        private Userinformation GetUserInformation()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return _context.Userinformations.FirstOrDefault(u => u.UserId == userId);
            }
            return null;
        }

        [HttpPost]
        public IActionResult EditeProfile(Userinformation userinformation, Login Login)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                    if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                    {
                        userinformation.UserId = userId;
                        Login.UserId = userId;


                        // Check if a user with the same ID exists
                        var existingUser = _context.Userinformations.FirstOrDefault(u => u.UserId == userId);
                        if (existingUser != null)
                        {
                            // Update the existing user's properties
                            existingUser.UserId = (decimal)Login.UserId;
                            existingUser.FirstName = userinformation.FirstName;
                            existingUser.Email = userinformation.Email;
                            existingUser.Address = userinformation.Address;
                            existingUser.DateOfBirth = userinformation.DateOfBirth;

                            // Attach the entity as modified to the context
                            _context.Entry(existingUser).State = EntityState.Modified;
                        }
                        else
                        {
                            // Insert a new user if they don't exist
                            _context.Userinformations.Add(userinformation);
                        }

                        _context.SaveChanges(); // Save changes to the database

                        return RedirectToAction("Index");
                    }
                }
                catch (OracleException ex)
                {
                    // Handle the unique constraint violation
                    ModelState.AddModelError("CustomError", "A unique constraint violation occurred.");
                }
            }

            return View(userinformation);
        }

    }


    //public IActionResult EditeProfile(Userinformation userinformation)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    //        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
    //        {
    //            userinformation.UserId = userId;
    //        }

    //        _context.Userinformations.Add(userinformation);
    //        _context.SaveChanges();

    //    }

    //    return View("EditeProfile");

    //}

    //[Route("Website")]
    //public async Task <IActionResult> WebsiteAsync()
    //{

    //    var Desktopview = new HtmlToPdf();
    //    Desktopview.Options.WebPageWidth = 1920;

    //    var Pdf = (Desktopview.ConvertUrl("http://www.Payment.com"));
    //    var pdfBytes = Pdf.Save();
    //    return File(pdfBytes, "Application/pdf");
}


                    //}

                


    
    
    
    
