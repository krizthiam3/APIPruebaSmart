using Entities;
using Entities.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PruebaAPI.Data;
using Repository.IRepositorio;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _config = configuration;
        }

        ICollection<User> IUserRepository.GetAllUSer()
        {
            return _db.user.OrderBy(c => c.UserId).ToList();
        }

        User IUserRepository.GetUserById(int id)
        {
            return _db.user.FirstOrDefault(c => c.UserId == id);
        }

        bool IUserRepository.IsUniqueUser(string userName)
        {
            bool valor = _db.user.Any(p => p.UserName == userName);
            return valor;
        }

        UserLoginResponseDto IUserRepository.Login(UserLoginDto userLogin)
        {
            var usuaio =  _db.user.FirstOrDefault(c => c.UserName == userLogin.UserName && c.Password == userLogin.Password);

            if(usuaio == null)
            {
                return new UserLoginResponseDto() { Token = "", User = null };
            }

            var claveSecreta = _config.GetSection("ApiSetting:ClaveSecreta").Value; 
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescription = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(
                new Claim[]{
                    new Claim(ClaimTypes.Name, usuaio.UserName),
                    new Claim(ClaimTypes.Role, usuaio.Role)

                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = manejadorToken.CreateToken(tokenDescription);

            UserLoginResponseDto userLoginResponse = new UserLoginResponseDto()
            {
                Token = manejadorToken.WriteToken(token),
                User = usuaio
            };

            return userLoginResponse;
        }

    }
}
