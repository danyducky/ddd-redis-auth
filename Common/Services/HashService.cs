using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;

namespace Common.Services;

public class HashService : IHashService
{
    private readonly IConfiguration _configuration;

    private byte[] Salt => Encoding.UTF8.GetBytes(_configuration["Config:Salt"]);

    public HashService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(string plain)
    {
        var bytes = KeyDerivation.Pbkdf2(
            password: plain,
            salt: Salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8);

        return Convert.ToBase64String(bytes);
    }

    public bool Validate(string plain, string hashed)
    {
        return Generate(plain) == hashed;
    }
}