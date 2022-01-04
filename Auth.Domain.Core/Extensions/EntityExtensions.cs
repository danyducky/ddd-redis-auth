namespace Core.Extensions
{
    public static class EntityExtensions
    {
        public static bool IsExists<T>(this T entity) => entity is not null;
        public static bool IsNull<T>(this T entity) => entity is null;
    }
}
