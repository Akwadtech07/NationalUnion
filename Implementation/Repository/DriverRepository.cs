using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NationalUnion.ApplicationContext;
using NationalUnion.Interface.Repository;
using NationalUnion.Models;

namespace NationalUnion.Implementation.Repository
{
    public class DriverRepository :  BaseRepository<Driver>, IDriverRepository
    {
        
        public DriverRepository(ApplicatioDbContext context)
        {
            _context = context;
        }

        public async Task<Driver> BuyTicket(int DriverId)
        {
            return await _context.Drivers.Include(a => a.User).FirstOrDefaultAsync(a => a.UserId == DriverId);
        }

        public async Task<Driver> FundWallet(int id, double wallet)
        {
            return await _context.Drivers.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Driver> Get(int id)
        {
            return await _context.Drivers.Include(a => a.Tickets).FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<User> GetDriver(int id)
        {
            return await _context.Users.Include(x => x.Driver).ThenInclude(a => a.Tickets).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Driver> Get(Expression<Func<Driver, bool>> expression)
        {
            return await _context.Drivers.Include(a => a.User).Include(a => a.Tickets).FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Driver>> GetAll()
        {
            return await _context.Drivers.Include(a => a.Tickets).Include(a => a.User).ToListAsync();
            
        }

        public async Task<IEnumerable<Driver>> GetSelected(List<int> ids)
        {
            return await _context.Drivers.Include(a => a.Tickets).Where(a => ids.Contains(a.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Driver>> GetSelected(Expression<Func<Driver, bool>> expression)
        {
            return await _context.Drivers.Include(a => a.Tickets).Where(expression).ToListAsync();
        }

        public async Task<Driver> TicketPayment(int id, double wallet  )
        {
            return await _context.Drivers.Include(a => a.User.Admin).FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}