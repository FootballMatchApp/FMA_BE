using FMA.DAL.Context;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;

namespace FMA.DAL.Repositories.Implementations;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    private new readonly FootballMatchAppContext _context;

    public BookingRepository(FootballMatchAppContext context) : base(context)
    {
        _context = context;
    }
}