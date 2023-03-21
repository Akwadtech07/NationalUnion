using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.Models;
using NationalUnion.Models.DTOs;


namespace NationalUnion.Interface.Service
{
    public interface ITicketService
    {
        Task<BaseResponce<TicketDTO>> Create(CreateTicketRequestModel model);
        Task<BaseResponce<TicketDTO>> Get(int id);
        Task<BaseResponce<IEnumerable<TicketDTO>>> GetAll();
        Task<BaseResponce<IEnumerable<TicketDTO>>> GetAllAvailableTickets();
        Task<BaseResponce<IEnumerable<TicketDTO>>> GetAllSoldTickets();


    }
}