using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IUserRepository: IBaseRepository<User>
{
    Task<User> GetUserWithPermissionsAsync(Guid userId);
    Task<bool> UserExistsAsync(string username, string email);
}

