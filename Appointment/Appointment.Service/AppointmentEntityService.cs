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

    public async Task<AppointmentEntity> CreateAppointment(AppointmentEntity appointment)
    {
        throw new NotImplementedException();
    }

    public async Task<AppointmentEntity> CreateAppointment(long customerId,
        long employeeId,
        string description,
        DateTimeOffset startTime,
        DateTimeOffset endTime,
        int locationId)
    {
        var customer =await _readOnlyContext.Customers.AsNoTracking()
            .Where(x => x.Id == customerId)
            .Select(x=>x.Id)
            .FirstOrDefaultAsync();

        if (customer <= 0)
        {
            throw new Exception("Customer not found");
        }

        var location = await _readOnlyContext.Locations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == locationId);

        if (location == null)
        {
            throw new Exception("Location not found");
        }
        
        var employee = await _readOnlyContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == employeeId);
        if (employee == null)
        {
            throw new Exception("Employee not found");
        }
        
        var existingAppointmentCheck = _dbContext.Appointments.AsNoTracking()
            .FirstOrDefaultAsync(x=>x.CustomerId == customerId && 
                                    x.LocationId == locationId &&
                                    x.StartDate == startTime && 
                                    x.EndDate == endTime);

        if (existingAppointmentCheck != null)
        {
            throw new Exception("Appointment already exists");
        }

        var appointment = new AppointmentEntity()
        {
            CustomerId = customerId,
            LocationId = locationId,
            EmployeeId = employeeId,
            Description = description,
            StartDate = startTime,
            EndDate = endTime
        };
        
        await _dbContext.Appointments.AddAsync(appointment);
        var isCreated = await _dbContext.SaveChangesAsync();
        if (isCreated > 0)
        {
            return appointment;
        }

        return null;
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