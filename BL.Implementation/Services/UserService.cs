using BL.Abstraction;
using BL.DTO.Models;
using BL.Implementation.Extensions;
using DAL.Abstraction;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;

        public UserService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;
        }
        public async Task<object> Login(UserLoginModel userLoginModel)
        {
            var res = await _unitOfWork.SignInManager
                .PasswordSignInAsync(userLoginModel.Email, userLoginModel.Password, userLoginModel.RememberMe, false);
            if (res.Succeeded)
            {
                var user = await _unitOfWork.UserManager.Users.SingleOrDefaultAsync(u => u.Email == userLoginModel.Email);

                return GenerateJwtToken(userLoginModel.Email, user.AdaptToDTO());
            }

            throw new Exception("Login failed");
        }

        public async Task<object> Register(UserRegisterModel userRegisterModel)
        {
            var user = userRegisterModel.AdaptToUser();
            var res = await _unitOfWork.UserManager.CreateAsync(user, userRegisterModel.Password);

            if (res.Succeeded)
            {
                await _unitOfWork.UserManager.AddToRoleAsync(user, userRegisterModel.UserRole);
                return GenerateJwtToken(userRegisterModel.Email, user.AdaptToDTO());
            }

            throw new Exception("Registration failed");
        }

        public async Task SignOut()
        {
            await _unitOfWork.SignInManager.SignOutAsync();
        }

        private object GenerateJwtToken(string email, UserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.Add(_jwtSettings.Lifetime);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
