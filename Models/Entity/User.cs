using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NationalUnion.Models.Entity;

namespace NationalUnion.Models
{
    public class User: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber{get; set;}
        public double Wallet{get; set;}
        public string Address { get; set; }
        public Admin Admin { get; set; }
        public Driver Driver { get; set; }
        public IList<UserRole>UserRoles{ get; set; } = new List<UserRole>();

    }
}