using Microsoft.AspNetCore.Mvc;
using Restaurant_Model;
using Restaurant_Model.Request;
using Restaurant_Model.SearchObjects;
using Restaurant_Services;

namespace RestaurantApplication.Controllers
{
    public class TableController : BaseCRUDController<Restaurant_Model.Table, TableSearchObject, TableUpsertRequest, TableUpsertRequest>
    {
        public TableController(ITableService service) : base(service)
        {
        }
    }
}
