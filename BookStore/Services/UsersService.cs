using BookStore.Entities;
using BookStore.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace BookStore.Services
{
    public class UsersService: IUsersService
    {
        private readonly AppSettings _appSettings;
        private readonly IServiceProvider serviceProvider;

        public UsersService(IOptions<AppSettings> appSettings, IServiceProvider serviceProvider)
        {
            _appSettings = appSettings.Value;
            this.serviceProvider = serviceProvider;
        }

        public List<User> GetUsers() {
            using var db = serviceProvider.GetService<IBookStoreDb>();

            db.OpenConnectionAsync(CancellationToken.None).Wait();

            return  db.GetUsers(CancellationToken.None) ;
        }
    }
}
