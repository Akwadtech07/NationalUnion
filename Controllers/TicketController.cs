using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NationalUnion.Interface.Service;
using NationalUnion.Models.DTOs;

namespace NationalUnion.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IDriverService _driverService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TicketController(ITicketService ticketService, IDriverService driverService, IHttpContextAccessor httpContextAccessor)
        {
            _ticketService = ticketService;
            _driverService = driverService;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketRequestModel model)
        {
            var driver = await _ticketService.Create(model);
            if (driver.Status == true)
            {   TempData["Tick"] = "Ticket Created sucessfuly";
                return RedirectToAction("GetAll");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAvailableTickets()
        {
            var tickets = await _ticketService.GetAllAvailableTickets();
            if (tickets.Status == true)
            {
                return View(tickets.Data);
            }
            return View();
        }
         public async Task<IActionResult> BuyTicket()
        {
            var user = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var tickets = await _driverService.BuyTicket(int.Parse(user));
            if (tickets.Status == true)
            {
                TempData["Fund"] = "Your Purchase is Successful A  nd The Ticket price is deducted from your wallet";
                RedirectToAction("DriverBoard", "User");

            }
            return RedirectToAction("DriverBoard", "User");

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ticket = await _ticketService.GetAll();
            if (ticket.Status == true)
            {
                return View(ticket.Data);
            }
            return View();
        }

    }
}