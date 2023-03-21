using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NationalUnion.Models;

namespace NationalUnion.Interface.Repository
{
    public interface IAdminRepository: IBaseRepository<Admin>
    {
        Task<Admin> Get(int id);
        Task<Admin> Get(Expression<Func<Admin, bool>> expression);
        Task<IEnumerable<Admin>> GetSelected(List<int> ids);
        Task<IEnumerable<Admin>> GetSelected(Expression<Func<Admin, bool>> expression);
        Task<IEnumerable<Admin>> GetAll();
        
        
    }
}