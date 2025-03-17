using Appointment.Entity;

namespace AppointmentService;

public interface IAppointmentEntityService
{
    Task<AppointmentEntity?> GetById(long id);

    Task<AppointmentEntity> CreateAppointment(
        long customerId,
        long employeeId,
        long locationId,
        string description,
        DateTimeOffset startDate,
        DateTimeOffset endDate
    );
    Task<AppointmentEntity> UpdateAppointment(AppointmentEntity appointment);
    Task<bool> DeleteAppointment(AppointmentEntity appointment);
    
    
    
}