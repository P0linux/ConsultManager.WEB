using BL.Abstraction;
using BL.DTO.Models;
using BL.Implementation.Extensions;
using DAL.Abstraction;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SignInResult> Login(UserLoginModel userLoginModel)
        {
            var res = await _unitOfWork.SignInManager
                .PasswordSignInAsync(userLoginModel.Email, userLoginModel.Password, userLoginModel.RememberMe, false);
            return res;

        }

        public async Task<IdentityResult> Register(UserRegisterModel userRegisterModel)
        {
            var user = userRegisterModel.AdaptToUser();
            var res = await _unitOfWork.UserManager.CreateAsync(user, userRegisterModel.Password);
            await _unitOfWork.UserManager.AddToRoleAsync(user, userRegisterModel.UserRole);
            return res;
        }

        public async Task SignOut()
        {
            await _unitOfWork.SignInManager.SignOutAsync();
        }
    }
}
