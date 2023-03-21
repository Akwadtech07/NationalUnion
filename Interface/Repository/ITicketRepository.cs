using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NationalUnion.Models;
using NationalUnion.Models.Entity;

namespace NationalUnion.Interface.Repository
{
    public interface ITicketRepository:  IBaseRepository<Ticket>
    {
        
        Task<Ticket> Get(int id);
        Task<Ticket> Get(Expression<Func<Ticket, bool>> expression);
        Task<IEnumerable<Ticket>> GetSelected(List<int> ids);
        Task<IEnumerable<Ticket>> GetSelected(Expression<Func<Ticket, bool>> expression);
        Task<IEnumerable<Ticket>> GetAll();
       
    }
}