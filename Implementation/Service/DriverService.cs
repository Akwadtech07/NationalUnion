using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NationalUnion.Implementation.Repository;
using NationalUnion.Interface.Repository;
using NationalUnion.Interface.Service;
using NationalUnion.Models;
using NationalUnion.Models.DTOs;
using NationalUnion.Models.Entity;

namespace NationalUnion.Implementation.Service
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DriverService(IDriverRepository driverRepository, IUserRepository userRepository, IRoleRepository roleRepository, ITicketRepository ticketRepository, IHttpContextAccessor httpContextAccessor)
        {
            _driverRepository = driverRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<BaseResponce<DriverDTO>> BuyTicket(int DriverId)
        {
            var user = await _driverRepository.GetDriver(DriverId);
            var pickTicket = await _ticketRepository.Get(a => a.IsAvailable == true);
            if (user.Wallet >= pickTicket.Price)
            {
                user.Wallet = user.Wallet - pickTicket.Price;
                await _userRepository.Update(user);
                var trees = await _driverRepository.GetDriver(2);
                trees.Wallet = trees.Wallet + pickTicket.Price;
                await _userRepository.Update(trees);
            }
            else if (user.Wallet < pickTicket.Price)
            {   
                return new BaseResponce<DriverDTO>
                {
                    Message = "Insufficient  Fund Pls Fund Wallet",
                    Status = false,
                };
            }
            else
            {
                pickTicket.IsAvailable = false;
                pickTicket.DriverId = user.Driver.Id;
                await _ticketRepository.Update(pickTicket);
            }
            return new BaseResponce<DriverDTO>
            {
                Message = " Ticket successfully purchased",
                Status = true,

            };


        }

        public async Task<BaseResponce<DriverDTO>> Create(CreateDriverRequestModel model)
        {
            var driverExist = await _driverRepository.Get(b => b.User.Email == model.Email);
            if (driverExist != null)
            {
                return new BaseResponce<DriverDTO>
                {
                    Message = "user already exist",
                    Status = false,
                };
            }

            var role = await _roleRepository.Get(b => b.Name == "Driver");

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
            };

            user = await _userRepository.Add(user);

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
                Role = role,
                User = user,
            };

            user.UserRoles.Add(userRole);
            await _userRepository.Update(user);

            var driver = new Driver
            {
                UserId = user.Id,
                User = user,
                DriverRegNum = $"NURTW" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToUpper(),


            };


            await _driverRepository.Add(driver);


            return new BaseResponce<DriverDTO>
            {
                Message = "Created successfully",
                Status = true,
                Data = new DriverDTO
                {
                    FirstName = driver.User.FirstName,
                    LastName = driver.User.LastName,
                    Email = driver.User.Email,

                }
            };
        }

        public async Task<BaseResponce<DriverDTO>> FundWallet(int id, double wallet)
        {
            var user = await _driverRepository.GetDriver(id);
            if (user == null) return new BaseResponce<DriverDTO>
            {
                Message = "User not found",
                Status = false,
            };

             if (wallet <= 0) return new BaseResponce<DriverDTO>
            {
                Message = "Invalid Transaction",
                Status = false,
            };
            else
            {
            user.Wallet += wallet;
            await _userRepository.Update(user);
            return new BaseResponce<DriverDTO>
            {
                Message = "success",
                Status = true,

            };
            }
        }

        public async Task<BaseResponce<DriverDTO>> Get(int id)
        {
            var user = await _driverRepository.GetDriver(id);
            if (user == null) return new BaseResponce<DriverDTO>
            {
                Message = "User not found",
                Status = false,
            };

            return new BaseResponce<DriverDTO>
            {
                Message = "success",
                Status = true,
                Data = new DriverDTO
                {
                    Id = user.Id,
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    Roles = user.UserRoles.Select(b => new RoleDto
                    {
                        Id = b.RoleId,
                        Name = b.Role.Name,
                    }).ToList(),
                }
            };
        }

        public async Task<BaseResponce<IEnumerable<DriverDTO>>> GetAll()
        {
            var drivers = await _driverRepository.GetAll();
            var listOfdrivers = drivers.ToList().Select(driver => new DriverDTO
            {
                Id = driver.Id,
                UserId = driver.User.Id,
                FirstName = driver.User.FirstName,
                LastName = driver.User.LastName,
                Email = driver.User.Email,
                Address = driver.User.Address,
                PhoneNumber = driver.User.PhoneNumber,
                Roles = driver.User.UserRoles.Select(b => new RoleDto
                {
                    Id = b.RoleId,
                    Name = b.Role.Name,
                }).ToList(),
            });

            return new BaseResponce<IEnumerable<DriverDTO>>
            {
                Message = "ok",
                Status = true,
                Data = listOfdrivers,
            };
        }

        // public async Task<BaseResponce<DriverDTO>> TicketPayment(int id, double wallet)
        // {
        //     var driver = await _driverRepository.Get(id);
        //     if(driver == null) return new BaseResponce<DriverDTO>
        //     {
        //         Message = "User not found",
        //         Status = false,
        //     };
        //     else
        //     {
        //         driver.User.Wallet +- 
        //     }
        // }

        public async Task<BaseResponce<DriverDTO>> Update(int id, UpdateDriverRequestModel model)
        {
            var user = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var driver = await _driverRepository.Get(a => a.UserId == int.Parse(user));
            if (driver == null) return new BaseResponce<DriverDTO>
            {
                Message = "driver not found",
                Status = false,
            };

            driver.User.FirstName = model.FirstName;
            driver.User.LastName = model.LastName;
            driver.User.Email = model.Email;
            driver.User.PhoneNumber = model.PhoneNumber;
            driver.User.Address = model.Address;
            await _driverRepository.Update(driver);
            return new BaseResponce<DriverDTO>
            {
                Message = "Successful",
                Status = true,
                Data = new DriverDTO
                {
                    FirstName = driver.User.FirstName,
                    LastName = driver.User.LastName,
                }
            };
        }
    }
}




