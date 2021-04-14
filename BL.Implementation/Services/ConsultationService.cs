using BL.Abstraction;
using BL.DTO.Models;
using BL.Implementation.Extensions;
using DAL.Abstraction;
using DAL.Implementation;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    class ConsultationService : IConsultationService
    {
        IUnitOfWork _unitOfWork;
        ApplicationContext _context;
        public ConsultationService(IUnitOfWork unitOfWork, ApplicationContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
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
            var consultations = await _unitOfWork.ConsultationRepository.GetAllAsync();
            consultations.Select();
            return consultations;
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
