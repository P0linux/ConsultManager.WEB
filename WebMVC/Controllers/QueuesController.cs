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

namespace WebMVC.Controllers
{
    public class QueuesController : Controller
    {
        private readonly IQueueService _service;
        ApplicationContext _context;
        private readonly IConsultationService _consultationService;

        public QueuesController(IQueueService service, ApplicationContext context)
        {
            _service = service;
            _context = context;
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
        public IActionResult Create()
        {
            ViewData["ConsultationId"] = new SelectList(_context.Consultations, "Id", "Id");
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
            ViewData["ConsultationId"] = new SelectList(_context.Consultations, "Id", "Id", queue.ConsultationId);
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
            ViewData["ConsultationId"] = new SelectList(_context.Consultations, "Id", "Id", queue.ConsultationId);
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
            ViewData["ConsultationId"] = new SelectList(_context.Consultations, "Id", "Id", queue.ConsultationId);
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
