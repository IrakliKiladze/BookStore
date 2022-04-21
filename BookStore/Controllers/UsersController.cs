using BookStore.Models.Users;
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
    [Authorize]
    public class UsersController : ControllerBase
    {

        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }


        [HttpGet("GetUsers")]
        public GetUsersResponse GetUsers()
        {

            return new GetUsersResponse() { users = _usersService.GetUsers() };
        }

    }
}
