using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    class Consultation: BaseEntity
    {
        public DateTime Date  { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int LecturerUserId { get; set; }
        public User LecturerUser { get; set; }
    }
}
