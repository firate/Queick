namespace Appointment.API.DTOs;

public record AppointmentCreateDto(
    long CustomerId,
    long LocationId,
    long EmployeeId,
    string Description,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate);