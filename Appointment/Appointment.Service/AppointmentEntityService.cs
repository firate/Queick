using Appointment.Entity;
using AppointmentData;
using Microsoft.EntityFrameworkCore;

namespace AppointmentService;

public class AppointmentEntityService : IAppointmentEntityService
{
    private readonly AppointmentReadOnlyContext _readOnlyContext;
    private readonly AppointmentDbContext _dbContext;

    public AppointmentEntityService(AppointmentReadOnlyContext readOnlyContext, AppointmentDbContext dbContext)
    {
        _readOnlyContext = readOnlyContext;
        _dbContext = dbContext;
    }

    public async Task<AppointmentEntity?> GetById(long id)
    {
        var appointment = await _dbContext.Appointments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        return appointment;
    }

    public async Task<AppointmentEntity> CreateAppointment(long customerId,
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
                                      x.EndDate == endDate);

        if (existingAppointmentCheck != null)
        {
            throw new Exception("Appointment already exists");
        }

        var appointment = new AppointmentEntity()
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

        return await Task.FromResult(new AppointmentEntity());
    }

    public async Task<AppointmentEntity> UpdateAppointment(AppointmentEntity appointment)
    {
        return await Task.FromResult(new AppointmentEntity());
    }

    public async Task<bool> DeleteAppointment(AppointmentEntity appointment)
    {
        return await Task.FromResult(false);
    }
}