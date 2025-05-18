namespace Appointment.API;

public class JwtSecurityOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double ExpireTime { get; set; }
    public string SecretKey { get; set; }
}