using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Services.Database
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }  
        public string UserName { get; set; } 
        public string PasswordHash { get; set; }  
        public string PasswordSalt { get; set; }

        
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
