using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TRAFFIC2.Models;


[Authorize(Roles = "Admin")]
public class EmailController : Controller
{
    // Display the email sending form
    public IActionResult SendEmail()
    {
        var model = new EmailViewModel();
        return View(model);
    }

    // Handle the POST request for sending email
    [HttpPost]
    public IActionResult SendEmail(EmailViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Code to send email using your email library
            SendEmailToUsers(model);
            return RedirectToAction("talaalqaissi45@gmail.com"); // Redirect back to the email sending form
        }

        // If the model is not valid, return to the same view with validation errors
        return View(model);
    }

    private void SendEmailToUsers(EmailViewModel model)
    {
        // Code to send email using your chosen email library (e.g., MailKit)
        // Construct and send the email based on the model
    }
}
