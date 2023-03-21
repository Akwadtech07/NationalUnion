using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.ApplicationContext;
using NationalUnion.Interface.Repository;
using NationalUnion.Models;
using NationalUnion.Models.DTOs;

namespace NationalUnion.Interface.Service
{
    public interface IUserService
    {
        Task<BaseResponce<UserDTO>> Login(LoginUserRequestModel model);
        Task<BaseResponce<UserDTO>> Get(int id);
        Task<BaseResponce<IEnumerable<UserDTO>>> GetAll(); 
        Task<BaseResponce<UserDTO>> AssignRoles(int id, List<int> roleIds);
        Task<BaseResponce<UserDTO>> AssignManagerRole(int id, string name);
        Task<BaseResponce<UserDTO>> AssignTreasurerRole(int id, string name);
        Task<BaseResponce<UserDTO>> AssignDriverRole(int id, string name);
    }
}