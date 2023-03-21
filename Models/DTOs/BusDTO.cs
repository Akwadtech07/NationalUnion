using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalUnion.Models.DTOs
{
    public class BusDTO 
    {
        public int Id { get; set; }
        public string BusRegNumber { get; set; }
        public string PlateNumber { get; set; }
        public int DriverId{get; set;}
        public Driver DriverName{get; set;}
    }
    public class CreateBusRequestModel
    {
       public string PlateNumber { get; set; }
    }
}