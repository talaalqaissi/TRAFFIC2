using Microsoft.EntityFrameworkCore;

namespace TRAFFIC2.Models
{
    public class YourDbContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
    }

    public class Appointment
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime AppointmentDate { get; set; }

    }

}
