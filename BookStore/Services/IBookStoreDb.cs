using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IBookStoreDb : ISqlDb
    {
        public User GetUserByID(int UserId, CancellationToken cancellationToken);
        public User GetUser(string UserName, string Password, CancellationToken cancellationToken);
        public int CreateUser(User user, CancellationToken cancellationToken);


        List<Book> GetBooks(string Name, string Author, string ISBN, string Genre, CancellationToken cancellationToken);
        int SaveBook(Book book, CancellationToken cancellationToken);


        public List<User> GetUsers( CancellationToken cancellationToken);

    }
}