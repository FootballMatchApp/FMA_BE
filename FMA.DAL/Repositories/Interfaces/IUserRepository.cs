using FMA.DAL.Entities;

namespace FMA.DAL.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllUser();
    Task<User?> GetUserById(int id);
    Task<User> CreateUser(User user);
    Task<User> UpdateUser(User user);
    Task<bool> DeleteUser(int id);
}