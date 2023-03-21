using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.Interface.Repository;
using NationalUnion.Interface.Service;
using NationalUnion.Models;
using NationalUnion.Models.DTOs;
using NationalUnion.Models.Entity;

namespace NationalUnion.Implementation.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<BaseResponce<UserDTO>> AssignDriverRole(int id, string name)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return new BaseResponce<UserDTO>
            {
                Message = "user not found",
                Status = false,
            };

            var role = await _roleRepository.Get(b => b.Name == "Driver");

            user.UserRoles.Add(new Models.Entity.UserRole
            {
                RoleId = role.Id,
                UserId = user.Id,
            });

            return new BaseResponce<UserDTO>
            {
                Status = true,
                Message = "successfull",

            };
        }

        public async Task<BaseResponce<UserDTO>> AssignManagerRole(int id, string name)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return new BaseResponce<UserDTO>
            {
                Message = "user not found",
                Status = false,
            };

            var role = await _roleRepository.Get(b => b.Name == "Manager");

            user.UserRoles.Add(new Models.Entity.UserRole
            {
                RoleId = role.Id,
                UserId = user.Id,
            });

            return new BaseResponce<UserDTO>
            {
                Status = true,
                Message = "successfull",

            };
        }

        public Task<BaseResponce<UserDTO>> AssignRoles(int id, List<int> roleIds)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponce<UserDTO>> AssignTreasurerRole(int id, string name)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return new BaseResponce<UserDTO>
            {
                Message = "user not found",
                Status = false,
            };

            var role = await _roleRepository.Get(b => b.Name == "Tresurer");

            user.UserRoles.Add(new Models.Entity.UserRole
            {
                RoleId = role.Id,
                UserId = user.Id,
            });

            return new BaseResponce<UserDTO>
            {
                Status = true,
                Message = "successfull",

            };
        }

        public async Task<BaseResponce<UserDTO>> Get(int id)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return new BaseResponce<UserDTO>
            {
                Message = "User not found",
                Status = false,
            };
            return new BaseResponce<UserDTO>
            {
                Message = "Success",
                Status = true,
                Data = new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Roles = user.UserRoles.Select(a => new RoleDto
                    {
                        Id = a.Role.Id,
                        Name = a.Role.Name,
                        Description = a.Role.Description,
                    }).ToList(),
                }
            };
        }

        public async Task<BaseResponce<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userRepository.GetAll();
            var listOfUsers = users.ToList().Select(user => new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Roles = user.UserRoles.Select(a => new RoleDto
                {
                    Id = a.Role.Id,
                    Name = a.Role.Name,
                    Description = a.Role.Description,
                }).ToList(),
            });

            return new BaseResponce<IEnumerable<UserDTO>>
            {
                Message = "success",
                Status = true,
                Data = listOfUsers,
            };
        }

        public async Task<BaseResponce<UserDTO>> Login(LoginUserRequestModel model)
        {
            var user = await _userRepository.Get(a => a.Email == model.Email && a.Password == model.Password);
            if (user == null) return new BaseResponce<UserDTO>
            {
                Message = "invalid cridentials",
                Status = false,

            };


            else
            {
                return new BaseResponce<UserDTO>
                {
                    Message = "login successful",
                    Status = true,
                    Data = new UserDTO
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Roles = user.UserRoles.Select(a => new RoleDto
                        {
                            Id = a.Role.Id,
                            Name = a.Role.Name,
                            Description = a.Role.Description,
                        }).ToList(),

                    }
                };
            }

        }
    }
}
