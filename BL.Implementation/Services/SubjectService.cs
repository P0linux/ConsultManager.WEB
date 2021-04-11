using BL.Abstraction;
using BL.DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    class SubjectService : ISubjectService
    {
        public Task AddAsync(SubjectDTO subject)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SubjectDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SubjectDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SubjectDTO subject)
        {
            throw new NotImplementedException();
        }
    }
}
