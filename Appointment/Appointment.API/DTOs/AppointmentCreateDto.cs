public record AppointmentCreateDto
{
    public long CustomerId { get; set; }
    public int LocationId { get; set; }
    public string Description { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }

}
