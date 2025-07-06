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
    public class UserTokenRepository : GenericRepository<UserToken>, IUserTokenRepository
    {
        private readonly FootballMatchAppContext _context;
        public UserTokenRepository(FootballMatchAppContext context) : base(context)
        {
            _context = context;
        }
        public async Task<UserToken> GetRefreshTokenByUserID(Guid userId)
        {
            // lấy token đúng id và chưa bị thu hồi
            return await _context.UserTokens
                .Where(rt => rt.TokenId == userId && !rt.IsRevoked)
                .FirstOrDefaultAsync();
        }
    }
}
