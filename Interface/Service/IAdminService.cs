using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.Models;
using NationalUnion.Models.DTOs;

namespace NationalUnion.Interface.Service
{
    public interface IAdminService
    {
        Task<BaseResponce<AdminDTO>> Create(CreateAdminRequestModel model);
        Task<BaseResponce<AdminDTO>> Get(int id);
        Task<BaseResponce<AdminDTO>> GetByEmail(string email);
        Task<BaseResponce<IEnumerable<AdminDTO>>> GetAll();
        Task<BaseResponce<AdminDTO>> Update( int id, UpdateAdminRequestModel model);
    }
}