using Core.Entities;

namespace Core.Commands
{
    public abstract class CommandHandlerBase
    {
        /// <summary>
        /// Dictionary of errors by error name 
        /// </summary>
        private IDictionary<string, IDictionary<string, string>> Notifications =>
            new Dictionary<string, IDictionary<string, string>>()
            {
                {"errors", State}
            };

        protected readonly IDictionary<string, string> State = new Dictionary<string, string>();

        private bool IsSuccess => !State.Any();

        protected Result Result() => new Result(IsSuccess, Notifications);

        protected Result<T> Result<T>() => new Result<T>(IsSuccess, Notifications);
        protected Result<T> Nullable<T>() => new Result<T>(IsSuccess, Notifications);

        protected Result<T> Result<T>(T? entity) => new Result<T>(IsSuccess, Notifications, entity);
    }
}