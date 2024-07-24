using Core.Entities;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IAuthService
    {
        Task<User> GetUser(string token);
    }
}