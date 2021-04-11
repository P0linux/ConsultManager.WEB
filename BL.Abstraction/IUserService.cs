using BL.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Abstraction
{
    public interface IUserService
    {
        Task<IdentityResult> Register(UserDTO userRegisterModel);
        Task<SignInResult> Login(UserDTO userLoginModel);
        Task SignOut();
    }
}
