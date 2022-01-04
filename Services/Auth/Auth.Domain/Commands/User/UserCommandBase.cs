using Infrastructure.Core.Commands;

namespace Auth.Domain.Commands.User
{
    public abstract class UserCommandBase : CommandBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}