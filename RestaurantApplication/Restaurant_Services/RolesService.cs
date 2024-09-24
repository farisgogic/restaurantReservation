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
    public class RolesService : BaseCRUDService<Restaurant_Model.Roles, Database.Role, RolesSearchObject, RolesUpsertRequest, RolesUpsertRequest>, IRolesService
    {
        public RolesService(RestaurantDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IQueryable<Role> AddFilter(IQueryable<Role> query, RolesSearchObject search = null)
        {
            var filteredQuery = base.AddFilter(query, search);

            if(!string.IsNullOrWhiteSpace(search?.RoleName))
            {
                filteredQuery = filteredQuery.Where(x=>x.RoleName ==  search.RoleName);
            }

            return filteredQuery;
        }
    }
}
