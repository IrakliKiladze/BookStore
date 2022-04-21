using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Auth
{
    public class LoginResponse
    {
        public User user { get; set; }
        public string Token { get; set; }


        public LoginResponse(User _user, string token)
        {
            user = _user;
            Token = token;
        }
    }
}
