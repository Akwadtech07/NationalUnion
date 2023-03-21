using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NationalUnion.Models;

namespace NationalUnion.Interface.Repository
{
    public interface IBusRepository:IBaseRepository<Bus>
    {
        Task<Bus> Get(int id);
        Task<Bus> Get(Expression<Func<Bus, bool>> expression);
        Task<IEnumerable<Bus>> GetSelected(List<int> ids);
        Task<IEnumerable<Bus>> GetSelected(Expression<Func<Bus, bool>> expression);
        Task<IEnumerable<Bus>> GetAll();  
    }
}