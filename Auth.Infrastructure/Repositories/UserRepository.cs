using Auth.Domain.Aggregates.UserAggregate;
using Auth.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthContext _context;

        public DbContext UnitOfWork => _context;

        public UserRepository(AuthContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public IQueryable<User> GetBy(string email, string password)
        {
            return _context.Users.Where(x => x.Email == email && x.Password == password);
        }

        public IQueryable<User> GetBy(string email)
        {
            return _context.Users.Where(x => x.Email == email);
        }
    }
}