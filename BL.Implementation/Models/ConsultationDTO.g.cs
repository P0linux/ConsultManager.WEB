using System;

namespace DAL.Entities
{
    public partial class ConsultationDTO
    {
        public DateTime Date { get; set; }
        public int SubjectId { get; set; }
        public int LecturerId { get; set; }
        public int Id { get; set; }
    }
}