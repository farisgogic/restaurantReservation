using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Model.Request
{
    public class ReservationUpsertRequest
    {
        public int UserId { get; set; }
        public int TableId { get; set; }
        public DateTime DateReservation { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
