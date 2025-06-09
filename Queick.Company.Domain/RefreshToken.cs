namespace Queick.Company.Domain;

public class RefreshToken
{
    public long Id { get; set; }
    public string Token { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAt { get; set; }
}