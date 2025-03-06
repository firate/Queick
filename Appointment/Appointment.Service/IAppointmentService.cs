using Appointment.Entity;

namespace Service;

public interface IAppointmentService
{
    Task<AppointmentEntity> CreateAppointment(AppointmentEntity appointment);
    Task<AppointmentEntity> UpdateAppointment(AppointmentEntity appointment);
    Task<AppointmentEntity> DeleteAppointment(AppointmentEntity appointment);
    
    
}