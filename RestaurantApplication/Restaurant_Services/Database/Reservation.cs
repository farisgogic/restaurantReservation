using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Services.Database
{
    public class Reservation
    {
        public int ReservationId { get; set; }  
        public int UserId { get; set; }
        public int TableId { get; set; }
        public DateTime DateReservation { get; set; }  
        public DateTime CreatedAt { get; set; } 

        public virtual Users User { get; set; }
        public virtual Table Table { get; set; }
    }
}
