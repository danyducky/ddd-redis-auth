using Auth.Domain.Commands.Token;
using Auth.Infrastructure.Identity.Entities;
using Core.Commands;
using Core.Entities;
using Core.Interfaces;

namespace Auth.Api.Handlers.Token;

public class RefreshTokenCommandHandler : CommandHandlerBase, ICommandHandler<RefreshTokenCommand, TokenPack>
{
    public RefreshTokenCommandHandler()
    {
    }

    public Result<TokenPack> Handle(RefreshTokenCommand command)
    {
        throw new NotImplementedException();
    }
}