namespace Auth.Domain.Aggregates.UserAggregate
{
    public class Credential 
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        
        public User User { get; set; }
        public Role Role { get; set; }

        public Credential(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
