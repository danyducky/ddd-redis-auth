namespace Auth.Infrastructure.Identity.Structs;

public struct RoleGuids
{
    public static Guid User => Guid.Parse(UserGuid);

    private const string UserGuid = "4e721121-cdcd-4809-840f-5c8148333386";
}