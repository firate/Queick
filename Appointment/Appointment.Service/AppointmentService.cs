using Appointment.Entity;
using AppointmentData;

namespace Service;

public class AppointmentService: IAppointmentService
{
    private readonly AppointmentReadOnlyContext _readOnlyContext;
    private readonly AppointmentDbContext _context;
    
    public AppointmentService(AppointmentReadOnlyContext readOnlyContext, AppointmentDbContext context)
    {
        _readOnlyContext = readOnlyContext;
        _context = context;
    }


    public Task<AppointmentEntity> CreateAppointment(AppointmentEntity appointment)
    {
        throw new NotImplementedException();
    }

    public Task<AppointmentEntity> UpdateAppointment(AppointmentEntity appointment)
    {
        throw new NotImplementedException();
    }

    public Task<AppointmentEntity> DeleteAppointment(AppointmentEntity appointment)
    {
        throw new NotImplementedException();
    }
}