using Restaurant_Model.Request;
using Restaurant_Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Services
{
    public interface IUserService: ICRUDService<Restaurant_Model.Users, UserSearchObject, UserInsertRequest, UserUpdateRequest>
    {
        Restaurant_Model.Users Login(string username, string password);

    }
}
