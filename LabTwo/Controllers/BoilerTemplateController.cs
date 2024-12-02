using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoilerTemplateController : ControllerBase
    {
        private readonly BoilerTemplateContext _context;

        public BoilerTemplateController(BoilerTemplateContext context)
        {
            _context = context;
        }

        // GET: api/BoilerTemplate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoilerTemplate>>> GetBoilerTemplates()
        {
            return await _context.BoilerTemplates.ToListAsync();
        }

        // GET: api/BoilerTemplate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoilerTemplate>> GetBoilerTemplate(int id)
        {
            var boilerTemplate = await _context.BoilerTemplates.FindAsync(id);

            if (boilerTemplate == null)
            {
                return NotFound();
            }

            return boilerTemplate;
        }

        // PUT: api/BoilerTemplate/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoilerTemplate(int id, BoilerTemplate boilerTemplate)
        {
            if (id != boilerTemplate.Id)
            {
                return BadRequest();
            }

            _context.Entry(boilerTemplate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoilerTemplateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BoilerTemplate
        [HttpPost]
        public async Task<ActionResult<BoilerTemplate>> PostBoilerTemplate(BoilerTemplate boilerTemplate)
        {
            _context.BoilerTemplates.Add(boilerTemplate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoilerTemplate", new { id = boilerTemplate.Id }, boilerTemplate);
        }

        // DELETE: api/BoilerTemplate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoilerTemplate(int id)
        {
            var boilerTemplate = await _context.BoilerTemplates.FindAsync(id);
            if (boilerTemplate == null)
            {
                return NotFound();
            }

            _context.BoilerTemplates.Remove(boilerTemplate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoilerTemplateExists(int id)
        {
            return _context.BoilerTemplates.Any(e => e.Id == id);
        }
    }

    public class BoilerTemplateContext : DbContext
    {
        public BoilerTemplateContext(DbContextOptions<BoilerTemplateContext> options)
            : base(options)
        {
        }

        public DbSet<BoilerTemplate> BoilerTemplates { get; set; }
    }

    public class BoilerTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
