using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NationalUnion.Interface.Service;
using NationalUnion.Models.DTOs;

namespace NationalUnion.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;

        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequestModel model)
        {
            var user = await _userService.Login(model);
            
            if (user.Data != null)
            {
                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.NameIdentifier, user.Data.Id.ToString()),
                     new Claim(ClaimTypes.Email, user.Data.Email),
                     new Claim(ClaimTypes.Name, user.Data.FirstName + " " + user.Data.LastName),


                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
                if (user.Status == true)
                {
                    if (user.Data.Roles.Select(r => r.Name).Contains("SuperAdmin"))
                    {
                        TempData["Success"] = "Login sucessful";
                        return RedirectToAction("SuperBoard");
                    }
                    else if (user.Data.Roles.Select(r => r.Name).Contains("Driver"))
                    {

                        TempData["Success"] = "Welcome";
                        return RedirectToAction("DriverBoard");
                    }
                    else if ((user.Data.Roles.Select(r => r.Name).Contains("Manager")))
                    {
                        TempData["Success"] = "Login sucessful";
                        return RedirectToAction("ManagerBoard");
                    }
                    else if ((user.Data.Roles.Select(r => r.Name).Contains("Tresurer")))
                        TempData["Success"] = "Login sucessful";
                    return RedirectToAction("TresurerBoard");
                }
                else
                {

                    TempData["Fail"] = "Username Or Password Not Correct";
                    return View();
                }
            }
            TempData["Fail"] = "Invalid Credentials pls try again with correct details!";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult SuperBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DriverBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManagerBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TresurerBoard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListAllUsers()
        {
            var users = await _userService.GetAll();
            if (users.Status == true)
            {
                return View(users.Data);
            }
            return View();
        }
    }
}