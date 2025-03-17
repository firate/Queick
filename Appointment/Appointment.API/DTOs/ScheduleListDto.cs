namespace Appointment.API.DTOs;

public record ScheduleListDto(
    long CustomerId,
    long LocationId,
    long EmployeeId,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate);