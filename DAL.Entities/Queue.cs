namespace DAL.Entities
{
    public class Queue : BaseEntity
    {
        public string IssueCategory { get; set; }
        public int Priority { get; set; }

        public Consultation Consultation { get; set; }
        public int ConsultationId { get; set; }
    }
}
