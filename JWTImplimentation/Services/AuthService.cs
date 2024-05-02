using JWTImplimentation.Context;
using JWTImplimentation.Interfaces;
using JWTImplimentation.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTImplimentation.Services
{
    public class AuthService : IAuthService
    {
        private readonly JWTContext _db;
        private readonly IConfiguration _configuration;
        public AuthService(JWTContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public User AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User object cannot be null.");
            }

            var userAdded = _db.Users.Add(user);
            _db.SaveChanges();

            if (userAdded.Entity == null)
            {
                throw new Exception("Failed to add user.");
            }

            return userAdded.Entity;

        }

        public string Login(LoginRequest request)
        {
            if (request.UserName != null && request.Password != null)
            {
                var user = _db.Users.SingleOrDefault(s => s.Email == request.UserName && s.Password == request.Password);
                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub,_configuration["JwtSettings:Subject"]),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("UserName",user.Name),
                        new Claim("Email",user.Email)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_secret_key_1234567890"));
                    var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["JwtSettings:Issuer"],
                        _configuration["JwtSettings:Audience"],
                        claims,
                        DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signin
                        );
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;

                }
                else
                {
                    throw new Exception("Credentials does not match");
                }
            }
            else
            {
                throw new Exception("Credentials does not match");
            }

        }
    }
}
