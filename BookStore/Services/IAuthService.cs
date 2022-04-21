using BookStore.Entities;
using BookStore.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IAuthService
    {
        public User GetById(int id);
        public LoginResponse Login(LoginRequest request);
        public LoginResponse Register(RegisterRequest request);



    }
}
