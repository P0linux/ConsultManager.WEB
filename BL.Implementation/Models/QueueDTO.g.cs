using DAL.Entities;

namespace DAL.Entities
{
    public partial class QueueDTO
    {
        public IssueCategory IssueCategory { get; set; }
        public int Priority { get; set; }
        public int ConsultationId { get; set; }
        public int Id { get; set; }
    }
}