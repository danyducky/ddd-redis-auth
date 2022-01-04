using Auth.Domain.Aggregates.UserAggregate;
using Auth.Domain.Commands.User;
using Auth.Infrastructure.Identity.Services;
using Infrastructure.Core.Commands;
using Infrastructure.Core.Entities;
using Infrastructure.Core.Interfaces;

namespace Auth.Api.Handlers.User
{
    using User = Auth.Domain.Aggregates.UserAggregate.User;

    public class RegisterUserCommandHandler : CommandHandlerBase, ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserManager _userManager;

        public RegisterUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Result Handle(RegisterUserCommand command)
        {
            var user = new User(command.Email, DateTime.UtcNow);
            var personal = new Personal(command.Firstname, command.Surname, command.Patronymic, command.BirthDate);

            _userManager.CreateNew(user, personal, command.Password);

            return Result();
        }
    }
}