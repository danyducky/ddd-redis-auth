using Auth.Domain.Commands.User;
using Auth.Infrastructure.Identity.Entities;
using Infrastructure.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICommandHandler<RegisterUserCommand> _registerUserCommandHandler;
        private readonly ICommandHandler<LoginUserCommand, TokenPack> _loginUserCommandHandler;

        public UserController(ICommandHandler<RegisterUserCommand> registerUserCommandHandler,
            ICommandHandler<LoginUserCommand, TokenPack> loginUserCommandHandler)
        {
            _registerUserCommandHandler = registerUserCommandHandler;
            _loginUserCommandHandler = loginUserCommandHandler;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterUserCommand command)
        {
            var result = _registerUserCommandHandler.Handle(command);

            if (result.Success) return Ok(command);

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserCommand command)
        {
            var result = _loginUserCommandHandler.Handle(command);

            if (!result.Success) return BadRequest(result.Errors);

            Response.Cookies.Append("Refresh", result.Entity!.Payload.RefreshToken, new CookieOptions()
            {
                HttpOnly = true
            });
            return Ok(result.Entity!.AccessToken);
        }
    }
}