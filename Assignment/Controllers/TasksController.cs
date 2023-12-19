using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;


        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // TasksController.cs
        [HttpGet]
        public async Task<ActionResult<List<TaskDetail>>> GetTasks()
        {
            var tasks = await _context.TaskDetail.ToListAsync();
            return Ok(tasks); // Wrap the list in an Ok result
        }

        [HttpGet("{id}")]
             public async Task<ActionResult<List<TaskDetail>>> GetTask(Guid id)
        {
            var task = await _context.TaskDetail.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDetail>> PostTaskDetail(TaskDetail tasks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TaskDetail.Add(tasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = tasks.Id }, tasks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskDetail(Guid id, TaskDetail task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await _context.TaskDetail.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            _context.TaskDetail.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(Guid id)
        {
            return _context.TaskDetail.Any(e => e.Id == id);
        }

    }
}


