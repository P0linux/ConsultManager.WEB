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
using BL.DTO;

namespace WebMVC.Controllers
{
    public class QueuesController : Controller
    {
        private readonly IQueueService _service;
        private readonly IConsultationService _consultationService;

        public QueuesController(IQueueService service, 
                                ApplicationContext context, 
                                IConsultationService consultationService)
        {
            _service = service;
            _consultationService = consultationService;
        }

        // GET: Queues
        public async Task<IActionResult> Index()
        {

            return View(await _service.GetAllAsync());
        }

        // GET: Queues/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var queue = await _service.GetByIdAsync(id);
            if (queue == null)
            {
                return NotFound();
            }

            return View(queue);
        }

        // GET: Queues/Create
        public async Task<IActionResult> Create()
        {
            var consultations = await _consultationService.GetAllAsync();
            
            var query = consultations.Select(c => new { Id = c.Id,
                DisplayText = String.Format("{0} {1} {2} {3}", c.Lecturer.FirstName, c.Lecturer.SecondName, c.Subject.Name, c.Date) });
            ViewData["ConsultationId"] = new SelectList(query, "Id", "DisplayText");

            var enamData = Enum.GetValues(typeof(IssueCategory))
                               .OfType<Enum>()
                               .Select(e => new { Id = Convert.ToInt32(e), Display = Enum.GetName(typeof(IssueCategory), e)});
            ViewData["IssueCategory"] = new SelectList(enamData, "Id", "Display");

            return View();
        }

        // POST: Queues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IssueCategory,Priority,ConsultationId,Id")] QueueDTO queue)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(queue);
                return RedirectToAction(nameof(Index));
            }
            var consultations = await _consultationService.GetAllAsync();
            ViewData["ConsultationId"] = new SelectList(consultations, "Id", "Id", queue.ConsultationId);
            return View(queue);
        }

        // GET: Queues/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var queue = await _service.GetByIdAsync(id);
            if (queue == null)
            {
                return NotFound();
            }

            var consultations = await _consultationService.GetAllAsync();
            ViewData["ConsultationId"] = new SelectList(consultations, "Id", "Id", queue.ConsultationId);
            return View(queue);
        }

        // POST: Queues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IssueCategory,Priority,ConsultationId,Id")] QueueDTO queue)
        {
            if (id != queue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(queue);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.QueueExists(queue.Id))
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

            var consultations = await _consultationService.GetAllAsync();
            ViewData["ConsultationId"] = new SelectList(consultations, "Id", "Id", queue.ConsultationId);
            return View(queue);
        }

        // GET: Queues/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var queue = await _service.GetByIdAsync(id);
            if (queue == null)
            {
                return NotFound();
            }

            return View(queue);
        }

        // POST: Queues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
