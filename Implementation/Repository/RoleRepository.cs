using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NationalUnion.ApplicationContext;
using NationalUnion.Interface.Repository;
using NationalUnion.Models.Entity;

namespace NationalUnion.Implementation.Repository
{
    public class RoleRepository: BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicatioDbContext context)
        {
            _context = context;
        }

        public async Task<Role> Get(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Role> Get(Expression<Func<Role, bool>> expression)
        {
            return await _context.Roles.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetSelected(List<int> ids)
        {
            return await _context.Roles.Where(a => ids.Contains(a.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetSelected(Expression<Func<Role, bool>> expression)
        {
            return await _context.Roles.Where(expression).ToListAsync();
        }
    }
}