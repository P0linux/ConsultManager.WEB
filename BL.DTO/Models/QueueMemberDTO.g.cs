using System;

namespace BL.DTO.Models
{
    public partial class QueueMemberDTO
    {
        public int Priority { get; set; }
        public TimeSpan TimeInterval { get; set; }
        public bool IsAbsent { get; set; }
        public int QueueId { get; set; }
        public int StudentId { get; set; }
        public int Id { get; set; }
    }
}