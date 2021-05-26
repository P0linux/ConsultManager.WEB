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
    public class QueueMembersController : Controller
    {
        private readonly IQueueMemberService _service;
        ApplicationContext _context;

        public QueueMembersController(IQueueMemberService service, ApplicationContext context)
        {
            _service = service;
            _context = context;
        }

        // GET: QueueMembers
        public async Task<IActionResult> Index()
        {
            var members = await _service.GetAllAsync();
            return View(members);
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
        public IActionResult Create()
        {
            ViewData["QueueId"] = new SelectList(_context.Queues, "Id", "Id");
            return View();
        }

        // POST: QueueMembers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Priority,TimeInterval,IsAbsent,QueueId,StudentId,Id")] QueueMemberDTO queueMember)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(queueMember);
                return RedirectToAction(nameof(Index));
            }
            ViewData["QueueId"] = new SelectList(_context.Queues, "Id", "Id", queueMember.QueueId);
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
            ViewData["QueueId"] = new SelectList(_context.Queues, "Id", "Id", queueMember.QueueId);
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
            ViewData["QueueId"] = new SelectList(_context.Queues, "Id", "Id", queueMember.QueueId);
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
