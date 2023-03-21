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
    public class BusController : Controller
    {
        private readonly IBusService _busService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BusController(IBusService busService, IWebHostEnvironment host)
        {
            _busService = busService;
            _webHostEnvironment = host;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBusRequestModel model)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;

            var bus = await _busService.Create(model);
            if (bus.Status == true)
            {
                ViewBag.Message = "Bus Created Succssfully";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var buses = await _busService.GetAll();
            if (buses.Status == true)
            {
                return View(buses.Data);
            }
            return View();
        }
    }
}