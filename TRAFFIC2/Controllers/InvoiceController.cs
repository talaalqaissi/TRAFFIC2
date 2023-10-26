using DinkToPdf;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;

namespace TRAFFIC2.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendInvoiceAsync()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(" Tala", "talaalqaissi45@gmail.com"));
            message.To.Add(new MailboxAddress("tala", "talaalqaissi04@gmail.com"));
            message.Subject = "Invoice";

            var builder = new BodyBuilder();
            builder.TextBody = "Please find attached the invoice as a PDF.";
            builder.Attachments.Add("path_to_invoice.pdf");

            message.Body = builder.ToMessageBody();


            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("talaalqaissi04@gmail.com", 587, false);
                await client.AuthenticateAsync("talaalqaissi45@gmail.com", "Three237");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            return RedirectToAction("Index"); // Redirect to a suitable page.
        }

        public IActionResult GeneratePdf()
        {
            var converter = new BasicConverter(new PdfTools());
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
            PaperSize = PaperKind.A4,
            Orientation = Orientation.Portrait,
        },
                Objects = {
            new ObjectSettings()
            {
                PagesCount = true,
            HtmlContent = "<h1>Invoice</h1><p>Your invoice details go here</p>",
                WebSettings = { DefaultEncoding = "utf-8" },
            }
        }
            };

            var pdf = converter.Convert(doc);
            return File(pdf, "application/pdf", "invoice.pdf");
        }
    }
    
}
