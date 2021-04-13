using BL.Abstraction;
using BL.DTO.Models;
using BL.Implementation.Extensions;
using DAL.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    class QueueService : IQueueService
    {
        IUnitOfWork _unitOfWork;
        public QueueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(QueueDTO queue)
        {
            await _unitOfWork.QueueRepository.InsertAsync(queue.AdaptToQueue());
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var queue = await _unitOfWork.QueueRepository.GetByIdAsync(id);
            _unitOfWork.QueueRepository.Delete(queue);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<QueueDTO>> GetAllAsync()
        {
            var queues = await _unitOfWork.QueueRepository.GetAllAsync();
            return;
        }

        public async Task<IEnumerable<QueueDTO>> GetByConsultationIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<QueueDTO> GetByIdAsync(int id)
        {
            var queue = await _unitOfWork.QueueRepository.GetByIdAsync(id);
            return queue.AdaptToDTO();
        }

        public async Task UpdateAsync(QueueDTO queue)
        {
            _unitOfWork.QueueRepository.Update(queue.AdaptToQueue());

            await _unitOfWork.CommitAsync();
        }
    }
}
