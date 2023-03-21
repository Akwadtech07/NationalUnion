using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalUnion.Models.DTOs
{
    public class DriverDTO
    {
        public int Id{get; set;}
        public int UserId{get;set;}
        public int BusId{get;set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber{get; set;}
        public double Wallet{get; set;}
        public string Address { get; set; }
        public string DriverRegNum { get; set; }
        public IList<RoleDto>Roles{ get; set; }      

    }
    public class CreateDriverRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber{get; set;}
        public string Address { get; set; }
    }
        public class UpdateDriverRequestModel
    {
        public int Id {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber{get; set;}
        public string Address { get; set; }
    }
}