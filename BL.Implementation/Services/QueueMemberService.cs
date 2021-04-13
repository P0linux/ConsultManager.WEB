using BL.Abstraction;
using BL.DTO.Models;
using BL.Implementation.Extensions;
using DAL.Abstraction;
using System;
using System.Collections.Generic;
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

        public void CalculatePriority(QueueMemberDTO queueMember)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var queueMember = await _unitOfWork.QueueMemberRepository.GetByIdAsync(id);
            _unitOfWork.QueueMemberRepository.Delete(queueMember);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<QueueMemberDTO>> GetAllAsync()
        {
            var queueMembers = await _unitOfWork.QueueMemberRepository.GetAllAsync();
            return;
        }

        public async Task<QueueMemberDTO> GetByIdAsync(int id)
        {
            var queueMember = await _unitOfWork.QueueMemberRepository.GetByIdAsync(id);
            return consult.AdaptToDTO();
        }

        public Task<IEnumerable<QueueMemberDTO>> GetByQueueIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void MarkAbsent(QueueMemberDTO queueMember)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(QueueMemberDTO queueMember)
        {
            _unitOfWork.QueueMemberRepository.Update(queueMember.AdaptToQueueMember());

            await _unitOfWork.CommitAsync();
        }
    }
}
