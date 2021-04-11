using BL.Abstraction;
using BL.DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    class ConsultationService : IConsultationService
    {
        public Task AddAsync(ConsultationDTO consultation)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ConsultationDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ConsultationDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ConsultationDTO consultation)
        {
            throw new NotImplementedException();
        }
    }
}
