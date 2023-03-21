using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalUnion.Models.DTOs
{
    public class AdminDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string RegNo { get; set; }
        public int UserId { get; set; }
        public IList<RoleDto> Roles { get; set; }
    }
    public class CreateAdminRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }
    public class UpdateAdminRequestModel
    {
        public int Id {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        
    }


}