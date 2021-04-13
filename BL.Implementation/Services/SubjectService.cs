﻿using BL.Abstraction;
using BL.DTO.Models;
using BL.Implementation.Extensions;
using DAL.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Implementation.Services
{
    class SubjectService : ISubjectService
    {
        IUnitOfWork _unitOfWork;
        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(SubjectDTO subject)
        {
            await _unitOfWork.SubjectRepository.InsertAsync(subject.AdaptToSubject());
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(id);
            _unitOfWork.SubjectRepository.Delete(subject);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<SubjectDTO>> GetAllAsync()
        {
            var subject = await _unitOfWork.SubjectRepository.GetAllAsync();
            return;
        }

        public async Task<SubjectDTO> GetByIdAsync(int id)
        {
            var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(id);
            return subject.AdaptToDTO();
        }

        public async Task UpdateAsync(SubjectDTO subject)
        {
            _unitOfWork.SubjectRepository.Update(subject.AdaptToSubject());

            await _unitOfWork.CommitAsync();
        }
    }
}
