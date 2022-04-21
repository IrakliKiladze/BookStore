using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Books
{
    public class GetBooksRequest
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
    }
}
