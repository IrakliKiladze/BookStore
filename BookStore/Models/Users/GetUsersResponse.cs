using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Users
{
    public class GetUsersResponse
    {
        public List<User> users { get; set; }
    }
}
