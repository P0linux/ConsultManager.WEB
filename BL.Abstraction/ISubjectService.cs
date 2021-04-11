using BL.DTO.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Abstraction
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDTO>> GetAllAsync();
        Task AddAsync(SubjectDTO subject);
        Task DeleteByIdAsync(int id);
        Task<SubjectDTO> GetByIdAsync(int id);
        Task UpdateAsync(SubjectDTO subject);
    }
}
