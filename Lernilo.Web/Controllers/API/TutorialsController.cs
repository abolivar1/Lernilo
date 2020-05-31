using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lernilo.Web.Data;
using Lernilo.Web.Data.Entities;
using Lernilo.Web.Helpers;

namespace Lernilo.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorialsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TutorialsController(
            DataContext context,
            IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        // GET: api/Tutorials
        [HttpGet]
        public async Task<IActionResult> GetTutorials()
        {
            List<Tutorial> Tutorials = await _context.Tutorials
                .Include(c => c.Comments)
                .Include(c => c.Customer)
                .ThenInclude(c => c.User)
                .Include(t => t.TutorialReports)
                .Include(c => c.Category)
                .ToListAsync();
            return Ok(_converterHelper.ToTutorialResponse(Tutorials));
        }

        // GET: api/Tutorials/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTutorial([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tutorial = await _context.Tutorials.FindAsync(id);

            if (tutorial == null)
            {
                return NotFound();
            }

            return Ok(tutorial);
        }

        // PUT: api/Tutorials/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTutorial([FromRoute] int id, [FromBody] Tutorial tutorial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tutorial.Id)
            {
                return BadRequest();
            }

            _context.Entry(tutorial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TutorialExists(id))
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

        // POST: api/Tutorials
        [HttpPost]
        public async Task<IActionResult> PostTutorial([FromBody] Tutorial tutorial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tutorials.Add(tutorial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTutorial", new { id = tutorial.Id }, tutorial);
        }

        // DELETE: api/Tutorials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTutorial([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tutorial = await _context.Tutorials.FindAsync(id);
            if (tutorial == null)
            {
                return NotFound();
            }

            _context.Tutorials.Remove(tutorial);
            await _context.SaveChangesAsync();

            return Ok(tutorial);
        }

        private bool TutorialExists(int id)
        {
            return _context.Tutorials.Any(e => e.Id == id);
        }
    }
}