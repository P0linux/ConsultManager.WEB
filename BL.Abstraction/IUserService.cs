using BL.DTO.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BL.Abstraction
{
    public interface IUserService
    {
        Task<object> Register(UserRegisterModel userRegisterModel);
        Task<object> Login(UserLoginModel userLoginModel);
        Task SignOut();
    }
}
