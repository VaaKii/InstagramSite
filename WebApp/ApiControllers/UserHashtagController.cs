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
    public class UserHashtagController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserHashtagController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserHashtag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserHashtag>>> GetUserHashtags()
        {
          if (_context.UserHashtags == null)
          {
              return NotFound();
          }
            return await _context.UserHashtags.ToListAsync();
        }

        // GET: api/UserHashtag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserHashtag>> GetUserHashtag(Guid id)
        {
          if (_context.UserHashtags == null)
          {
              return NotFound();
          }
            var userHashtag = await _context.UserHashtags.FindAsync(id);

            if (userHashtag == null)
            {
                return NotFound();
            }

            return userHashtag;
        }

        // PUT: api/UserHashtag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserHashtag(Guid id, UserHashtag userHashtag)
        {
            if (id != userHashtag.Id)
            {
                return BadRequest();
            }

            _context.Entry(userHashtag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserHashtagExists(id))
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

        // POST: api/UserHashtag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserHashtag>> PostUserHashtag(UserHashtag userHashtag)
        {
          if (_context.UserHashtags == null)
          {
              return Problem("Entity set 'AppDbContext.UserHashtags'  is null.");
          }
            _context.UserHashtags.Add(userHashtag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserHashtag", new { id = userHashtag.Id }, userHashtag);
        }

        // DELETE: api/UserHashtag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserHashtag(Guid id)
        {
            if (_context.UserHashtags == null)
            {
                return NotFound();
            }
            var userHashtag = await _context.UserHashtags.FindAsync(id);
            if (userHashtag == null)
            {
                return NotFound();
            }

            _context.UserHashtags.Remove(userHashtag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserHashtagExists(Guid id)
        {
            return (_context.UserHashtags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
