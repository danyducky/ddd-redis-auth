using Infrastructure.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Core.Interfaces;

public interface IRepository<T>
    where T : IAggregateRoot
{
    DbContext UnitOfWork { get; }
}