using Core.Entities;

namespace Auth.Domain.Aggregates.UserAggregate
{
    public class Role : Entity 
    {
        public string Caption { get; private set; }

        private readonly List<Credential> _credentials;
        public IReadOnlyCollection<Credential> Credentials => _credentials;

        public Role(string caption)
        {
            Caption = caption;

            _credentials = new List<Credential>();
        }

        public void AddCredential(Guid userId)
        {
            var credential = new Credential(userId, Id);
            
            _credentials.Add(credential);
        }
    }
}
