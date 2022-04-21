using BookStore.Entities;
using BookStore.Helpers;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class BookStoreDb :  SqlDb, IBookStoreDb
    {
        private readonly AppSettings _appSettings;
        private CancellationToken cancellationToken;

        public BookStoreDb(IOptions<AppSettings> appSettings) {
            _appSettings = appSettings.Value;
        }

        #region Auth
        public int CreateUser(User user, CancellationToken cancellationToken)
        {
            user.Password= HashPassword(user.Password);
            var cmd = new CommandDefinition(
              @"INSERT INTO [dbo].[Users]([FirstName],[LastName],[Username],[Password],[UserType])
                VALUES(@FirstName,@LastName,@Username,@Password,@UserType);
                select @@identity id", 
              new { user.FirstName,user.LastName,user.UserName,user.Password, user.UserType},
              cancellationToken: cancellationToken);

            return connection.QueryAsync<int>(cmd).Result.FirstOrDefault();
        }

        public User GetUser(string UserName, string Password, CancellationToken cancellationToken)
        {
            Password = HashPassword(Password);
            var cmd = new CommandDefinition(
              $"SELECT [Id],[FirstName],[LastName],[Username],[UserType] FROM Users (NOLOCK) where UserName=@UserName and Password=@Password;", new { UserName, Password },
              cancellationToken: cancellationToken);

            return connection.QueryAsync<User>(cmd).Result.FirstOrDefault();
        }

        public User GetUserByID(int UserId , CancellationToken cancellationToken)
        {
            var cmd = new CommandDefinition(
              $"SELECT [Id],[FirstName],[LastName],[Username],[UserType] FROM Users (NOLOCK) where Id=@UserId;", new { UserId },
              cancellationToken: cancellationToken);           

            return  connection.QueryAsync<User>(cmd).Result.FirstOrDefault();
        }

        public string HashPassword(string password)
        {
            //some Hash
            return password;
        }
        #endregion

        #region Books
        public List<Book> GetBooks( string Name, string Author, string ISBN, string Genre, CancellationToken cancellationToken)
        {
            var cmd = new CommandDefinition(
             $"SELECT [Id],[Name],[Author],[ISBN],[Genre],[Count]FROM [Books] (NOLOCK) where Name like '%'+@Name+'%' and  Author like '%'+@Author+'%'  and  ISBN like '%'+@ISBN+'%' and Genre  like '%'+@Genre+'%' ;", new { Name, Author, ISBN, Genre },
             cancellationToken: cancellationToken);

            return connection.QueryAsync<Book>(cmd).Result.ToList();
        }

        public int SaveBook(Book book, CancellationToken cancellationToken)
        {

            if (book.Id == 0)
            {
                var cmd = new CommandDefinition(
                 $"INSERT INTO [Books]([Name],[Author],[ISBN],[Genre],[Count])VALUES(@Name,@Author,@ISBN,@Genre,@Count);select @@identity id;", new { book.Name, book.Author, book.ISBN, book.Genre, book.Count },
                 cancellationToken: cancellationToken);

                return connection.QueryAsync<int>(cmd).Result.FirstOrDefault();
            }
            if (book.Id < 0)
            {
                var cmd = new CommandDefinition(
              $"delete [Books] WHERE [Id]=-@Id; select @Id Id;", new { book.Id },
              cancellationToken: cancellationToken);

                return connection.QueryAsync<int>(cmd).Result.FirstOrDefault();
            }
            else
            {
                var cmd = new CommandDefinition(
                $"UPDATE [dbo].[Books]SET [Name] = @Name,[Author] = @Author,[ISBN] = @ISBN,[Genre] = @Genre,[Count] = @Count WHERE [Id]=@Id; select @Id Id;", new { book.Name, book.Author, book.ISBN, book.Genre, book.Count, book.Id },
                cancellationToken: cancellationToken);
                return connection.QueryAsync<int>(cmd).Result.FirstOrDefault();
            }

        }
        #endregion

        #region Users
        public List<User> GetUsers(CancellationToken cancellationToken)
        {
          
            var cmd = new CommandDefinition(
              $"SELECT [Id],[FirstName],[LastName],[Username],[UserType] FROM Users (NOLOCK) ",
              cancellationToken: cancellationToken);

            return connection.QueryAsync<User>(cmd).Result.ToList();
        }
        #endregion
        protected override async Task<DbConnection> OpenConnectionCoreAsync(string connectionString, CancellationToken cancellationToken)
        {
            
            var connection = new SqlConnection(connectionString);

            await connection.OpenAsync(cancellationToken);

            return connection;
        }

        protected override Task<string> GetDefaultConnectionString()
        {           
            string connectionString = _appSettings.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception($"MS SQL connection string is not provided");

            return Task.FromResult(connectionString);
        }
    }
}
