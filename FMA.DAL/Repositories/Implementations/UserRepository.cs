using FMA.DAL.Context;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FMA.DAL.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly FootballMatchAppContext _context;

    public UserRepository(FootballMatchAppContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUser()
    {
        return await _context.Users.Include(s => s.Role).ToListAsync();
    }
    public async Task<User?> GetUserById(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(s => s.UserId == id);
    }

    public async Task<User> CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUser(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(s => s.UserId == user.UserId);
        if (existingUser == null);
        existingUser.UserId = user.UserId;
        existingUser.Username = user.Username;
        existingUser.Email = user.Email;
        return existingUser;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(s => s.UserId == id);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }


}