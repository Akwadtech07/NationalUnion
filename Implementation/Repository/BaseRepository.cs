using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.ApplicationContext;
using NationalUnion.Interface.Repository;
using NationalUnion.Models.Entity;

namespace NationalUnion.Implementation.Repository
{
     public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {

        protected ApplicatioDbContext _context;

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
             _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

         public bool Save()
        {
            _context.SaveChanges();
            return true;
        }
    }
}