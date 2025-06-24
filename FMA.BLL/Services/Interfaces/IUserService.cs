using FMA.DAL.Entities;

namespace FMA.BLL.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllUser();
    Task<User?> GetUserById(int id);
    Task<User> CreateUser(User user);
    Task<User> UpdateUser(User user);
    Task<bool> DeleteUser(int id);
}