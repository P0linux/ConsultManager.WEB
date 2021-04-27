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

        /// <summary>
        /// Gets all Queue items.
        /// </summary>
        /// <returns></returns>
        /// <responce code="200">Success</responce>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<QueueDTO>>> GetAll()
            => Ok(await _queueService.GetAllAsync());

        /// <summary>
        /// Gets a specific Queue item by id. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <responce code="200">Success</responce>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<QueueDTO>> GetById(int id)
            => Ok(await _queueService.GetByIdAsync(id));

        /// <summary>
        /// Creates a specific Queue item.
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">The item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Updates a specific Queue item.
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="400">The item is null</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] QueueDTO queue)
        {
            if (queue is null)
                return BadRequest();

            await _queueService.UpdateAsync(queue);

            return Ok();
        }

        /// <summary>
        /// Deletes a specific Queue item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            await _queueService.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
