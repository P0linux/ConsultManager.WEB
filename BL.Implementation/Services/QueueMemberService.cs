using BL.Abstraction;
using BL.DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    class QueueMemberService : IQueueMemberService
    {
        public Task AddAsync(QueueMemberDTO queueMember)
        {
            throw new NotImplementedException();
        }

        public void CalculatePriority(QueueMemberDTO queueMember)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QueueMemberDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<QueueMemberDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QueueMemberDTO>> GetByQueueIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void MarkAbsent(QueueMemberDTO queueMember)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(QueueMemberDTO queueMember)
        {
            throw new NotImplementedException();
        }
    }
}
