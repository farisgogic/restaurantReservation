using AutoMapper;
using Restaurant_Model.Request;
using Restaurant_Model.SearchObjects;
using Restaurant_Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Services
{
    public class TableService : BaseCRUDService<Restaurant_Model.Table, Database.Table, TableSearchObject, TableUpsertRequest, TableUpsertRequest>, ITableService
    {
        public TableService(RestaurantDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

    }
}
