namespace Appointment.API.DTOs;

public record AppointmentCreateDto(
    long CustomerId,
    long LocationId,
    string Description,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate);