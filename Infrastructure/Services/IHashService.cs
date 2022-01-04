namespace Infrastructure.Services;

public interface IHashService
{
    public string Generate(string plain);
    public bool Validate(string plain, string hashed);
}