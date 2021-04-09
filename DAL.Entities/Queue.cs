using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    class Queue: BaseEntity
    {
        public IssueCategory IssueCategory { get; set; }
        public int Priority { get; set; }

        public Consultation Consultation { get; set; }
        public int ConsultationId { get; set; }
    }

    enum IssueCategory
    {
        Theoretical,
        Code,
        Pass
    }
}
