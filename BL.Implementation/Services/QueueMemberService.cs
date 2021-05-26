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
    public class QueueMemberService : IQueueMemberService
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

            return await CalculateAllPriorities(queueMembers).AsQueryable().ToListAsync();
        }

        public async Task<QueueMemberDTO> GetByIdAsync(int id)
        {
            //var queueMember = await _unitOfWork.QueueMemberRepository.GetByIdAsync(id);

            var queueMembers = await GetAllAsync();

            return await queueMembers.AsQueryable().FirstOrDefaultAsync(qm => qm.Id == id);
        }

        public async Task<IEnumerable<QueueMemberDTO>> GetByQueueIdAsync(int id)
        {
            var queueMembers = _unitOfWork.QueueMemberRepository
                .GetAllAsync(qm => qm.QueueId == id)
                .Select(QueueMemberMapper.ProjectToDTO);

            return await CalculatePrioritiesByGroup(queueMembers).AsQueryable().ToListAsync();
        }

        public async Task UpdateAsync(QueueMemberDTO queueMember)
        {
            _unitOfWork.QueueMemberRepository.Update(queueMember.AdaptToQueueMember());

            await _unitOfWork.CommitAsync();
        }

        private IEnumerable<QueueMemberDTO> CalculatePrioritiesByGroup(IEnumerable<QueueMemberDTO> queueMembers)
        {
            List<QueueMemberDTO> sortedMembers = queueMembers.OrderBy(qm => qm.TimeInterval).ToList();
            
            foreach (var member in sortedMembers)
            {
                int i = sortedMembers.IndexOf(member);
                member.Priority = i + 1;
            }

            return sortedMembers;
        }

        private IEnumerable<QueueMemberDTO> CalculateAllPriorities(IEnumerable<QueueMemberDTO> queueMembers)
        {
            var queueMemberGroups = queueMembers.GroupBy(q => q.QueueId);

            List<QueueMemberDTO> newQueueMembers = new List<QueueMemberDTO>();

            foreach (var group in queueMemberGroups)
            {
                var prioritizedMembers = CalculatePrioritiesByGroup(group);
                newQueueMembers.AddRange(prioritizedMembers);
            }

            return newQueueMembers;
        }

        public bool QueueMemberExists(int id)
        {
            return _unitOfWork.QueueMemberRepository.Exists(id);
        }
    }
}
