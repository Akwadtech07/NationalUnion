using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.Interface.Repository;
using NationalUnion.Interface.Service;
using NationalUnion.Models.DTOs;
using NationalUnion.Models.Entity;

namespace NationalUnion.Implementation.Service
{
   public class RoleService: IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<BaseResponce<RoleDto>> Create(CreateRoleRequestModel model)
        {
            var roleExist = await _roleRepository.Get(a => a.Name == model.Name);
            if (roleExist != null) return new BaseResponce<RoleDto>
            {
                Message = "Role already exist",
                Status = false,

            };

            var role = new Role
            {
                Name = model.Name,
                Description = model.Description,
            };

            await _roleRepository.Add(role);

            return new BaseResponce<RoleDto>
            {
                Message = "Created Successfully",
                Status = true,
                Data = new RoleDto
                {
                    Name = role.Name
                }
            };
        }

        public async Task<BaseResponce<RoleDto>> Get(int id)
        {
            var role = await _roleRepository.Get(id);
            if (role == null) return new BaseResponce<RoleDto>
            {
                Message = "Role not found",
                Status = false,
            };
            return new BaseResponce<RoleDto>
            {
                Message = "Successful",
                Status = true,
                Data = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    Users = role.UserRoles.Select(v => new UserDTO
                    {
                        FirstName = v.User.FirstName,
                        LastName = v.User.LastName,
                        Email = v.User.Email,

                    }).ToList(),
                },
            };
        }

        public async Task<BaseResponce<IEnumerable<RoleDto>>> GetAll()
        {
            var roles = await _roleRepository.GetAll();
            var listOfRoles = roles.ToList().Select(a => new RoleDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Users = a.UserRoles.Select(b => new UserDTO
                {
                    FirstName = b.User.FirstName,
                    LastName = b.User.LastName,
                    Email = b.User.Email,
                }).ToList(),
            });
            return new BaseResponce<IEnumerable<RoleDto>>
            {
                Message = "ok",
                Status = true,
                Data = listOfRoles,
            };
        }
    }
}