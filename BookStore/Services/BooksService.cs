using BookStore.Helpers;
using BookStore.Models.Books;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace BookStore.Services
{
    public class BooksService : IBooksService
    {
        private readonly AppSettings _appSettings;
        private readonly IServiceProvider serviceProvider;

        public BooksService(IOptions<AppSettings> appSettings, IServiceProvider serviceProvider)
        {
            _appSettings = appSettings.Value;
            this.serviceProvider = serviceProvider;
        }
        public GetBooksResponse GetBooks(GetBooksRequest request)
        {
            using var db = serviceProvider.GetService<IBookStoreDb>();

            db.OpenConnectionAsync(CancellationToken.None).Wait();

            return new GetBooksResponse() { books = db.GetBooks(request.Name, request.Author, request.ISBN, request.Genre, CancellationToken.None) };
        }

        public SaveBookResponse SaveBook(SaveBookRequest request)
        {
            using var db = serviceProvider.GetService<IBookStoreDb>();

            db.OpenConnectionAsync(CancellationToken.None).Wait();

            return new SaveBookResponse() {  BookId = db.SaveBook(request.book, CancellationToken.None) };
        }
    }
}
