using Auth.Domain.Commands.Token;
using Auth.Infrastructure.Identity.Services;
using Core.Commands;
using Core.Entities;
using Core.Interfaces;

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
        var user = new Domain.Aggregates.UserAggregate.User("", DateTime.Now);

        _tokenService.ForgetRefreshToken(user);

        return Result();
    }
}