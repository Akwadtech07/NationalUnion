using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.Models.Entity;

namespace NationalUnion.Models
{
    public class Bus:BaseEntity
    {
        public string BusRegNumber { get; set; }
        public string PlateNumber { get; set; }
        public int DriverId{get; set;}
        public Driver Driver{get; set;}
    }
}