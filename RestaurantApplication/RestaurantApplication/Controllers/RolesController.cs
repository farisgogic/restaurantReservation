using Microsoft.AspNetCore.Mvc;
using Restaurant_Model;
using Restaurant_Model.Request;
using Restaurant_Model.SearchObjects;
using Restaurant_Services;

namespace RestaurantApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseCRUDController<Restaurant_Model.Roles, RolesSearchObject, RolesUpsertRequest, RolesUpsertRequest>
    {
        public RolesController(IRolesService service) : base(service)
        {
        }
    }
}
