using Auth.Domain.Aggregates.UserAggregate;
using Auth.Domain.Interfaces.Repositories;
using Common.Services;
using Common.Structs;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Identity.Services;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;

    public UserManager(IUserRepository userRepository, IHashService hashService)
    {
        _userRepository = userRepository;
        _hashService = hashService;
    }

    public User? FindByEmail(string email)
    {
        return _userRepository.GetBy(email)
            .Include(x => x.Credentials)
            .ThenInclude(x => x.Role)
            .FirstOrDefault();
    }

    public bool ValidatePassword(User user, string password)
    {
        return _hashService.Validate(password, user.Password);
    }

    public void CreateNew(User user, Personal personal, string password)
    {
        var hashed = _hashService.Generate(password);

        user.SetPassword(hashed);
        user.SetPersonal(personal);
        user.AddCredential(RoleGuids.User);

        _userRepository.Add(user);
        _userRepository.UnitOfWork.SaveChanges();
    }
}