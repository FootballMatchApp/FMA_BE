using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.DAL.Entities;

namespace FMA.DAL.Repositories.Interfaces
{
    public interface IUserRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> GetRoleByNameAsync(string roleName);
    }
}
