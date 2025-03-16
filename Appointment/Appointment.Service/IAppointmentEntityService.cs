using Appointment.Entity;

namespace AppointmentService;

public interface IAppointmentEntityService
{
    Task<AppointmentEntity?> GetById(long id);
    Task<AppointmentEntity> CreateAppointment(AppointmentEntity appointment);
    Task<AppointmentEntity> UpdateAppointment(AppointmentEntity appointment);
    Task<bool> DeleteAppointment(AppointmentEntity appointment);
    
    
    
}