using BL.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Abstraction
{
    public interface IConsultationService
    {
        Task<IEnumerable<ConsultationDTO>> GetAllAsync();
        Task AddAsync(ConsultationDTO consultation);
        Task DeleteByIdAsync(int id);
        Task<ConsultationDTO> GetByIdAsync(int id);
        Task UpdateAsync(ConsultationDTO consultation);
        bool ConsultationExists(int id);
    }
}
