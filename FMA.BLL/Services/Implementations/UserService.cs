using AutoMapper;
using FMA.BLL.Services.Interfaces;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;

namespace FMA.BLL.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;
    public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

    public async Task<List<User>> GetAllUser()
    {
        return await _repo.GetAllUser();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _repo.GetUserById(id);
    }

    public async Task<User> CreateUser(User user)
    {
        return await _repo.CreateUser(user);
    }

    public async Task<User> UpdateUser(User user)
    {
        return await _repo.UpdateUser(user);
    }

    public async Task<bool> DeleteUser(int id)
    {
        return await _repo.DeleteUser(id);
    }

    
}