using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using LabTwoApi.Models;
using LabTwoApi.Repositories;

namespace LabTwoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordRepository _repository;

        public RecordsController(IRecordRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Record>>> GetAll()
        {
            var records = await _repository.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Record>> GetById(int id)
        {
            var record = await _repository.GetByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult<Record>> Create(Record record)
        {
            var createdRecord = await _repository.AddAsync(record);
            return CreatedAtAction(nameof(GetById), new { id = createdRecord.Id }, createdRecord);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Record record)
        {
            if (id != record.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(record);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
