using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Services.Database
{
    public class Table
    {
        public int TableId { get; set; } 
        public int TableNumber { get; set; } 
        public int Capacity { get; set; }
        public bool isOccupied { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }

}
