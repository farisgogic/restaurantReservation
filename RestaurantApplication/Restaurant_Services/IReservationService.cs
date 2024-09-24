using Restaurant_Model.Request;
using Restaurant_Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Services
{
    public interface IReservationService: ICRUDService<Restaurant_Model.Reservation, ReservationSearchObject, ReservationUpsertRequest, ReservationUpsertRequest>
    {
    }
}
