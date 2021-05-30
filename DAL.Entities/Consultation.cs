using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Consultation : BaseEntity
    {
        public DateTime Date { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string LecturerId { get; set; }

        [ForeignKey("LecturerId")]
        public User Lecturer { get; set; }
    }
}
