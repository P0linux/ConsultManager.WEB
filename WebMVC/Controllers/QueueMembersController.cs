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
    public class QueueMembersController : Controller
    {
        private readonly IQueueMemberService _service;
        private readonly IQueueService _queueService;
        private readonly IConsultationService _consultationService;

        public QueueMembersController(IQueueMemberService service, IQueueService queueService, IConsultationService consultationService)
        {
            _queueService = queueService;
            _service = service;
            _consultationService = consultationService;
        }

        // GET: QueueMembers
        public async Task<IActionResult> Index(int? consultationId)
        {
            var consultations = await _consultationService.GetAllAsync();

            var query = consultations.Select(c => new {
                Id = c.Id,
                DisplayText = String.Format("{0} {1} {2} {3}", c.Lecturer.FirstName, c.Lecturer.SecondName, c.Subject.Name, c.Date)
            });

            ViewData["ConsultationId"] = new SelectList(query, "Id", "DisplayText");

            if (consultationId is null)
            {
                return View(await _service.GetAllAsync());
            }

            var members = await _service.GetAllAsync();
            return View(members.Where(m => m.Queue.ConsultationId == consultationId));
        }

        // GET: QueueMembers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var queueMember = await _service.GetByIdAsync(id);
            if (queueMember == null)
            {
                return NotFound();
            }

            return View(queueMember);
        }

        // GET: QueueMembers/Create
        public async Task<IActionResult> Create()
        {
            var queues = await _queueService.GetAllAsync();

            var query = queues.Select(q => new
            {
                Id = q.Id,
                DisplayText = String.Format("{0} {1} {2} {3}", q.Consultation.Lecturer.SecondName, 
                q.Consultation.Subject.Name, q.Consultation.Date, q.IssueCategory.ToString())
            });

            ViewData["QueueId"] = new SelectList(query, "Id", "DisplayText");
            return View();
        }

        // POST: QueueMembers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Priority,TimeInterval,IsAbsent,QueueId,StudentId,Id")] QueueMemberDTO queueMember)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            queueMember.StudentId = userId;

            if (ModelState.IsValid)
            {
                await _service.AddAsync(queueMember);
                return RedirectToAction(nameof(Index));
            }
            var queues = await _queueService.GetAllAsync();
            ViewData["QueueId"] = new SelectList(queues, "Id", "Id", queueMember.QueueId);
            return View(queueMember);
        }

        // GET: QueueMembers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var queueMember = await _service.GetByIdAsync(id);
            if (queueMember == null)
            {
                return NotFound();
            }
            var queues = await _queueService.GetAllAsync();

            var query = queues.Select(q => new
            {
                Id = q.Id,
                DisplayText = String.Format("{0} {1} {2} {3}", q.Consultation.Lecturer.SecondName,
                q.Consultation.Subject.Name, q.Consultation.Date, q.IssueCategory.ToString())
            });
            ViewData["QueueId"] = new SelectList(query, "Id", "DisplayText", queueMember.QueueId);
            return View(queueMember);
        }

        // POST: QueueMembers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Priority,TimeInterval,IsAbsent,QueueId,StudentId,Id")] QueueMemberDTO queueMember)
        {
            if (id != queueMember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(queueMember);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.QueueMemberExists(queueMember.Id))
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
            var queues = await _queueService.GetAllAsync();

            var query = queues.Select(q => new
            {
                Id = q.Id,
                DisplayText = String.Format("{0} {1} {2} {3}", q.Consultation.Lecturer.SecondName,
                q.Consultation.Subject.Name, q.Consultation.Date, q.IssueCategory.ToString())
            });
            ViewData["QueueId"] = new SelectList(query, "Id", "DisplayText", queueMember.QueueId);

            return View(queueMember);
        }

        // GET: QueueMembers/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var queueMember = await _service.GetByIdAsync(id);
            if (queueMember == null)
            {
                return NotFound();
            }

            return View(queueMember);
        }

        // POST: QueueMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
