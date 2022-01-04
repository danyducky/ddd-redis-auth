using System.Collections.Generic;

namespace Infrastructure.Core.Entities
{
    public class Result
    {
        public readonly IDictionary<string, IDictionary<string, string>> Errors;

        public readonly bool Success;

        public Result(bool success, IDictionary<string, IDictionary<string, string>> errors)
        {
            Success = success;
            Errors = errors;
        }
    }

    public class Result<T> : Result
    {
        public readonly T? Entity;

        public Result(bool success, IDictionary<string, IDictionary<string, string>> errors) : base(success, errors)
        {
        }

        public Result(bool success, IDictionary<string, IDictionary<string, string>> errors, T? entity) : base(success, errors)
        {
            Entity = entity;
        }
    }
}