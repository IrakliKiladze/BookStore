using BookStore.Helpers;
using BookStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddTransient<IBookStoreDb, BookStoreDb>();
        }

   
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
         
            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
