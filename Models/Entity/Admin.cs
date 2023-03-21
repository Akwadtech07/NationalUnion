using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.Models.Entity;

namespace NationalUnion.Models
{
    public class Admin: BaseEntity
    {
        public string RegNo{get; set;}
        public int UserId { get; set; }
        public User User { get; set; }
    

    }
}