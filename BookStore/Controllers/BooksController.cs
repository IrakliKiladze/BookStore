using BookStore.Models.Books;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
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
    public class BooksController : ControllerBase
    {
        private IBooksService  _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

      
        [HttpPost("GetBooks")]
        public GetBooksResponse GetBooks(GetBooksRequest request)
        {
            return _booksService.GetBooks(request);
        }

      
        [HttpPost("SaveBooks")]
        public SaveBookResponse SaveBooks(SaveBookRequest request)
        {
            return _booksService.SaveBook(request);
        }
    }
}