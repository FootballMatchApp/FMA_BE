using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.DAL.Context;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FMA.DAL.Repositories.Implementations
{
    public class UserRoleRepository : GenericRepository<Role>, IUserRoleRepository
    {
        private readonly FootballMatchAppContext _context;
        public UserRoleRepository(FootballMatchAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == roleName);
        }
    }
}
