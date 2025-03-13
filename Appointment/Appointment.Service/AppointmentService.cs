using Appointment.Entity;
using AppointmentData;
using Microsoft.EntityFrameworkCore;

namespace Service;

public class AppointmentService : IAppointmentService
{
    private readonly AppointmentReadOnlyContext _readOnlyContext;
    private readonly AppointmentDbContext _dbContext;

    public AppointmentService(AppointmentReadOnlyContext readOnlyContext, AppointmentDbContext dbContext)
    {
        _readOnlyContext = readOnlyContext;
        _dbContext = dbContext;
    }

    public async Task GetTemplate(int locationId, DateTimeOffset startTime, DateTimeOffset endTime, string templateName)
    {
        
    }


    public async Task<AppointmentEntity?> GetById(long id)
    {
        var appointment = await _dbContext.Appointments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        return appointment;
    }

    public async Task<AppointmentEntity> CreateAppointment(AppointmentEntity appointment)
    {
        throw new NotImplementedException();
    }

    // public long CustomerId { get; set; }
    // public string Name { get; set; }
    // public string Description { get; set; }
    // public DateTimeOffset StartDate { get; set; }
    // public DateTimeOffset EndDate { get; set; }
    // public Location Location { get; set; }
    // public int LocationId { get; set; }

    public async Task<AppointmentEntity> CreateAppointment(long customerId,
        string description,
        DateTimeOffset startTime,
        DateTimeOffset endTime,
        int locationId)
    {
        var customer = _readOnlyContext.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == customerId);

        if (customer == null)
        {
            throw new Exception("Customer not found");
        }

        var location = _readOnlyContext.Locations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == locationId);

        if (location == null)
        {
            throw new Exception("Location not found");
        }

        // var existingAppointmentCheck = _dbContext.Appointments.AsNoTracking().FirstOrDefaultAsync(
        //     x=>x.CustomerId == customerId && x.LocationId == locationId);
        //

        throw new NotImplementedException();
    }

    public async Task<AppointmentEntity> UpdateAppointment(AppointmentEntity appointment)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAppointment(AppointmentEntity appointment)
    {
        throw new NotImplementedException();
    }
}