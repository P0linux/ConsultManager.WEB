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
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> GetAll()
            => Ok(await _subjectService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDTO>> GetById(int id)
            => Ok(await _subjectService.GetByIdAsync(id));

        [HttpPost]
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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SubjectDTO subject)
        {
            if (subject is null)
                return BadRequest();

            await _subjectService.UpdateAsync(subject);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _subjectService.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
