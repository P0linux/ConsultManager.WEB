using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Abstraction
{
    public interface IUserService
    {
        Task<IdentityResult> Register(UserRegisterModel userRegisterModel);
        Task<SignInResult> Login(UserLoginModel userLoginModel);
        Task SignOut();
    }
}
