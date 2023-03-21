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
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RoleController(IRoleService roleService, IWebHostEnvironment host)
        {
            _roleService = roleService;
            _webHostEnvironment = host;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequestModel model)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;

            var role = await _roleService.Create(model);
            if (role.Status == true)
            {
                return RedirectToAction("SuperBoard", "User");
            }
            return View();
        }
            [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            if(roles.Status == true)
            {
                return View(roles.Data);
            }
            return View();
        }
    }
}