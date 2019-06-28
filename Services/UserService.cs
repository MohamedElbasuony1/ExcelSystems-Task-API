using AutoMapper;
using Contracts;
using DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasherService _passwordHasher;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
                           PasswordHasherService passwordHasher,
                           IConfiguration config, 
                           IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _config = config;
        }

        public UserTokenModel BuildToken(UserTokenModel user)
        {
            var claims = new[] {
                        new Claim("UserID",user.ID.ToString())
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddDays(7),
              signingCredentials: creds
              );
            user.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return user;
        }

        public async Task<User> Authenticate(UserModel loginuser)
        {
            return await _userRepository
                .FindByAsync(a => a.UserName == loginuser.UserName 
                             && _passwordHasher.VerifyPassword(a.Password, loginuser.Password)
                             && a.IsActive==true);
        }

        public async Task<User> SignUp(User user)
        {
            user.Password = _passwordHasher.HashPassword(user.Password);
            user.IsActive = true;
            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> IsExistByUserName(string username)
        {
          return  await _userRepository.ExistAsync(a => a.UserName == username);
        }

        public async Task<bool> IsExistByID(int userid)
        {
            return await _userRepository.ExistAsync(a => a.ID == userid);
        }
    }
}

