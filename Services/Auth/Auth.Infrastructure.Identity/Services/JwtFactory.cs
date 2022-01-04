using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Auth.Domain.Aggregates.UserAggregate;
using Auth.Infrastructure.Identity.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Infrastructure.Identity.Services
{
    public class JwtFactory : IJwtFactory
    {
        private readonly IConfiguration _configuration;

        private int ExpirationTimeInMinutes => int.Parse(_configuration["Jwt:Expiration"]);

        private string Issuer => _configuration["Jwt:Issuer"];
        private string Audience => _configuration["Jwt:Audience"];
        private string Key => _configuration["Jwt:Key"];

        public JwtFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenPack BuildTokenPack(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Email),
                new(JwtRegisteredClaimNames.Name, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iss, Issuer),
                new(JwtRegisteredClaimNames.Aud, Audience)
            };

            claims.AddRange(
                user.Credentials.Select(
                    credential => new Claim(ClaimTypes.Role, credential.Role.Caption)
                )
            );

            var accessToken = BuildAccessToken(claims);
            var refreshToken = BuildRefreshToken();

            return new TokenPack(accessToken, refreshToken);
        }

        private string BuildAccessToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(Issuer, Issuer, claims,
                expires: DateTime.Now.AddMinutes(ExpirationTimeInMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private string BuildRefreshToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}