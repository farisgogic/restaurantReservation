using Microsoft.AspNetCore.Mvc;
using Restaurant_Services;

namespace RestaurantApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BaseCRUDController<T, TSearch, TInsert, TUpdate> : BaseController<T, TSearch> where T : class where TSearch : class where TInsert : class where TUpdate : class
    {

        public BaseCRUDController(ICRUDService<T, TSearch, TInsert, TUpdate> service) : base(service){}


        [HttpPost]
        public virtual T Insert([FromBody] TInsert insert)
        {
            var result = ((ICRUDService<T, TSearch, TInsert, TUpdate>)this.Service).Insert(insert);
            
            return result;
        }


        [HttpPut("{id}")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public virtual T Update(int id, [FromBody] TUpdate update)
        {
            var result = ((ICRUDService<T, TSearch, TInsert, TUpdate>)this.Service).Update(id, update);

            return result;
        }

        [HttpDelete("{id}")]
        public virtual IActionResult Delete(int id)
        {
            try
            {
                ((ICRUDService<T, TSearch, TInsert, TUpdate>)this.Service).Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
