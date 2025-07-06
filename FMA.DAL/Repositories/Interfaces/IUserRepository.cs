using FMA.DAL.Entities;

namespace FMA.DAL.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> FindByEmailAsync(string email);
}