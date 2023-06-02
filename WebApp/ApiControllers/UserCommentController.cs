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
    public class UserCommentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserCommentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserComment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserComment>>> GetUserComments()
        {
          if (_context.UserComments == null)
          {
              return NotFound();
          }
            return await _context.UserComments.ToListAsync();
        }

        // GET: api/UserComment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserComment>> GetUserComment(Guid id)
        {
          if (_context.UserComments == null)
          {
              return NotFound();
          }
            var userComment = await _context.UserComments.FindAsync(id);

            if (userComment == null)
            {
                return NotFound();
            }

            return userComment;
        }

        // PUT: api/UserComment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserComment(Guid id, UserComment userComment)
        {
            if (id != userComment.Id)
            {
                return BadRequest();
            }

            _context.Entry(userComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCommentExists(id))
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

        // POST: api/UserComment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserComment>> PostUserComment(UserComment userComment)
        {
          if (_context.UserComments == null)
          {
              return Problem("Entity set 'AppDbContext.UserComments'  is null.");
          }
            _context.UserComments.Add(userComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserComment", new { id = userComment.Id }, userComment);
        }

        // DELETE: api/UserComment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserComment(Guid id)
        {
            if (_context.UserComments == null)
            {
                return NotFound();
            }
            var userComment = await _context.UserComments.FindAsync(id);
            if (userComment == null)
            {
                return NotFound();
            }

            _context.UserComments.Remove(userComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserCommentExists(Guid id)
        {
            return (_context.UserComments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
