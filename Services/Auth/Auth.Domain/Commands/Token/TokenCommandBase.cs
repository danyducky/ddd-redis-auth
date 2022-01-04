using Infrastructure.Core.Commands;

namespace Auth.Domain.Commands.Token;

public class TokenCommandBase : CommandBase
{
    public string RefreshToken { get; set; }
}