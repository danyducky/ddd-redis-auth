using Infrastructure.Core.Entities;

namespace Auth.Domain.Aggregates.UserAggregate
{
    public class Personal : ValueObject 
    {
        public string Firstname { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime BirthDate { get; set; }

        public Personal(string firstname, string surname, string patronymic, DateTime birthDate)
        {
            Firstname = firstname;
            Surname = surname;
            Patronymic = patronymic;
            BirthDate = birthDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Firstname;
            yield return Surname;
            yield return Patronymic;
            yield return BirthDate;
        }
    }
}
