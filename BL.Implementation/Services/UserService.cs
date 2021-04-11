using BL.Abstraction;
using BL.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    class UserService : IUserService
    {
        public Task<SignInResult> Login(UserDTO userLoginModel)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> Register(UserDTO userRegisterModel)
        {
            throw new NotImplementedException();
        }

        public Task SignOut()
        {
            throw new NotImplementedException();
        }
    }
}
