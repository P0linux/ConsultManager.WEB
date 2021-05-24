using BL.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Abstraction
{
    public interface IQueueMemberService
    {
        Task<IEnumerable<QueueMemberDTO>> GetAllAsync();
        Task AddAsync(QueueMemberDTO queueMember);
        Task DeleteByIdAsync(int id);
        Task<QueueMemberDTO> GetByIdAsync(int id);
        Task UpdateAsync(QueueMemberDTO queueMember);
        Task<IEnumerable<QueueMemberDTO>> GetByQueueIdAsync(int id);
        void MarkAbsent(QueueMemberDTO queueMember);
    }
}
