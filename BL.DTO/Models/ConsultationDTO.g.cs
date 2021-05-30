using System;

namespace BL.DTO.Models
{
    public partial class ConsultationDTO
    {
        public DateTime Date { get; set; }
        public int SubjectId { get; set; }
        public SubjectDTO Subject { get; set; }
        public string LecturerId { get; set; }
        public UserDTO Lecturer { get; set; }
        public int Id { get; set; }
    }
}