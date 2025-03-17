using Appointment.Entity;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Data;
public class AppointmentDbContext:DbContext
{
    public AppointmentDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Entity.Appointment> Appointments { get; set; }
    public DbSet<Schedule> Schedules { get; set; }

}
