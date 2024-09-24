using Microsoft.AspNetCore.Mvc;
using Restaurant_Model;
using Restaurant_Model.Request;
using Restaurant_Model.SearchObjects;
using Restaurant_Services;

namespace RestaurantApplication.Controllers
{
    public class ReservationController : BaseCRUDController<Restaurant_Model.Reservation, ReservationSearchObject, ReservationUpsertRequest, ReservationUpsertRequest>
    {
        public ReservationController(IReservationService service) : base(service)
        {
        }
    }
}
