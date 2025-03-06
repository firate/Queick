using Appointment.Entity;

namespace Service;

public interface IAppointmentService
{
    Task<AppointmentEntity?> GetById(long id);
    Task<AppointmentEntity> CreateAppointment(AppointmentEntity appointment);
    Task<AppointmentEntity> UpdateAppointment(AppointmentEntity appointment);
    Task<bool> DeleteAppointment(AppointmentEntity appointment);
    
    
    
}