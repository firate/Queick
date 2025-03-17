namespace Appointment.API.DTOs;

public record ScheduleListDto(
    long LocationId,
    long EmployeeId,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate);