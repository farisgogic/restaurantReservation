using AutoMapper;
using Restaurant_Model.SearchObjects;
using Restaurant_Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Services
{
    public class BaseService<T, TDb, TSearch> : IService<T, TSearch> where T : class where TDb : class where TSearch : BaseSearchObject
    {
        public RestaurantDbContext context;
        public IMapper mapper;

        public BaseService(RestaurantDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public virtual IEnumerable<T> Get(TSearch search = null)
        {
            var entity = context.Set<TDb>().AsQueryable();

            entity = AddFilter(entity, search);
            entity = AddInclude(entity, search);

            if (search.Page.HasValue == true && search.PageSize.HasValue == true)
            {
                entity = entity.Take(search.PageSize.Value).Skip(search.Page.Value * search.PageSize.Value);
            }

            var list = entity.ToList();
            return mapper.Map<IEnumerable<T>>(list);

        }



        public virtual IQueryable<TDb> AddInclude(IQueryable<TDb> query, TSearch search = null)
        {
            return query;
        }

        public virtual IQueryable<TDb> AddFilter(IQueryable<TDb> query, TSearch search = null)
        {
            return query;

        }

        public virtual T GetById(int id)
        {
            var set = context.Set<TDb>();

            var entity = set.Find(id);
            return mapper.Map<T>(entity);

        }
    }
}
