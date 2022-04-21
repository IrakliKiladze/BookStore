using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Auth
{
    public class RegisterRequest
    {
        public User user { get; set; }
    }
}
