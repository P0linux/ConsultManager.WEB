using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class QueueMember: BaseEntity
    {
        public int Priority { get; set; }
        public TimeSpan TimeInterval { get; set; }
        public bool IsAbsent { get; set; }

        public Queue Queue { get; set; }
        public int QueueId { get; set; }
        public  User StudentUser { get; set; }
        public int StudentUserId { get; set; }
    }
}
