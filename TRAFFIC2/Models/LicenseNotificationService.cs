using Microsoft.Extensions.Hosting;
using MimeKit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MailKit.Net.Smtp;
using TRAFFIC2.Models;
using Microsoft.EntityFrameworkCore;

public class LicenseNotificationService : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private Timer _timer;
    private readonly ModelContext _context;

    public LicenseNotificationService(IServiceProvider serviceProvider , ModelContext context)
    {
        _serviceProvider = serviceProvider;
        _context = context;
    }


    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24)); // Run every 24 hours
        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            //var dbContext = scope.ServiceProvider.GetRequiredService<Appointment>();
            // Query your data to find approaching appointment dates
            //var approachingAppointments = dbContext[0]; 
            //.Where(a => a.AppointmentDate <= DateTime.Now.AddDays(7))
            //.ToList();
            var licensesDue = _context.Cars.Where(l => l.AppointmentDate <= DateTime.Now).ToList();

            foreach (var appointment in licensesDue)
            {
                SendAppointmentDateReminder(appointment);
            }
        }
    }

    private Userinformation GetUserbyId (decimal? id )
    {
        var user = _context.Userinformations.Where(u => u.UserId == id).FirstOrDefault();
        return user;
    }

    private void SendAppointmentDateReminder(Car car)
    {
        // Send email notifications using MailKit
        var message = new MimeMessage();
        Userinformation user = GetUserbyId(car.UserId);
        message.From.Add(new MailboxAddress("Your Name", "talaalqaissi45@gmail.com"));
        message.To.Add(new MailboxAddress(user.FirstName, user.Email));
        message.Subject = "Appointment Date Reminder";
        message.Body = new TextPart("plain")
        {
            Text = $"Your car with LicensePlate {car.LicensePlate} is approaching on {car.AppointmentDate}."
        };

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("talaalqaissi45@gmail.com", "");
            client.Send(message);
            client.Disconnect(true);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}




