using Restaurant_Model.Request;
using Restaurant_Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Services
{
    public interface ITableService: ICRUDService<Restaurant_Model.Table, TableSearchObject, TableUpsertRequest, TableUpsertRequest>
    {
    }
}
