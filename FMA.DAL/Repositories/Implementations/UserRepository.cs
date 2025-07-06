using FMA.DAL.Context;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FMA.DAL.Repositories.Implementations;

public class UserRepository : GenericRepository<User>, IUserRepository
{ 
    private readonly FootballMatchAppContext _context;
    public UserRepository(FootballMatchAppContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

}