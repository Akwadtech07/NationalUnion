using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NationalUnion.Models;

namespace NationalUnion.Interface.Repository
{
    public interface IDriverRepository:IBaseRepository<Driver>
    {
        Task<User> GetDriver(int id);
        Task<Driver> Get(int Id);
        Task<Driver> Get(Expression<Func<Driver, bool>> expression);
        Task<IEnumerable<Driver>> GetSelected(List<int> ids);
        Task<IEnumerable<Driver>> GetSelected(Expression<Func<Driver, bool>> expression);
        Task<IEnumerable<Driver>> GetAll(); 
        Task<Driver> FundWallet(int id, double wallet);
        Task<Driver> BuyTicket(int DriverId);
        Task<Driver> TicketPayment(int id, double wallet);
    }
}