using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Model
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
