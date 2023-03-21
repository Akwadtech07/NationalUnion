using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.Models.Entity;

namespace NationalUnion.Interface.Repository
{
    public interface IBaseRepository<T> where T: BaseEntity, new()
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        bool Save();  
    }
}