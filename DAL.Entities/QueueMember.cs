using System;

namespace DAL.Entities
{
    public class QueueMember : BaseEntity
    {
        public int Priority { get; set; } = default;
        public int TimeInterval { get; set; }
        public bool IsAbsent { get; set; } = false;

        public Queue Queue { get; set; }
        public int QueueId { get; set; }
        public User Student { get; set; }
        public string StudentId { get; set; }
    }
}
