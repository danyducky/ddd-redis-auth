using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces;

public interface IRepository<T>
    where T : IAggregateRoot
{
    DbContext UnitOfWork { get; }
}