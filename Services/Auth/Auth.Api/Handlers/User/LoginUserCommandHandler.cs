using Auth.Domain.Commands.User;
using Auth.Infrastructure.Identity.Entities;
using Auth.Infrastructure.Identity.Services;
using Infrastructure.Core.Commands;
using Infrastructure.Core.Entities;
using Infrastructure.Core.Extensions;
using Infrastructure.Core.Interfaces;

namespace Auth.Api.Handlers.User
{
    public class LoginUserCommandHandler : CommandHandlerBase, ICommandHandler<LoginUserCommand, TokenPack>
    {
        private readonly IUserManager _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(IUserManager userManager, IJwtFactory jwtFactory, ITokenService tokenService)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _tokenService = tokenService;
        }

        public Result<TokenPack> Handle(LoginUserCommand command)
        {
            var user = _userManager.FindByEmail(command.Email);
            var isValid = user.IsExists() && _userManager.ValidatePassword(user, command.Password);

            if (!isValid)
            {
                State.Add("User", "Invalid data.");
                return Nullable<TokenPack>();
            }

            var pack = _jwtFactory.BuildTokenPack(user!);

            _tokenService.SetRefreshToken(user!, pack.RefreshToken);

            return Result(pack);
        }
    }
}