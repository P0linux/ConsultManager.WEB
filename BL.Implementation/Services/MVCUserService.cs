using BL.Abstraction;
using BL.DTO.Models;
using BL.Implementation.Extensions;
using DAL.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    public class MVCUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MVCUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SignInResult> Login(UserLoginModel userLoginModel)
        {
            var result = await _unitOfWork.SignInManager
                .PasswordSignInAsync(userLoginModel.Email, userLoginModel.Password, userLoginModel.RememberMe, false);

            return result;
        }

        public async Task<IdentityResult> Register(UserRegisterModel userRegisterModel)
        {
            var user = userRegisterModel.AdaptToUser();
            var result = await _unitOfWork.UserManager.CreateAsync(user, userRegisterModel.Password);

            if (result.Succeeded)
            {
                await _unitOfWork.UserManager.AddToRoleAsync(user, userRegisterModel.UserRole);
                await _unitOfWork.SignInManager.SignInAsync(user, false);
                return result;
            }

            else return result;
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            var roles = _unitOfWork.RoleManager.Roles;
            return roles.ToList();
        }

        public async Task SignOut()
        {
            await _unitOfWork.SignInManager.SignOutAsync();
        }
    }
}
