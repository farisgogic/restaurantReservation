using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant_Model
{
    public class Users
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public string UlogaIme => string.Join(" ", UserRoles?.Select(x => x.Role?.RoleName)?.ToList());
    }
}
