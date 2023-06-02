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
    public class UserLikeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserLikeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserLike
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLike>>> GetUserLikes()
        {
          if (_context.UserLikes == null)
          {
              return NotFound();
          }
            return await _context.UserLikes.ToListAsync();
        }

        // GET: api/UserLike/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserLike>> GetUserLike(Guid id)
        {
          if (_context.UserLikes == null)
          {
              return NotFound();
          }
            var userLike = await _context.UserLikes.FindAsync(id);

            if (userLike == null)
            {
                return NotFound();
            }

            return userLike;
        }

        // PUT: api/UserLike/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLike(Guid id, UserLike userLike)
        {
            if (id != userLike.Id)
            {
                return BadRequest();
            }

            _context.Entry(userLike).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLikeExists(id))
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

        // POST: api/UserLike
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserLike>> PostUserLike(UserLike userLike)
        {
          if (_context.UserLikes == null)
          {
              return Problem("Entity set 'AppDbContext.UserLikes'  is null.");
          }
            _context.UserLikes.Add(userLike);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserLike", new { id = userLike.Id }, userLike);
        }

        // DELETE: api/UserLike/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserLike(Guid id)
        {
            if (_context.UserLikes == null)
            {
                return NotFound();
            }
            var userLike = await _context.UserLikes.FindAsync(id);
            if (userLike == null)
            {
                return NotFound();
            }

            _context.UserLikes.Remove(userLike);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserLikeExists(Guid id)
        {
            return (_context.UserLikes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
