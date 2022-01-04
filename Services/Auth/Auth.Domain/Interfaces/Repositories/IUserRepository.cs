using Auth.Domain.Aggregates.UserAggregate;
using Infrastructure.Core.Interfaces;

namespace Auth.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public void Add(User user);

        public IQueryable<User> GetBy(string email, string password);

        public IQueryable<User> GetBy(string email);
    }
}
