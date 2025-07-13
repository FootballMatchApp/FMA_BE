using FMA.DAL.Entities;

namespace FMA.DAL.Repositories.Interfaces;

public interface IBookingRepository : IGenericRepository<Booking>
{
    Task<IEnumerable<Booking>> GetAllAsync();

}