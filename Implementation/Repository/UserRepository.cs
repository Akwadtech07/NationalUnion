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
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        
        public UserRepository(ApplicatioDbContext context)
        {
            _context = context;
        }

        public async Task<User> Get(int id)
        {
            return await _context.Users
                .Include(b => b.UserRoles)
                .ThenInclude(c => c.Role)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<User> Get(Expression<Func<User, bool>> expression)
        {
            return await _context.Users
            .Include(b => b.UserRoles)
                .ThenInclude(c => c.Role)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetSelected(List<int> ids)
        {
            return await _context.Users.Where(a => ids.Contains(a.Id)).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetSelected(Expression<Func<User, bool>> expression)
        {
            return await _context.Users.Where(expression).ToListAsync();
        }
        
    }
}