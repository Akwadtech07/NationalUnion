using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.Models.DTOs;

namespace NationalUnion.Interface.Service
{
    public interface IRoleService
    {
        Task<BaseResponce<RoleDto>> Create(CreateRoleRequestModel model);
        Task<BaseResponce<RoleDto>> Get(int id);
        Task<BaseResponce<IEnumerable<RoleDto>>> GetAll();
       
    }
}