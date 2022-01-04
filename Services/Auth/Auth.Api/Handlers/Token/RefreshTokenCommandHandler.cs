using Auth.Domain.Commands.Token;
using Auth.Infrastructure.Identity.Entities;
using Infrastructure.Core.Commands;
using Infrastructure.Core.Entities;
using Infrastructure.Core.Interfaces;

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