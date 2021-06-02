using System;

namespace BL.DTO.Models
{
    public partial class QueueMemberDTO
    {
        public int Priority { get; set; } = default;
        public int TimeInterval { get; set; }
        public bool IsAbsent { get; set; } = false;
        public int QueueId { get; set; }
        public QueueDTO Queue { get; set; }
        public string StudentId { get; set; }
        public UserDTO Student { get; set; }
        public int Id { get; set; }
    }
}