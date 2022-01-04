using Core.Entities;

namespace Core.Interfaces;

public interface IActionHandler
{
    Result Handle();
}