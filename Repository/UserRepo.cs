using AuthorizationMicroservice.Models;
using AuthorizationMicroservice.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace AuthorizationMicroservice.Repository
{
    public class UserRepo : IUserRepo
    {

       
        private readonly AuthContext _context;
        private readonly IConfiguration _configuration;

        public UserRepo()
        {
        }

        public UserRepo(AuthContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public User AuthenticateUser(User login)
        {
            User user = _context.Users.FirstOrDefault(z => z.EmployeeId == login.EmployeeId && z.Password == login.Password);
            return user;
        }

        public string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
