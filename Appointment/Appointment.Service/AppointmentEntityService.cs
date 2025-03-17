using Appointment.Entity;
using Appointment.Data;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Service;

public class AppointmentEntityService : IAppointmentEntityService
{
    private readonly AppointmentReadOnlyContext _readOnlyContext;
    private readonly AppointmentDbContext _dbContext;

    public AppointmentEntityService(AppointmentReadOnlyContext readOnlyContext, AppointmentDbContext dbContext)
    {
        _readOnlyContext = readOnlyContext;
        _dbContext = dbContext;
    }

    public async Task<Entity.Appointment?> GetById(long id)
    {
        var appointment = await _dbContext.Appointments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        return appointment;
    }

    public async Task<Entity.Appointment> CreateAppointment(long customerId,
        long employeeId,
        long locationId,
        string description,
        DateTimeOffset startDate,
        DateTimeOffset endDate)
    {
        var cusId = await _readOnlyContext.Customers.AsNoTracking()
            .Where(x => x.Id == customerId)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        if (cusId <= 0)
        {
            throw new Exception("Customer not found");
        }

        var locId = await _readOnlyContext.Locations.AsNoTracking()
            .Where(x => x.Id == locationId)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        if (locId <= 0)
        {
            throw new Exception("Location not found");
        }

        var empId = await _readOnlyContext.Employees.AsNoTracking()
            .Where(x => x.Id == employeeId)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        
        if (empId <= 0)
        {
            throw new Exception("Employee not found");
        }

        var existingAppointmentCheck = _dbContext.Appointments.AsNoTracking()
            .FirstOrDefaultAsync(x => x.CustomerId == cusId &&
                                      x.LocationId == locationId &&
                                      x.StartDate == startDate &&
                                      x.EndDate == endDate &&
                                      !x.IsDeleted);

        if (existingAppointmentCheck != null)
        {
            throw new Exception("Appointment already exists");
        }

        var appointment = new Entity.Appointment()
        {
            CustomerId = cusId,
            LocationId = locationId,
            EmployeeId = employeeId,
            Description = description,
            StartDate = startDate,
            EndDate = endDate
        };

        await _dbContext.Appointments.AddAsync(appointment);
        var isCreated = await _dbContext.SaveChangesAsync();
        if (isCreated > 0)
        {
            return appointment;
        }

        return await Task.FromResult(new Entity.Appointment());
    }

    public async Task<Entity.Appointment> UpdateAppointment(long appointmentId, long locationId, long employeeId, string description, DateTimeOffset startDate,
        DateTimeOffset endDate)
    {
        var  appointment = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.Id == appointmentId);
        if (appointment == null)
        {
            throw new Exception("Appointment not found");
        }
        
        appointment.LocationId = locationId;
        appointment.EmployeeId = employeeId;
        appointment.Description = description;
        appointment.StartDate = startDate;
        appointment.EndDate = endDate;
        
        var isUpdated = await _dbContext.SaveChangesAsync();

        if (isUpdated > 0)
        {
            return appointment;
        }
        
        return await Task.FromResult(new Entity.Appointment());
    }

    public async Task<bool> DeleteAppointment(long appointmentId)
    {
        var  appointment = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.Id == appointmentId);
        if (appointment == null)
        {
            throw new Exception("Appointment not found");
        }
        
        appointment.IsDeleted = true;
        var isUpdated = await _dbContext.SaveChangesAsync();
        
        return isUpdated > 0;
    }
}