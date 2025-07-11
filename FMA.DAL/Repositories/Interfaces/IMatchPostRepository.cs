

using FMA.DAL.Entities;

namespace FMA.DAL.Repositories.Interfaces;

public interface IMatchPostRepository : IGenericRepository<MatchPost>
{
    Task<IEnumerable<MatchPost>> GetAllAsync();

}