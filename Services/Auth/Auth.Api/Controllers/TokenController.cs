using Auth.Domain.Commands.Token;
using Auth.Infrastructure.Identity.Entities;
using FluentValidation;
using Infrastructure.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ICommandHandler<RefreshTokenCommand, TokenPack> _refreshTokenCommandHandler;
        private readonly ICommandHandler<RevokeTokenCommand> _revokeTokenCommandHandler;

        public TokenController(ICommandHandler<RefreshTokenCommand, TokenPack> refreshTokenCommandHandler,
            ICommandHandler<RevokeTokenCommand> revokeTokenCommandHandler)
        {
            _refreshTokenCommandHandler = refreshTokenCommandHandler;
            _revokeTokenCommandHandler = revokeTokenCommandHandler;
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromServices] IValidator<RefreshTokenCommand> validator)
        {
            var command = new RefreshTokenCommand
            {
                RefreshToken = Request.Cookies["Refresh"]
            };
            var result = validator.Validate(command);

            if (!result.IsValid) return BadRequest(result.Errors);

            var handleResult = _refreshTokenCommandHandler.Handle(command);

            if (!handleResult.Success) return BadRequest(result.Errors);

            Response.Cookies.Append("Refresh", handleResult.Entity!.RefreshToken, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(handleResult.Entity!.AccessToken);
        }

        [HttpDelete("revoke")]
        public IActionResult Revoke([FromServices] IValidator<RevokeTokenCommand> validator)
        {
            var command = new RevokeTokenCommand
            {
                RefreshToken = Request.Cookies["Refresh"]
            };
            var result = validator.Validate(command);

            if (!result.IsValid) return BadRequest(result.Errors);

            var handleResult = _revokeTokenCommandHandler.Handle(command);

            if (!handleResult.Success) return BadRequest(result.Errors);

            Response.Cookies.Delete("Refresh");

            return Ok();
        }
    }
}