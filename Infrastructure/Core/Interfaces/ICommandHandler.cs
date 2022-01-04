using Infrastructure.Core.Commands;
using Infrastructure.Core.Entities;

namespace Infrastructure.Core.Interfaces
{
    public interface ICommandHandler<TCommand>
        where TCommand : CommandBase
    {
        Result Handle(TCommand command);
    }

    public interface ICommandHandler<TCommand, TEntity>
        where TCommand : CommandBase
        where TEntity : class
    {
        Result<TEntity> Handle(TCommand command);
    }
}