using System;
using System.Linq.Expressions;
using BL.DTO.Models;
using DAL.Entities;

namespace BL.Implementation.Extensions
{
    public static partial class ConsultationMapper
    {
        public static Consultation AdaptToConsultation(this ConsultationDTO p1)
        {
            return p1 == null ? null : new Consultation()
            {
                Date = p1.Date,
                SubjectId = p1.SubjectId,
                LecturerId = p1.LecturerId,
                Id = p1.Id
            };
        }
        public static Consultation AdaptTo(this ConsultationDTO p2, Consultation p3)
        {
            if (p2 == null)
            {
                return null;
            }
            Consultation result = p3 ?? new Consultation();
            
            result.Date = p2.Date;
            result.SubjectId = p2.SubjectId;
            result.LecturerId = p2.LecturerId;
            result.Id = p2.Id;
            return result;
            
        }
        public static Expression<Func<ConsultationDTO, Consultation>> ProjectToConsultation => p4 => new Consultation()
        {
            Date = p4.Date,
            SubjectId = p4.SubjectId,
            LecturerId = p4.LecturerId,
            Id = p4.Id
        };
        public static ConsultationDTO AdaptToDTO(this Consultation p5)
        {
            return p5 == null ? null : new ConsultationDTO()
            {
                Date = p5.Date,
                SubjectId = p5.SubjectId,
                LecturerId = p5.LecturerId,
                Id = p5.Id
            };
        }
        public static ConsultationDTO AdaptTo(this Consultation p6, ConsultationDTO p7)
        {
            if (p6 == null)
            {
                return null;
            }
            ConsultationDTO result = p7 ?? new ConsultationDTO();
            
            result.Date = p6.Date;
            result.SubjectId = p6.SubjectId;
            result.LecturerId = p6.LecturerId;
            result.Id = p6.Id;
            return result;
            
        }
        public static Expression<Func<Consultation, ConsultationDTO>> ProjectToDTO => p8 => new ConsultationDTO()
        {
            Date = p8.Date,
            SubjectId = p8.SubjectId,
            LecturerId = p8.LecturerId,
            Id = p8.Id
        };
    }
}