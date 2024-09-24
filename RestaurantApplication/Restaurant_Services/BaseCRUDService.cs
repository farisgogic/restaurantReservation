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
    public class BaseCRUDService<T, TDb, TSearch, TInsert, TUpdate>
        : BaseService<T, TDb, TSearch>, ICRUDService<T, TSearch, TInsert, TUpdate>
        where T : class where TDb : class where TSearch : BaseSearchObject where TInsert : class where TUpdate : class
    {
        public BaseCRUDService(RestaurantDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public virtual T Insert(TInsert insert)
        {
            var set = context.Set<TDb>();

            TDb entity = mapper.Map<TDb>(insert);

            set.Add(entity);

            BeforeInsert(insert, entity);

            context.SaveChanges();

            return mapper.Map<T>(entity);
        }

        public virtual void BeforeInsert(TInsert insert, TDb entity)
        { }


        public virtual T Update(int id, TUpdate update)
        {
            var set = context.Set<TDb>();

            var entity = set.Find(id);

            if (entity != null)
            {
                mapper.Map(update, entity);
            }
            else
            {
                return null;
            }

            context.SaveChanges();
            return mapper.Map<T>(entity);

        }

        public virtual void Delete(int id)
        {
            var set = context.Set<TDb>();
            var entity = set.Find(id);

            if (entity != null)
            {
                set.Remove(entity);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Entity not found");
            }
        }

    }
}
