using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NationalUnion.Models.DTOs
{
    public class UserDTO
    {
        public int Id {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber{get; set;}
        public double Wallet{get; set;}
        public string Address { get; set; }
        public IList<RoleDto> Roles {get;set;} = new List<RoleDto>(); 
    } 

    public class LoginUserRequestModel
    {
        public string Email {get; set;}
        public string Password {get; set;}
    }

    public class LoginUserResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber{get; set;}
        public IList<RoleDto> Roles {get;set;}
    }
}