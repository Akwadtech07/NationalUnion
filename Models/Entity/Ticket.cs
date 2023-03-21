using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NationalUnion.Models.Entity
{
    public class Ticket: BaseEntity
    {
        public string ReferenceNumber { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime DateCreated{get; set;}
        public int? DriverId{get; set;}
        public Driver Driver {get; set;} 
    }
}