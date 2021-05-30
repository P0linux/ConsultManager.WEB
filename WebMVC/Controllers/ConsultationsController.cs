using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using DAL.Implementation;
using BL.Abstraction;
using BL.DTO.Models;
using System.Security.Claims;

namespace WebMVC.Controllers
{
    public class ConsultationsController : Controller
    {
        private readonly IConsultationService _service;
        private readonly ISubjectService _subjectService;
        ApplicationContext _context;

        public ConsultationsController(IConsultationService service, 
                                       ApplicationContext context, 
                                       ISubjectService subjectService)
        {
            _subjectService = subjectService;
            _service = service;
            _context = context;
        }

        // GET: Consultations
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Consultations/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var consultation = await _service.GetByIdAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // GET: Consultations/Create
        public async Task<IActionResult> Create()
        {
            var subjects = await _subjectService.GetAllAsync();
            ViewData["Subject"] = new SelectList(subjects, "Id", "Name");
            return View();
        }

        // POST: Consultations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,SubjectId,LecturerId,Id")] ConsultationDTO consultation)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            consultation.LecturerId = userId;

            if (ModelState.IsValid)
            {
                await _service.AddAsync(consultation);
                return RedirectToAction(nameof(Index));
            }

            var subjects = await _subjectService.GetAllAsync();
            ViewData["SubjectId"] = new SelectList(subjects, "Id", "Id", consultation.SubjectId);
            return View(consultation);
        }

        // GET: Consultations/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var consultation = await _service.GetByIdAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }

            var subjects = await _subjectService.GetAllAsync();
            ViewData["SubjectId"] = new SelectList(subjects, "Id", "Id", consultation.SubjectId);
            return View(consultation);
        }

        // POST: Consultations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Date,SubjectId,LecturerId,Id")] ConsultationDTO consultation)
        {
            if (id != consultation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(consultation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.ConsultationExists(consultation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var subjects = await _subjectService.GetAllAsync();
            ViewData["SubjectId"] = new SelectList(subjects, "Id", "Id", consultation.SubjectId);
            return View(consultation);
        }

        // GET: Consultations/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var consultation = await _service.GetByIdAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // POST: Consultations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
