using Appointment.Entity;
using Microsoft.EntityFrameworkCore;

namespace AppointmentData;
public class AppointmentDbContext:DbContext
{
    public AppointmentDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AppointmentEntity> Appointments { get; set; }

}
