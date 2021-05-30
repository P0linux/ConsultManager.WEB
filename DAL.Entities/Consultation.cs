using System;

namespace DAL.Entities
{
    public class Consultation : BaseEntity
    {
        public DateTime Date { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string LecturerId { get; set; }
        public User Lecturer { get; set; }
    }
}
