using Auth.Domain.Commands.Token;
using Auth.Infrastructure.Identity.Services;
using Infrastructure.Core.Commands;
using Infrastructure.Core.Entities;
using Infrastructure.Core.Interfaces;

namespace Auth.Api.Handlers.Token;

public class RevokeTokenCommandHandler : CommandHandlerBase, ICommandHandler<RevokeTokenCommand>
{
    private readonly ITokenService _tokenService;

    public RevokeTokenCommandHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public Result Handle(RevokeTokenCommand command)
    {
        _tokenService.Forget(command.RefreshToken);

        return Result();
    }
}