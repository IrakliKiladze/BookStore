using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public int Count { get; set; }
    }
}