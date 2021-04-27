using BL.Abstraction;
using BL.DTO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        /// <summary>
        /// Gets all Subject items.
        /// </summary>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> GetAll()
            => Ok(await _subjectService.GetAllAsync());

        /// <summary>
        /// Gets a specific Subject item by id.
        /// </summary>
        /// <response code="200">Success</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SubjectDTO>> GetById(int id)
            => Ok(await _subjectService.GetByIdAsync(id));


        /// <summary>
        /// Creates a specific Subject item.
        /// </summary>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">The item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] SubjectDTO subject)
        {
            try
            {
                await _subjectService.AddAsync(subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(Add), new { subject.Id }, subject);
        }

        /// <summary>
        /// Updates a specific Subject item.
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">The item is null</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] SubjectDTO subject)
        {
            if (subject is null)
                return BadRequest();

            await _subjectService.UpdateAsync(subject);

            return Ok();
        }

        /// <summary>
        /// Deletes a specific Subject item.
        /// </summary>
        /// <response code="200">Success</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            await _subjectService.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
