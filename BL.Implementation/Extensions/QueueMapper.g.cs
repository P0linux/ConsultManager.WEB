using System;
using System.Linq.Expressions;
using BL.DTO;
using BL.DTO.Models;
using DAL.Entities;

namespace BL.Implementation.Extensions
{
    public static partial class QueueMapper
    {
        public static Queue AdaptToQueue(this QueueDTO p1)
        {
            return p1 == null ? null : new Queue()
            {
                IssueCategory = p1.IssueCategory.ToString(),
                Priority = p1.Priority,
                ConsultationId = p1.ConsultationId,
                Id = p1.Id,
                Consultation = p1.Consultation.AdaptToConsultation()
            };
        }
        public static Queue AdaptTo(this QueueDTO p2, Queue p3)
        {
            if (p2 == null)
            {
                return null;
            }
            Queue result = p3 ?? new Queue();
            
            result.IssueCategory = p2.IssueCategory.ToString();
            result.Priority = p2.Priority;
            result.ConsultationId = p2.ConsultationId;
            result.Consultation = p2.Consultation.AdaptToConsultation();
            result.Id = p2.Id;
            return result;
            
        }
        public static Expression<Func<QueueDTO, Queue>> ProjectToQueue => p4 => new Queue()
        {
            IssueCategory = p4.IssueCategory.ToString(),
            Priority = p4.Priority,
            ConsultationId = p4.ConsultationId,
            Consultation = p4.Consultation.AdaptToConsultation(),
            Id = p4.Id
        };
        public static QueueDTO AdaptToDTO(this Queue p5)
        {
            return p5 == null ? null : new QueueDTO()
            {
                IssueCategory = (IssueCategory)Enum.Parse(typeof(IssueCategory), p5.IssueCategory),
                Priority = p5.Priority,
                ConsultationId = p5.ConsultationId,
                Consultation = p5.Consultation.AdaptToDTO(),
                Id = p5.Id
            };
        }
        public static QueueDTO AdaptTo(this Queue p6, QueueDTO p7)
        {
            if (p6 == null)
            {
                return null;
            }
            QueueDTO result = p7 ?? new QueueDTO();
            
            result.IssueCategory = (IssueCategory)Enum.Parse(typeof(IssueCategory) ,p6.IssueCategory);
            result.Priority = p6.Priority;
            result.ConsultationId = p6.ConsultationId;
            result.Consultation = p6.Consultation.AdaptToDTO();
            result.Id = p6.Id;
            return result;
            
        }
        public static Expression<Func<Queue, QueueDTO>> ProjectToDTO => p8 => new QueueDTO()
        {
            IssueCategory = (IssueCategory)Enum.Parse(typeof(IssueCategory), p8.IssueCategory),
            Priority = p8.Priority,
            ConsultationId = p8.ConsultationId,
            Consultation = p8.Consultation.AdaptToDTO(),
            Id = p8.Id
        };
    }
}