using Auth.Domain.Aggregates.UserAggregate;

namespace Auth.Infrastructure.Identity.Services;

public interface IUserManager
{
    User? FindByEmail(string email);
    bool ValidatePassword(User user, string password);
    void CreateNew(User user, Personal personal, string password);
}