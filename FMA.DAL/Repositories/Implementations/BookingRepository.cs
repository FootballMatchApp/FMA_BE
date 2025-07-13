using FMA.DAL.Context;
using FMA.DAL.Entities;
using FMA.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FMA.DAL.Repositories.Implementations;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    private readonly FootballMatchAppContext _context;

    public BookingRepository(FootballMatchAppContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await _context.Bookings.ToListAsync();
    }

}