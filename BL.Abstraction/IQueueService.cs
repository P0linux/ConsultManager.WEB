using BL.DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Abstraction
{
    public interface IQueueService
    {
        Task<IEnumerable<QueueDTO>> GetAllAsync();
        Task AddAsync(QueueDTO queue);
        Task DeleteByIdAsync(int id);
        Task<QueueDTO> GetByIdAsync(int id);
        Task UpdateAsync(QueueDTO queue);
        Task<IEnumerable<QueueDTO>> GetByConsultationIdAsync(int id);
    }
}
