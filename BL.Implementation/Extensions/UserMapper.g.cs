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
            return p1 == null ? null : new User()
            {
                Id = p1.Id,
                FirstName = p1.FirstName,
                SecondName = p1.SecondName
            };
        }
        public static User AdaptTo(this UserDTO p2, User p3)
        {
            if (p2 == null)
            {
                return null;
            }
            User result = p3 ?? new User();

            result.Id = p2.Id;
            result.FirstName = p2.FirstName;
            result.SecondName = p2.SecondName;
            return result;
            
        }
        public static Expression<Func<UserDTO, User>> ProjectToUser => p4 => new User() 
        {
            Id = p4.Id,
            FirstName = p4.FirstName,
            SecondName = p4.SecondName
        };

        public static UserDTO AdaptToDTO(this User p5)
        {
            return p5 == null ? null : new UserDTO()
            {
                Id = p5.Id,
                FirstName = p5.FirstName,
                SecondName = p5.SecondName
            };
        }
        public static UserDTO AdaptTo(this User p6, UserDTO p7)
        {
            if (p6 == null)
            {
                return null;
            }
            UserDTO result = p7 ?? new UserDTO();

            result.Id = p6.Id;
            result.FirstName = p6.FirstName;
            result.SecondName = p6.SecondName;
            return result;
            
        }
        public static Expression<Func<User, UserDTO>> ProjectToDTO => p8 => new UserDTO() 
        {
            Id = p8.Id,
            FirstName = p8.FirstName,
            SecondName = p8.SecondName
        };

        public static User AdaptToUser(this UserRegisterModel p9)
        {
            return p9 == null ? null : new User()
            {
                FirstName = p9.FirstName,
                SecondName = p9.SecondName,
                UserName = p9.Email,
                Email = p9.Email
            };
        }
    }
}