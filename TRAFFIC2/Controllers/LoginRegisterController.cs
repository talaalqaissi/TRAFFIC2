using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TRAFFIC2.Models;

namespace TRAFFIC2.Controllers
{
    public class LoginRegisterController : Controller
    { 
        private readonly ModelContext _context;

        public LoginRegisterController(ModelContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
		public IActionResult Login([Bind("Username", "Password")] Login login)
		{
			var auth = _context.Logins.Where(x => x.Username == login.Username && x.Password == login.Password).SingleOrDefault();
            if ( auth != null)
            {
                switch(auth.RoleId)
                {
                    case 1:

						HttpContext.Session.SetString("AdminName", "Tala");
                        return RedirectToAction("Index", "Admin");



                    case 2:
                        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                        {

                            HttpContext.Session.SetInt32("UserId", userId);
                        }
                        return RedirectToAction("Index", "User");
                        //case 2:
                        //    if (auth.UserId.HasValue)
                        //    {
                        //        HttpContext.Session.SetInt32("UserId", (int)auth.UserId.Value);

                        //    }
                        //    return RedirectToAction("Index", "User");
                }
            }



            return RedirectToAction("Login");
        }
       

		// POST: Userinformations/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserId,FirstName,LastName,Email,Address,DateOfBirth")] Userinformation userinformation, string Username,string Password, decimal? v)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userinformation);
                await _context.SaveChangesAsync();

                Login login = new Models.Login
                {
                    Username = Username,
                    Password = Password,
                    
                };
                //login.UserId = User.UserId;
                login.RoleId = 2;

                _context.Add(login);
                await _context.SaveChangesAsync();

				return RedirectToAction("Index", "User");
			}
            ViewData["UserId"] = new SelectList(_context.Logins, "UserId", "UserId", userinformation.UserId);
            return View(userinformation);

            


        }



    }
}
    


