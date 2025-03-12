namespace Appointment.API.DTOs;

public record GetAppointmentListDto(long CompanyId, long LocationId, DateTimeOffset StartDate, DateTimeOffset EndDate);