using FMA.DAL.Entities;

namespace FMA.DAL.Repositories.Interfaces;

public interface ITeamMemberRepository : IGenericRepository<TeamMember>
{
    Task<TeamMember> GetByTeamAndUserAsync(Guid teamId, Guid userId);
    Task<TeamMember?> GetTeamMemberDetailAsync(Guid userId);
    Task<IEnumerable<TeamMember>> GetMembersByTeamIdAsync(Guid teamId);
}