using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NationalUnion.ApplicationContext;
using NationalUnion.Implementation.Repository;
using NationalUnion.Interface.Repository;
using NationalUnion.Models;

namespace nurtwMvc.Implementation.Repository
{
    public class BusRepository : BaseRepository<Bus>, IBusRepository
    {
          public BusRepository(ApplicatioDbContext context)
        {
            _context = context;
        }

        public async Task<Bus> Get(int id)
        {
            return await _context.Buses.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Bus> Get(Expression<Func<Bus, bool>> expression)
        {
            return await _context.Buses.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Bus>> GetAll()
        {
            return await _context.Buses.Where(a => a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Bus>> GetSelected(List<int> ids)
        {
            return await _context.Buses.Where(a => ids.Contains(a.Id) && a.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Bus>> GetSelected(Expression<Func<Bus, bool>> expression)
        {
            return await _context.Buses.Where(expression).ToListAsync();
        }
    }
}