using BL.Abstraction;
using BL.DTO.Models;
using BL.Implementation.Extensions;
using DAL.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    class QueueMemberService : IQueueMemberService
    {
        IUnitOfWork _unitOfWork;
        public QueueMemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(QueueMemberDTO queueMember)
        {
            await _unitOfWork.QueueMemberRepository.InsertAsync(queueMember.AdaptToQueueMember());
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var queueMember = await _unitOfWork.QueueMemberRepository.GetByIdAsync(id);
            _unitOfWork.QueueMemberRepository.Delete(queueMember);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<QueueMemberDTO>> GetAllAsync()
        {
            var queueMembers = _unitOfWork.QueueMemberRepository.GetAllAsync()
                                          .Select(QueueMemberMapper.ProjectToDTO);

            return await queueMembers.ToListAsync();
        }

        public async Task<QueueMemberDTO> GetByIdAsync(int id)
        {
            var queueMember = await _unitOfWork.QueueMemberRepository.GetByIdAsync(id);
            return queueMember.AdaptToDTO();
        }

        public async Task<IEnumerable<QueueMemberDTO>> GetByQueueIdAsync(int id)
        {
            var queueMembers = _unitOfWork.QueueMemberRepository
                .GetAllAsync(qm => qm.QueueId == id)
                .Select(QueueMemberMapper.ProjectToDTO);

            return await queueMembers.ToListAsync();
        }

        public async Task UpdateAsync(QueueMemberDTO queueMember)
        {
            _unitOfWork.QueueMemberRepository.Update(queueMember.AdaptToQueueMember());

            await _unitOfWork.CommitAsync();
        }

        public void CalculatePriority(QueueMemberDTO queueMember)
        {
            throw new NotImplementedException();
        }

        public void MarkAbsent(QueueMemberDTO queueMember)
        {
            throw new NotImplementedException();
        }
    }
}
