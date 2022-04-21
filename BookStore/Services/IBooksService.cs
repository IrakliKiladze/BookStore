using BookStore.Models.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBooksService
    {
        public GetBooksResponse GetBooks(GetBooksRequest request);
        public SaveBookResponse SaveBook(SaveBookRequest request);


    }
}
