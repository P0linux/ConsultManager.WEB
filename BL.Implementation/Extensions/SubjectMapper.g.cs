using System;
using System.Linq.Expressions;
using BL.Implementation.Models;
using DAL.Entities;

namespace BL.Implementation.Extensions
{
    public static partial class SubjectMapper
    {
        public static Subject AdaptToSubject(this SubjectDTO p1)
        {
            return p1 == null ? null : new Subject()
            {
                Name = p1.Name,
                Id = p1.Id
            };
        }
        public static Subject AdaptTo(this SubjectDTO p2, Subject p3)
        {
            if (p2 == null)
            {
                return null;
            }
            Subject result = p3 ?? new Subject();
            
            result.Name = p2.Name;
            result.Id = p2.Id;
            return result;
            
        }
        public static Expression<Func<SubjectDTO, Subject>> ProjectToSubject => p4 => new Subject()
        {
            Name = p4.Name,
            Id = p4.Id
        };
        public static SubjectDTO AdaptToDTO(this Subject p5)
        {
            return p5 == null ? null : new SubjectDTO()
            {
                Name = p5.Name,
                Id = p5.Id
            };
        }
        public static SubjectDTO AdaptTo(this Subject p6, SubjectDTO p7)
        {
            if (p6 == null)
            {
                return null;
            }
            SubjectDTO result = p7 ?? new SubjectDTO();
            
            result.Name = p6.Name;
            result.Id = p6.Id;
            return result;
            
        }
        public static Expression<Func<Subject, SubjectDTO>> ProjectToDTO => p8 => new SubjectDTO()
        {
            Name = p8.Name,
            Id = p8.Id
        };
    }
}