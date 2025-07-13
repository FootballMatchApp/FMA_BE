using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMA.DAL.Repositories.Interfaces;

namespace FMA.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository BookingRepository { get; }
        IMatchPostRepository MatchPostRepository { get; }

        IPitchRepository PitchRepository { get; }

        ITeamRepository TeamRepository { get; }
        ITeamMemberRepository TeamMemberRepository { get; }
        IUserRepository UserRepository { get; }

        IUserTokenRepository UserTokenRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IMatchRequestRepository MatchRequestRepository { get; }
        Task<int> SaveAsync();
        Task<bool> SaveChangeAsync();
    }
}
