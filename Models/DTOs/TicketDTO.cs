using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalUnion.Models.DTOs
{
    public class TicketDTO
    {
        public int Id{get; set;}
        public string ReferenceNumber { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DateCreated{get; set;}
        public int DriverRegNum{get; set;}
        public string DriverName{get; set;} 
    }

    public class CreateTicketRequestModel
    {
     
        public int DriverId {get; set;}
        public double Price { get; set; }
    }
}