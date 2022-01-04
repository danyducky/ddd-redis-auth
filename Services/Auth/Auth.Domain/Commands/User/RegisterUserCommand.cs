namespace Auth.Domain.Commands.User
{
    public class RegisterUserCommand : UserCommandBase
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
