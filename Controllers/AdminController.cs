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
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(IAdminService adminService, IWebHostEnvironment host)
        {
            _adminService = adminService;
            _webHostEnvironment = host;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminRequestModel model)
        {
            var admin = await _adminService.Create(model);
            if (admin.Status == true)
            {
               TempData["send"] = "Resitrstion sucessful";
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var admins = await _adminService.GetAll();
            if (admins.Status == true)
            {
                return View(admins.Data);
            }
            return View();
        }


         [HttpGet]
        public IActionResult Update()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateAdminRequestModel model)
        {
            await _adminService.Update(id, model);
            TempData["Success"] = "Login sucessful";
            return RedirectToAction("GetAll","Admin");
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
             var admins = await _adminService.Get(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            if (admins.Status == true)
            {
                return View(admins.Data);
            }
            return View();
        }
       


    }
}