using System;

namespace BL.Implementation.Models
{
    public partial class ConsultationDTO
    {
        public DateTime Date { get; set; }
        public int SubjectId { get; set; }
        public int LecturerId { get; set; }
        public int Id { get; set; }
    }
}