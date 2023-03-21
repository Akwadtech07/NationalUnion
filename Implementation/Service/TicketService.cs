using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NationalUnion.Interface.Repository;
using NationalUnion.Interface.Service;
using NationalUnion.Models;
using NationalUnion.Models.DTOs;
using NationalUnion.Models.Entity;

namespace NationalUnion.Implementation.Service
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TicketService(ITicketRepository ticketRepository, IDriverRepository driverRepository, IUserRepository userRepository, IRoleRepository roleRepository, IHttpContextAccessor httpContextAccessor)
        {
            _ticketRepository = ticketRepository;
            _driverRepository = driverRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponce<TicketDTO>> Create(CreateTicketRequestModel model)
        {

            var ticket = new Ticket
            {
                Price = model.Price,
                DateCreated = DateTime.UtcNow,
                IsAvailable = true,
                ReferenceNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToUpper(),
            };

            await _ticketRepository.Add(ticket);

            return new BaseResponce<TicketDTO>
            {
                Message = "ticket created successfully",
                Status = true,
                Data = new TicketDTO
                {
                    ReferenceNumber = ticket.ReferenceNumber,
                }
            };
        }

        public async Task<BaseResponce<TicketDTO>> Get(int id)
        {
            var ticket = await _ticketRepository.Get(id);
            if (ticket == null) return new BaseResponce<TicketDTO>
            {
                Message = "ticket not found",
                Status = false,
            };
            return new BaseResponce<TicketDTO>
            {
                Message = "Successful",
                Status = true,
                Data = new TicketDTO
                {
                    Id = ticket.Id,
                    ReferenceNumber = ticket.ReferenceNumber,
                }
            };
        }

        public async Task<BaseResponce<IEnumerable<TicketDTO>>> GetAll()
        {
            var tickets = await _ticketRepository.GetAll();
            var listOftickets = tickets.ToList().Select(ticket => new TicketDTO
            {
                Id = ticket.Id,
                ReferenceNumber = ticket.ReferenceNumber,
                Price = ticket.Price,
                IsAvailable = ticket.IsAvailable,
                DateCreated = ticket.DateCreated,
                
            });
            return new BaseResponce<IEnumerable<TicketDTO>>
            {
                Message = "success",
                Status = true,
                Data = listOftickets
            };
        }

        public async Task<BaseResponce<IEnumerable<TicketDTO>>> GetAllAvailableTickets()
        {
            var tickets = await _ticketRepository.GetSelected(a => a.IsAvailable == true);
            
            var listOftickets = tickets.ToList().Select(ticket => new TicketDTO
            {
                Id = ticket.Id,
                DateCreated = ticket.DateCreated,
                ReferenceNumber = ticket.ReferenceNumber,
                Price = ticket.Price
            });
            return new BaseResponce<IEnumerable<TicketDTO>>
            {
                Message = "success",
                Status = true,
                Data = listOftickets
            };
        }

        public async Task<BaseResponce<IEnumerable<TicketDTO>>> GetAllSoldTickets()
        {
            var tickets = await _ticketRepository.GetSelected(a => a.IsAvailable == false);
            
            var listOftickets = tickets.ToList().Select(ticket => new TicketDTO
            {
                Id = ticket.Id,
                DateCreated = ticket.DateCreated,
                ReferenceNumber = ticket.ReferenceNumber
            });
            return new BaseResponce<IEnumerable<TicketDTO>>
            {
                Message = "success",
                Status = true,
                Data = listOftickets
            };
        }
    }

}
