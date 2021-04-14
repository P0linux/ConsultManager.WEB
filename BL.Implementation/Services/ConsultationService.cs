using BL.Abstraction;
using BL.DTO.Models;
using BL.Implementation.Extensions;
using DAL.Abstraction;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    class ConsultationService : IConsultationService
    {
        IUnitOfWork _unitOfWork;
        public ConsultationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(ConsultationDTO consultation)
        {
            await _unitOfWork.ConsultationRepository.InsertAsync(consultation.AdaptToConsultation());
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var consult = await _unitOfWork.ConsultationRepository.GetByIdAsync(id);
            _unitOfWork.ConsultationRepository.Delete(consult);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ConsultationDTO>> GetAllAsync()
        {
            var consultations = _unitOfWork.ConsultationRepository.GetAllAsync();
            return ;
        }

        public async Task<ConsultationDTO> GetByIdAsync(int id)
        {
            var consult = await _unitOfWork.ConsultationRepository.GetByIdAsync(id);
            return consult.AdaptToDTO();
        }

        public async Task UpdateAsync(ConsultationDTO consultation)
        {
            _unitOfWork.ConsultationRepository.Update(consultation.AdaptToConsultation());

            await _unitOfWork.CommitAsync();
        }
    }
}
