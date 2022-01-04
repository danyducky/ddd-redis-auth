using Auth.Domain.Aggregates.UserAggregate;
using Auth.Infrastructure.Identity.Entities;

namespace Auth.Infrastructure.Identity.Services
{
    public interface IJwtFactory
    {
        TokenPack BuildTokenPack(User user);
    }
}