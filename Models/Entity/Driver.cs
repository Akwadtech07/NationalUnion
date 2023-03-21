using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.Models.Entity;

namespace NationalUnion.Models
{
    public class Driver:BaseEntity
    {
        public string DriverRegNum { get; set; }
        public int UserId{ get; set; }
        public User User{ get; set; }
        public IList<Ticket>  Tickets { get; set; } = new List<Ticket>();
        public Bus Bus{get; set;}

    }
    
}