using Infrastructure.Core.Entities;

namespace Auth.Domain.Aggregates.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public string Email { get; set; }

        public string Password { get; private set; }

        public DateTime RegistrationDate { get; private set; }

        public Personal Personal { get; private set; }

        private readonly List<Credential> _credentials;
        public IReadOnlyCollection<Credential> Credentials => _credentials;

        public User(string email, DateTime registrationDate)
        {
            Email = email;
            RegistrationDate = registrationDate;

            _credentials = new List<Credential>();
        }

        public void SetPersonal(string firstname, string surname, string patronymic, DateTime birthDate)
        {
            Personal = new Personal(firstname, surname, patronymic, birthDate);
        }

        public void SetPersonal(Personal personal)
        {
            Personal = personal;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void AddCredential(Guid roleId)
        {
            var credential = new Credential(Id, roleId);

            _credentials.Add(credential);
        }
    }
}