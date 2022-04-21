using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Auth
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
