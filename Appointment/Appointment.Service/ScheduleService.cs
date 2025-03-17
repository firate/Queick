using Appointment.Data;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Service;

public interface IScheduleService
{
    Task<ScheduleListModel> GetScheduleList(
        long locationId,
        long employeeId,
        DateTimeOffset startDate,
        DateTimeOffset endDate);
}


public class ScheduleService: IScheduleService
{
    private readonly AppointmentDbContext _context;
    private readonly AppointmentReadOnlyContext _readOnlyContext;
    public ScheduleService(AppointmentDbContext context, AppointmentReadOnlyContext readOnlyContext)
    {
        _context = context;
        _readOnlyContext = readOnlyContext;
    }

    public async Task<ScheduleListModel> GetScheduleList(
        long locationId,
        long employeeId,
        DateTimeOffset startDate,
        DateTimeOffset endDate)
    {
        if (startDate > endDate)
        {
            throw new ArgumentException("Start date must be after end date");
        }
        
        var templates =  _context.Schedules.Where(t => t.LocationId == locationId && t.EmployeeId == employeeId);
        
        // startDate ve endDate'in TimeZone'ları aynı olmalı, farklı ise hata dönelim. 
        var (startDateOffset, endDateOffset ) = (startDate.Offset, endDate.Offset);
        if (startDateOffset != endDateOffset)
        {
            throw new ArgumentException("Start date and end dates are not the same time zone.");
        }

        var (startDateUtc, startTimeSpan) = (startDate.ToUniversalTime(),TimeOnly.FromDateTime(startDate.ToUniversalTime().DateTime));
        var (endDateUtc,endTimeSpan) = (endDate.ToUniversalTime(), TimeOnly.FromDateTime(endDate.ToUniversalTime().DateTime) );
        
        
        templates = templates.Where(s =>
            s.DayOfWeek >= startDateUtc.DayOfWeek && s.DayOfWeek <= endDateUtc.DayOfWeek &&
            TimeOnly.FromTimeSpan(s.StartDate.ToTimeSpan().Add(TimeSpan.FromMinutes(s.UtcOffSetMinutes))) >= startTimeSpan &&
            TimeOnly.FromTimeSpan(s.EndDate.ToTimeSpan().Add(TimeSpan.FromMinutes(s.UtcOffSetMinutes))) <= endTimeSpan
        );
        
        
        var templateList = await templates.ToListAsync();

        // ilgili tarih aralıkları için mevcut appointmenler alınacak.
        // IsOccupied alanı bu modele göre doldurulacak.
        
        var appointments = _context.Appointments.AsQueryable();

        if (locationId != 0)
        {
            appointments = appointments.Where(a => a.LocationId == locationId);
        }

        if (employeeId != 0)
        {
            appointments = appointments.Where(a => a.EmployeeId == employeeId);
        }
        
        var appointmentList = await appointments.Where(x => x.StartDate >= startDateUtc && x.EndDate <= endDateUtc).ToListAsync();
        
        ScheduleListModel scheduleListModel = new();
        
        foreach (var template in templateList)
        {
            var isOccupied = appointments.Any(a =>
                a.StartDate.TimeOfDay == template.StartDate.ToTimeSpan() &&
                a.EndDate.TimeOfDay == template.EndDate.ToTimeSpan());

            scheduleListModel.Schedules.Add(
                new ScheduleDetailModel()
                {
                    StartTime = template.StartDate,
                    EndTime = template.EndDate,
                    IsOccupied = isOccupied,
                }
            );
        }
        
        return scheduleListModel;
    }
}

public record ScheduleDetailModel
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public bool IsOccupied { get; set; }
}

public record ScheduleListModel
{
    public List<ScheduleDetailModel> Schedules { get; set; } = new();
}

