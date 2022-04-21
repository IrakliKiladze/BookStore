using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IUsersService
    {
        public List<User> GetUsers();
    }
}
