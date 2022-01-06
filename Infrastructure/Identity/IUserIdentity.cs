namespace Infrastructure.Identity;

public interface IUserIdentity
{
    string GetUserIdentity();
    string GetUserEmail();
    IEnumerable<string> GetUserRoles();
}