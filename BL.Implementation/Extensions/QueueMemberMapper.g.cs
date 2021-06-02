using System;
using System.Linq.Expressions;
using BL.DTO.Models;
using DAL.Entities;

namespace BL.Implementation.Extensions
{
    public static partial class QueueMemberMapper
    {
        public static QueueMember AdaptToQueueMember(this QueueMemberDTO p1)
        {
            return p1 == null ? null : new QueueMember()
            {
                Priority = p1.Priority,
                TimeInterval = p1.TimeInterval,
                IsAbsent = p1.IsAbsent,
                QueueId = p1.QueueId,
                Queue = p1.Queue.AdaptToQueue(),
                StudentId = p1.StudentId,
                Student = p1.Student.AdaptToUser(),
                Id = p1.Id
            };
        }
        public static QueueMember AdaptTo(this QueueMemberDTO p2, QueueMember p3)
        {
            if (p2 == null)
            {
                return null;
            }
            QueueMember result = p3 ?? new QueueMember();
            
            result.Priority = p2.Priority;
            result.TimeInterval = p2.TimeInterval;
            result.IsAbsent = p2.IsAbsent;
            result.QueueId = p2.QueueId;
            result.Queue = p2.Queue.AdaptToQueue();
            result.StudentId = p2.StudentId;
            result.Student = p2.Student.AdaptToUser();
            result.Id = p2.Id;
            return result;
            
        }
        public static Expression<Func<QueueMemberDTO, QueueMember>> ProjectToQueueMember => p4 => new QueueMember()
        {
            Priority = p4.Priority,
            TimeInterval = p4.TimeInterval,
            IsAbsent = p4.IsAbsent,
            QueueId = p4.QueueId,
            Queue = p4.Queue.AdaptToQueue(),
            StudentId = p4.StudentId,
            Student = p4.Student.AdaptToUser(),
            Id = p4.Id
        };
        public static QueueMemberDTO AdaptToDTO(this QueueMember p5)
        {
            return p5 == null ? null : new QueueMemberDTO()
            {
                Priority = p5.Priority,
                TimeInterval = p5.TimeInterval,
                IsAbsent = p5.IsAbsent,
                QueueId = p5.QueueId,
                Queue = p5.Queue.AdaptToDTO(),
                StudentId = p5.StudentId,
                Student = p5.Student.AdaptToDTO(),
                Id = p5.Id
            };
        }
        public static QueueMemberDTO AdaptTo(this QueueMember p6, QueueMemberDTO p7)
        {
            if (p6 == null)
            {
                return null;
            }
            QueueMemberDTO result = p7 ?? new QueueMemberDTO();
            
            result.Priority = p6.Priority;
            result.TimeInterval = p6.TimeInterval;
            result.IsAbsent = p6.IsAbsent;
            result.QueueId = p6.QueueId;
            result.Queue = p6.Queue.AdaptToDTO();
            result.StudentId = p6.StudentId;
            result.Student = p6.Student.AdaptToDTO();
            result.Id = p6.Id;
            return result;
            
        }
        public static Expression<Func<QueueMember, QueueMemberDTO>> ProjectToDTO => p8 => new QueueMemberDTO()
        {
            Priority = p8.Priority,
            TimeInterval = p8.TimeInterval,
            IsAbsent = p8.IsAbsent,
            QueueId = p8.QueueId,
            Queue = p8.Queue.AdaptToDTO(),
            StudentId = p8.StudentId,
            Student = p8.Student.AdaptToDTO(),
            Id = p8.Id
        };
    }
}