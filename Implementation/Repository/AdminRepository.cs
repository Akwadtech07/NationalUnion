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
     public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        
        public AdminRepository(ApplicatioDbContext context)
        {
            _context = context;
        }

       

        public async Task<Admin> Get(int id)
        {
            return await _context.Admins.Include(x=>x.User).FirstOrDefaultAsync(a => a.User.Id == id);
        }

        public async Task<Admin> Get(Expression<Func<Admin, bool>> expression)
        {
            return await _context.Admins.Include(a => a.User).FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Admin>> GetAll()
        {
            return await _context.Admins.Include(x => x.User).ToListAsync();
        }

        public async Task<IEnumerable<Admin>> GetSelected(List<int> ids)
        {
            return await _context.Admins.Where(a => ids.Contains(a.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Admin>> GetSelected(Expression<Func<Admin, bool>> expression)
        {
            return await _context.Admins.Where(expression).ToListAsync();
        }
    }
}