using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NationalUnion.Interface.Repository;
using NationalUnion.Interface.Service;
using NationalUnion.Models;
using NationalUnion.Models.DTOs;

namespace NationalUnion.Implementation.Service
{
    public class BusService : IBusService
    {
        private readonly IBusRepository _busRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        public BusService(IBusRepository busRepository, IDriverRepository driverRepository,IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _busRepository = busRepository;

            _httpContextAccessor = httpContextAccessor;
            _driverRepository = driverRepository;
            _userRepository = userRepository;



        }

        public async Task<BaseResponce<BusDTO>> Create(CreateBusRequestModel model)
        {
            var busExist = await _busRepository.Get(a => a.PlateNumber == model.PlateNumber);
            if (busExist != null) return new BaseResponce<BusDTO>
            {
                Message = "Bus already exist",
                Status = false,
            };
            var user = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var driver = await _driverRepository.Get(a => a.UserId == int.Parse(user));
            var bus = new Bus
            {

                PlateNumber = model.PlateNumber,
                BusRegNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6).ToUpper(),
                DriverId = driver.Id

            };

            await _busRepository.Add(bus);

            return new BaseResponce<BusDTO>
            {
                Message = "Bus created successfully",
                Status = true,
                Data = new BusDTO
                {
                    BusRegNumber = bus.BusRegNumber,
                }
            };
        }

        public async Task<BaseResponce<BusDTO>> Get(int id)
        {
            var bus = await _busRepository.Get(id);
            if (bus == null) return new BaseResponce<BusDTO>
            {
                Message = "bus not found",
                Status = false,
            };
            return new BaseResponce<BusDTO>
            {
                Message = "Successful",
                Status = true,
                Data = new BusDTO
                {
                    Id = bus.Id,
                    PlateNumber = bus.PlateNumber,
                    BusRegNumber = bus.BusRegNumber
                }
            };
        }

        public async Task<BaseResponce<IEnumerable<BusDTO>>> GetAll()
        {
            var buses = await _busRepository.GetAll();
            var listOfbuses = buses.ToList().Select(buses => new BusDTO

            {
                BusRegNumber = buses.BusRegNumber,
                PlateNumber = buses.PlateNumber,
                DriverId = buses.DriverId,
                DriverName = buses.Driver       
            }
            );
            return new BaseResponce<IEnumerable<BusDTO>>
            {
                Message = "ok",
                Status = true,
                Data = listOfbuses,
            };

        }
    }



}


