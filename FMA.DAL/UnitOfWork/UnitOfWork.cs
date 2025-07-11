using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.DAL.Context;
using FMA.DAL.Repositories.Implementations;
using FMA.DAL.Repositories.Interfaces;

namespace FMA.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FootballMatchAppContext _context;

        public UnitOfWork(FootballMatchAppContext context)
        {
            _context = context;
            BookingRepository = new BookingRepository(_context);
            MatchPostRepository = new MatchPostRepository(_context);

            PitchRepository = new PitchRepository(_context);

            TeamRepository = new TeamRepository(_context);
            TeamMemberRepository = new TeamMemberRepository(_context);
            UserRepository = new UserRepository(_context);
            UserTokenRepository = new UserTokenRepository(_context);
            UserRoleRepository = new UserRoleRepository(_context);
        }


        public IBookingRepository BookingRepository { get; private set; }
        public IMatchPostRepository MatchPostRepository { get; private set; }

        public IPitchRepository PitchRepository { get; private set; }

        public ITeamRepository TeamRepository { get; private set; }
        public ITeamMemberRepository TeamMemberRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }

        public IUserTokenRepository UserTokenRepository { get; private set; }
        public IUserRoleRepository UserRoleRepository { get; private set; }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> SaveChangeAsync()
        {
            return await SaveAsync() > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }



}
