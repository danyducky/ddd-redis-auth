using Auth.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Auth.Infrastructure
{
    public class AuthContextFactory : IDesignTimeDbContextFactory<AuthContext>
    {
        public AuthContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=tech_auth;Username=postgres;Password=1");

            return new AuthContext(optionsBuilder.Options);
        }
    }
}
