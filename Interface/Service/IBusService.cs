using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.Models;
using NationalUnion.Models.DTOs;

namespace NationalUnion.Interface.Service
{
    public interface IBusService
    {
       
        Task<BaseResponce<BusDTO>> Create(CreateBusRequestModel model);
        Task<BaseResponce<BusDTO>> Get(int id);
        Task<BaseResponce<IEnumerable<BusDTO>>> GetAll();

    }
}