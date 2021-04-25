using BL.Abstraction;
using BL.DTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueMemberController : ControllerBase
    {
        private readonly IQueueMemberService _queueMemberService;

        public QueueMemberController(IQueueMemberService queueMemberService)
        {
            _queueMemberService = queueMemberService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueueMemberDTO>>> GetAll()
            => Ok(await _queueMemberService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<QueueMemberDTO>> GetById(int id)
            => Ok(await _queueMemberService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] QueueMemberDTO queueMember)
        {
            try
            {
                await _queueMemberService.AddAsync(queueMember);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(Add), new { queueMember.Id }, queueMember);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] QueueMemberDTO queueMember)
        {
            if (queueMember is null)
                return BadRequest();

            await _queueMemberService.UpdateAsync(queueMember);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _queueMemberService.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
