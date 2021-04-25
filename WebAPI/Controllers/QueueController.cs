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
    public class QueueController : ControllerBase
    {
        private readonly IQueueService _queueService;

        public QueueController(IQueueService queueService)
        {
            _queueService = queueService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueueDTO>>> GetAll()
            => Ok(await _queueService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<QueueDTO>> GetById(int id)
            => Ok(await _queueService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] QueueDTO queue)
        {
            try
            {
                await _queueService.AddAsync(queue);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(Add), new { queue.Id }, queue);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] QueueDTO queue)
        {
            if (queue is null)
                return BadRequest();

            await _queueService.UpdateAsync(queue);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _queueService.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
