using BL.Abstraction;
using BL.DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    class QueueService : IQueueService
    {
        public Task AddAsync(QueueDTO queue)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QueueDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QueueDTO>> GetByConsultationIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<QueueDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(QueueDTO queue)
        {
            throw new NotImplementedException();
        }
    }
}
