using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Restaurant_Model
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }

        public int RoleId { get; set; }
        public Roles Role { get; set; }
    }
}
