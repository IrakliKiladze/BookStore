using BookStore.Models;
using BookStore.Models.Auth;
using BookStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("Login")]
        public LoginResponse Login(LoginRequest request)
        {
            return _authService.Login(request);
        }


        [HttpPost("Register")]
        public LoginResponse Register(RegisterRequest request)
        {

            return _authService.Register(request);
        }

        

    }
}
