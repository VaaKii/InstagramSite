using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserStoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserStoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserStories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserStories>>> GetUserStories()
        {
          if (_context.UserStories == null)
          {
              return NotFound();
          }
            return await _context.UserStories.ToListAsync();
        }

        // GET: api/UserStories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserStories>> GetUserStories(Guid id)
        {
          if (_context.UserStories == null)
          {
              return NotFound();
          }
            var userStories = await _context.UserStories.FindAsync(id);

            if (userStories == null)
            {
                return NotFound();
            }

            return userStories;
        }

        // PUT: api/UserStories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserStories(Guid id, UserStories userStories)
        {
            if (id != userStories.Id)
            {
                return BadRequest();
            }

            _context.Entry(userStories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserStoriesExists(id))
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

        // POST: api/UserStories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserStories>> PostUserStories(UserStories userStories)
        {
          if (_context.UserStories == null)
          {
              return Problem("Entity set 'AppDbContext.UserStories'  is null.");
          }
            _context.UserStories.Add(userStories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserStories", new { id = userStories.Id }, userStories);
        }

        // DELETE: api/UserStories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserStories(Guid id)
        {
            if (_context.UserStories == null)
            {
                return NotFound();
            }
            var userStories = await _context.UserStories.FindAsync(id);
            if (userStories == null)
            {
                return NotFound();
            }

            _context.UserStories.Remove(userStories);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserStoriesExists(Guid id)
        {
            return (_context.UserStories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
