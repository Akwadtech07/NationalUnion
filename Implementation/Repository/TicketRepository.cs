using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NationalUnion.ApplicationContext;
using NationalUnion.Interface.Repository;
using NationalUnion.Models;
using NationalUnion.Models.Entity;

namespace NationalUnion.Implementation.Repository
{
    public class TicketRepository:BaseRepository<Ticket>, ITicketRepository
    {
        
        public TicketRepository(ApplicatioDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> Get(int id)
        {
            return await _context.Tickets.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Ticket> Get(Expression<Func<Ticket, bool>> expression)
        {
            return await _context.Tickets.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetSelected(List<int> ids)
        {
            return await _context.Tickets.Where(a => ids.Contains(a.Id)).ToListAsync();
        }



        public async Task<IEnumerable<Ticket>> GetSelected(Expression<Func<Ticket, bool>> expression)
        {
            return await _context.Tickets.Where(expression).ToListAsync();
        }
    }
}