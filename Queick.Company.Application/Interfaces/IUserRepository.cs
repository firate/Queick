using Queick.Company.Domain;

namespace Queick.Company.Application.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(long id);
    Task<User> GetByUsernameAsync(string username);
    Task<User> GetByEmailAsync(string email);
    Task<User> GetUserWithPermissionsAsync(long userId);
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(long id);
    Task<bool> UserExistsAsync(string username, string email);
}

