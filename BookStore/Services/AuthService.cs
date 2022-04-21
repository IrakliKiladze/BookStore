using BookStore.Entities;
using BookStore.Helpers;
using BookStore.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace BookStore.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings _appSettings;
        private readonly IServiceProvider serviceProvider;

        public AuthService(IOptions<AppSettings> appSettings, IServiceProvider serviceProvider)
        {
            _appSettings = appSettings.Value;
            this.serviceProvider = serviceProvider;
        }
        public User GetById(int id)
        {
            using var db = serviceProvider.GetService<IBookStoreDb>();

            db.OpenConnectionAsync(CancellationToken.None).Wait();

            return db.GetUserByID(id,CancellationToken.None);
        }

        public LoginResponse Login(LoginRequest request)
        {
            using var db = serviceProvider.GetService<IBookStoreDb>();
            db.OpenConnectionAsync(CancellationToken.None).Wait();
            var user= db.GetUser(request.UserName, request.Password, CancellationToken.None);
           
            if (user == null) return null;            
            var token = generateJwtToken(user);

            return new LoginResponse(user, token);
        }

        public LoginResponse Register(RegisterRequest request)
        {
            using var db = serviceProvider.GetService<IBookStoreDb>();
            db.OpenConnectionAsync(CancellationToken.None).Wait();
            var userId = db.CreateUser(request.user, CancellationToken.None);

            if (userId == 0) return null;

            var token = generateJwtToken(request.user);

            return new LoginResponse(request.user, token);

        }



        private string generateJwtToken(User user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
