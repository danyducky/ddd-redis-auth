namespace Common.Services;

public interface IHashService
{
   string Generate(string plain);
   bool Validate(string plain, string hashed);
}