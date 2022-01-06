using Auth.Domain.Commands.Token;
using Auth.Infrastructure.Identity.Entities;
using Auth.Infrastructure.Identity.Services;
using Infrastructure.Core.Commands;
using Infrastructure.Core.Entities;
using Infrastructure.Core.Interfaces;
using Infrastructure.Identity;

namespace Auth.Api.Handlers.Token;

public class RefreshTokenCommandHandler : CommandHandlerBase, ICommandHandler<RefreshTokenCommand, TokenPack>
{
    private readonly IUserIdentity _userIdentity;
    private readonly IUserManager _userManager;
    private readonly ITokenService _tokenService;
    private readonly IJwtFactory _jwtFactory;

    public RefreshTokenCommandHandler(IUserIdentity userIdentity, IUserManager userManager, ITokenService tokenService,
        IJwtFactory jwtFactory)
    {
        _userIdentity = userIdentity;
        _userManager = userManager;
        _tokenService = tokenService;
        _jwtFactory = jwtFactory;
    }

    public Result<TokenPack> Handle(RefreshTokenCommand command)
    {
        var userEmail = _userIdentity.GetUserEmail();
        var user = _userManager.FindByEmail(userEmail);

        var payload = _tokenService.Get(command.RefreshToken);

        if (payload == null)
        {
            State.Add("Refresh token", "Not found.");
            return Nullable<TokenPack>();
        }

        var pack = _jwtFactory.BuildTokenPack(user!);

        _tokenService.Set(pack.Payload);

        return Result(pack);
    }
}