using Appointment.Entity;

namespace Appointment.Service;

public interface IAppointmentEntityService
{
    Task<Entity.Appointment?> GetById(long id);

    Task<Entity.Appointment> CreateAppointment(
        long customerId,
        long employeeId,
        long locationId,
        string description,
        DateTimeOffset startDate,
        DateTimeOffset endDate
    );
    Task<Entity.Appointment> UpdateAppointment(
        long appointmentId,
        long locationId,
        long employeeId,
        string description,
        DateTimeOffset startDate,
        DateTimeOffset endDate);
    Task<bool> DeleteAppointment(long appointmentId);
    
    
    
}