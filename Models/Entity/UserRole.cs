using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalUnion.Models.Entity
{
     public class UserRole: BaseEntity
    {
        public int UserId{ get; set; }
        public User User{ get; set; }
        public int RoleId{ get; set; }
        public Role Role{ get; set; }
    }
}