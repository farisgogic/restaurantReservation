using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Restaurant_Model.Request
{
    public class UserInsertRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        public string Password { get; set; }

        public List<int> RolesIdList { get; set; } = new List<int> { };

    }
}
