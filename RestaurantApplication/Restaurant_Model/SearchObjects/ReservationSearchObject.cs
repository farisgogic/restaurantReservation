using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Model.SearchObjects
{
    public class ReservationSearchObject: BaseSearchObject
    {
        public int? UserId { get; set; }
        public int? TableId { get; set; }
        public DateTime? DateReservation { get; set; }

    }
}
