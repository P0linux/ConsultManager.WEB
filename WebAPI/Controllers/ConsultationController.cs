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
    public class ConsultationController : ControllerBase
    {
        private readonly IConsultationService _consultationService;

        public ConsultationController(IConsultationService consultationService)
        {
            _consultationService = consultationService;
        }

        /// <summary>
        /// Gets all Consultation items.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ConsultationDTO>>> GetAll()
            => Ok(await _consultationService.GetAllAsync());

        /// <summary>
        /// Gets a specific Consultation item by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ConsultationDTO>> GetById(int id)
            => Ok(await _consultationService.GetByIdAsync(id));

        /// <summary>
        /// Creates a specific Consultation item.
        /// </summary>
        /// <param name="consultation"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">The item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] ConsultationDTO consultation)
        {
            try
            {
                await _consultationService.AddAsync(consultation);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(Add), new { consultation.Id }, consultation);
        }

        /// <summary>
        /// Updates a specific Consultation item.
        /// </summary>
        /// <param name="consultation"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="400">The item is null</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody]ConsultationDTO consultation)
        {
            if (consultation is null)
                return BadRequest();

            await _consultationService.UpdateAsync(consultation);
            
            return Ok();
        }

        /// <summary>
        /// Deletes a specific Consultation item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            await _consultationService.DeleteByIdAsync(id);

            return Ok(); 
        }
    }
}
