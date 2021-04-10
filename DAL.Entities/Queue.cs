using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Queue: BaseEntity
    {
        public IssueCategory IssueCategory { get; set; }
        public int Priority { get; set; }

        public Consultation Consultation { get; set; }
        public int ConsultationId { get; set; }
    }

    public enum IssueCategory
    {
        Theoretical,
        Code,
        Pass
    }
}
