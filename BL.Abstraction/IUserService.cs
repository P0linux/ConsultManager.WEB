using BL.DTO.Models;
using Microsoft.AspNetCore.Identity;
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
