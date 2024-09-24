using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Model
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int TableId { get; set; }
        public DateTime DateReservation { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
