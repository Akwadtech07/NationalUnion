using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.Models;
using NationalUnion.Models.DTOs;


namespace NationalUnion.Interface.Service
{
    public interface IDriverService
    {
        Task<BaseResponce<DriverDTO>> Create(CreateDriverRequestModel model);
        Task<BaseResponce<DriverDTO>> Get(int Id);
        Task<BaseResponce<IEnumerable<DriverDTO>>> GetAll();
        Task<BaseResponce<DriverDTO>> Update( int id, UpdateDriverRequestModel model);
        Task<BaseResponce<DriverDTO>> FundWallet(int id, double wallet);
        Task<BaseResponce<DriverDTO>> BuyTicket(int DriverId);
        // Task<BaseResponce<DriverDTO>> TicketPayment(int id, double wallet);

    }
}