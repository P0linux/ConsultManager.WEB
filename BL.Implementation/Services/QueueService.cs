using BL.Abstraction;
using BL.DTO.Models;
using BL.Implementation.Extensions;
using DAL.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    public class QueueService : IQueueService
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
            var queues = _unitOfWork.QueueRepository.GetAllAsync()
                                          .Select(QueueMapper.ProjectToDTO);

            return await queues.ToListAsync();
        }

        public async Task<IEnumerable<QueueDTO>> GetByConsultationIdAsync(int id)
        {
            var queues = _unitOfWork.QueueRepository
                .GetAllAsync(q => q.ConsultationId == id)
                .Select(QueueMapper.ProjectToDTO);

            return await queues.ToListAsync();
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
