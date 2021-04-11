using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Abstraction
{
    public interface IConsultationService
    {
        Task<IEnumerable<ConsultationDTO>>
    }
}
