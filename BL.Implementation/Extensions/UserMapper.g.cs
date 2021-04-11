using System;
using System.Linq.Expressions;
using BL.DTO.Models;
using DAL.Entities;

namespace BL.Implementation.Extensions
{
    public static partial class UserMapper
    {
        public static User AdaptToUser(this UserDTO p1)
        {
            return p1 == null ? null : new User() {};
        }
        public static User AdaptTo(this UserDTO p2, User p3)
        {
            if (p2 == null)
            {
                return null;
            }
            User result = p3 ?? new User();
            return result;
            
        }
        public static Expression<Func<UserDTO, User>> ProjectToUser => p4 => new User() {};
        public static UserDTO AdaptToDTO(this User p5)
        {
            return p5 == null ? null : new UserDTO() {};
        }
        public static UserDTO AdaptTo(this User p6, UserDTO p7)
        {
            if (p6 == null)
            {
                return null;
            }
            UserDTO result = p7 ?? new UserDTO();
            return result;
            
        }
        public static Expression<Func<User, UserDTO>> ProjectToDTO => p8 => new UserDTO() {};
    }
}