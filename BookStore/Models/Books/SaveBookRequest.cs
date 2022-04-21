using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Books
{
    public class SaveBookRequest
    {
        public Book book { get; set; }
    }
}
