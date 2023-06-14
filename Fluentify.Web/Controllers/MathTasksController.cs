using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fluentify.Web.Areas.Identity.Data;
using Fluentify.Web.Data;

namespace Fluentify.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MathTasksController : ControllerBase
    {
        private readonly FluentifyDbContext _context;

        public MathTasksController(FluentifyDbContext context)
        {
            _context = context;
        }

        // GET: api/MathTasks
        [HttpGet]
        public async Task<IActionResult> GetAllMathTasks()
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'FluentifyDbContext.Tasks' is null.");
            }

            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        // GET: api/MathTasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMathTask(int id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'FluentifyDbContext.Tasks' is null.");
            }

            var mathTask = await _context.Tasks.FindAsync(id);
            if (mathTask == null)
            {
                return NotFound();
            }

            return Ok(mathTask);
        }

        // POST: api/MathTasks
        [HttpPost]
        public async Task<IActionResult> CreateMathTask(MathTask mathTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mathTask);
                await _context.SaveChangesAsync();
                return Ok(mathTask);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/MathTasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMathTask(int id, MathTask mathTask)
        {
            if (id != mathTask.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mathTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MathTaskExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(mathTask);
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/MathTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMathTask(int id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'FluentifyDbContext.Tasks' is null.");
            }

            var mathTask = await _context.Tasks.FindAsync(id);
            if (mathTask == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(mathTask);
            await _context.SaveChangesAsync();
            return Ok(mathTask);
        }

        private bool MathTaskExists(int id)
        {
            return _context.Tasks?.Any(e => e.Id == id) ?? false;
        }
    }
}
