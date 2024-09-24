using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Restaurant_Model.Request
{
    public class LoginResponse
    {
        public Restaurant_Model.Users User { get; set; }
        public string Token { get; set; }
    }
}
