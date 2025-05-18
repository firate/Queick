namespace Queick.Appointment.Application;

public interface IAppointmentEntityService
{
    Task<Queick.Appointment.Domain.Appointment?> GetById(long id);

    Task<Queick.Appointment.Domain.Appointment> CreateAppointment(
        long customerId,
        long employeeId,
        long locationId,
        string description,
        DateTimeOffset startDate,
        DateTimeOffset endDate
    );
    Task<Queick.Appointment.Domain.Appointment> UpdateAppointment(
        long appointmentId,
        long locationId,
        long employeeId,
        string description,
        DateTimeOffset startDate,
        DateTimeOffset endDate);
    Task<bool> DeleteAppointment(long appointmentId);
    
    
    
}