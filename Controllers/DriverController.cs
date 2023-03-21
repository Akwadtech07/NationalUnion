using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NationalUnion.Interface.Repository;
using NationalUnion.Interface.Service;
using NationalUnion.Models.DTOs;

namespace NationalUnion.Controllers
{
    public class DriverController : Controller
    {
        private readonly IDriverService _driverService;
        private readonly IUserRepository _userRepository;
        private readonly ITicketService _ticketService;

        private readonly IHttpContextAccessor _httpContentAccessor;

        public DriverController(ITicketService ticketService, IDriverService driverService, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _driverService = driverService;
            _userRepository = userRepository;
            _ticketService = ticketService;
            _httpContentAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDriverRequestModel model)
        {
            var driver = await _driverService.Create(model);
            if (driver.Status == true)
            {
                TempData["Success"] = "Registration sucessful";
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var drivers = await _driverService.GetAll();
            if (drivers.Status == true)
            {
                return View(drivers.Data);
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> TicketPayment(int userId, int ticketId)
        {
            var ticket = await _ticketService.Get(ticketId);
            var drivers = await _userRepository.Get(userId);
            if (drivers.Wallet < ticket.Data.Price)
            {
                return NotFound();
            }

            return View();

        }
        [HttpGet]
        public IActionResult Update()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateDriverRequestModel model)
        {
            await _driverService.Update(id, model);
            TempData["Success"] = "Update sucessful";
            return RedirectToAction("GetAll", "Driver");
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var drivers = await _driverService.Get(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            if (drivers.Status == true)
            {
                return View(drivers.Data);
            }
            return View();
        }
        [HttpGet]
        public IActionResult FundWallet()
        {
            TempData["Updated"] = "Wallet Funded sucessful";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FundWallet(double price)
        {
            var user = _httpContentAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var tickets = await _driverService.FundWallet(int.Parse(user), price);
            if (tickets.Status == true)
            {
                TempData["Updated"] = "Wallet Funded sucessful";
                return RedirectToAction("DriverBoard", "User");
            }
            else
            {
                TempData["Invalid"] = "Invalid Transaction";
                return RedirectToAction("Fundwallet");
            }
          
        }
    }
}