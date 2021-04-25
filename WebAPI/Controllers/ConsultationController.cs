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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultationDTO>>> GetAll()
            => Ok(await _consultationService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultationDTO>> GetById(int id)
            => Ok(await _consultationService.GetByIdAsync(id)); 

        [HttpPost]
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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ConsultationDTO consultation)
        {
            if (consultation is null)
                return BadRequest();

            await _consultationService.UpdateAsync(consultation);
            
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _consultationService.DeleteByIdAsync(id);

            return Ok(); 
        }
    }
}
