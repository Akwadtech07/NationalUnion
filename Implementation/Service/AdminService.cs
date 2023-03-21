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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminService(IAdminRepository adminRepository, IUserRepository userRepository, IRoleRepository roleRepository, IHttpContextAccessor httpContextAccessor)
        {
            _adminRepository = adminRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponce<AdminDTO>> Create(CreateAdminRequestModel model)
        {
            var adminExist = await _adminRepository.Get(a => a.User.Email == model.Email);
            if (adminExist != null) return new BaseResponce<AdminDTO>
            {
                Message = "User already exist",
                Status = false,
            };

            var role = await _roleRepository.Get(b => b.Name == model.Role);

            var user = new User
            {

                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                UserRoles = new List<UserRole>()
            };


            var admin = new Admin
            {
                User = user,
                UserId = user.Id,
                RegNo = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5).ToUpper(),
            };

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
                Role = role,
                User = user,
            };

            user.UserRoles.Add(userRole);

            await _userRepository.Add(user);
            await _adminRepository.Add(admin);

            return new BaseResponce<AdminDTO>
            {
                Message = "created successful",
                Status = true,
                Data = new AdminDTO
                {
                    FirstName = admin.User.FirstName,
                    Email = admin.User.Email,
                }
            };
        }

        public async Task<BaseResponce<AdminDTO>> Get(int id)
        {
            var admin = await _adminRepository.Get(id);
            if (admin == null) return new BaseResponce<AdminDTO>
            {
                Message = "admin not found",
                Status = false,
            };
            return new BaseResponce<AdminDTO>
            {
                Message = "Success",
                Status = true,
                Data = new AdminDTO
                {
                    Id = admin.Id,
                    UserId = admin.UserId,
                    FirstName = admin.User.FirstName,
                    LastName = admin.User.LastName,
                    Email = admin.User.Email,
                    PhoneNumber = admin.User.PhoneNumber,
                    Address = admin.User.Address,
                    RegNo = admin.RegNo,
                    Roles = admin.User.UserRoles.Select(a => new RoleDto
                    {
                        Id = a.Id,
                        Name = a.Role.Name,
                        Description = a.Role.Description,

                    }).ToList(),
                }
            };
        }

        public async Task<BaseResponce<IEnumerable<AdminDTO>>> GetAll()
        {
            var admins = await _adminRepository.GetAll();
            var listOfadmins = admins.ToList().Select(admin => new AdminDTO
            {
                Id = admin.Id,
                UserId = admin.UserId,
                FirstName = admin.User.FirstName,
                LastName = admin.User.LastName,
                Email = admin.User.Email,
                PhoneNumber = admin.User.PhoneNumber,
                Address = admin.User.Address,
                RegNo = admin.RegNo,
                Roles = admin.User.UserRoles.Select(a => new RoleDto
                {
                    Id = a.Id,
                    Name = a.Role.Name,
                    Description = a.Role.Description,

                }).ToList(),
            });

            return new BaseResponce<IEnumerable<AdminDTO>>
            {
                Message = "successful",
                Status = true,
                Data = listOfadmins,
            };
        }
        public Task<BaseResponce<AdminDTO>> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponce<AdminDTO>> Update( int id, UpdateAdminRequestModel model)
        {
            var user = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var admin = await _adminRepository.Get(a => a.UserId == int.Parse(user));
            if (admin == null) return new BaseResponce<AdminDTO>
            {
                Message = "admin not found",
                Status = false,
            };
           
            admin.User.FirstName= model.FirstName;
            admin.User.LastName = model.LastName;
            admin.User.Email = model.Email;
            admin.User.PhoneNumber = model.PhoneNumber;
            admin.User.Address = model.Address;
            await _adminRepository.Update(admin);
            return new BaseResponce<AdminDTO>
            {
                Message = " Admin Updated Successfuly",
                Status = true,
                Data = new AdminDTO
                {
                    FirstName = admin.User.FirstName,
                    LastName = admin.User.LastName,
                }
            };
        }
    }
}
