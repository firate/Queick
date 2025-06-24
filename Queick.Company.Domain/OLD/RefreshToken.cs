using Queick.Company.Domain.Common;

namespace Queick.Company.Domain;

public class RefreshToken: Entity
{
    //public long Id { get; set; }
    public string Token { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAt { get; set; }
}