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

        /// <summary>
        /// Gets all QueueMember items.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<QueueMemberDTO>>> GetAll()
            => Ok(await _queueMemberService.GetAllAsync());

        /// <summary>
        /// Gets a specific QueueMember item by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<QueueMemberDTO>> GetById(int id)
            => Ok(await _queueMemberService.GetByIdAsync(id));

        /// <summary>
        /// Creates a specific QueueMember item.
        /// </summary>
        /// <param name="queueMember"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">The item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Updates a specific QueueMember item.
        /// </summary>
        /// <param name="queueMember"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="400">The item is null</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] QueueMemberDTO queueMember)
        {
            if (queueMember is null)
                return BadRequest();

            await _queueMemberService.UpdateAsync(queueMember);

            return Ok();
        }

        /// <summary>
        /// Deletes a specific QueueMember item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            await _queueMemberService.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
