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
    public class FollowContoller : ControllerBase
    {
        private readonly AppDbContext _context;

        public FollowContoller(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FollowContoller
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Follow>>> GetFollows()
        {
          if (_context.Follows == null)
          {
              return NotFound();
          }
            return await _context.Follows.ToListAsync();
        }

        // GET: api/FollowContoller/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Follow>> GetFollow(Guid id)
        {
          if (_context.Follows == null)
          {
              return NotFound();
          }
            var follow = await _context.Follows.FindAsync(id);

            if (follow == null)
            {
                return NotFound();
            }

            return follow;
        }

        // PUT: api/FollowContoller/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollow(Guid id, Follow follow)
        {
            if (id != follow.Id)
            {
                return BadRequest();
            }

            _context.Entry(follow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowExists(id))
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

        // POST: api/FollowContoller
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Follow>> PostFollow(Follow follow)
        {
          if (_context.Follows == null)
          {
              return Problem("Entity set 'AppDbContext.Follows'  is null.");
          }
            _context.Follows.Add(follow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollow", new { id = follow.Id }, follow);
        }

        // DELETE: api/FollowContoller/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollow(Guid id)
        {
            if (_context.Follows == null)
            {
                return NotFound();
            }
            var follow = await _context.Follows.FindAsync(id);
            if (follow == null)
            {
                return NotFound();
            }

            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FollowExists(Guid id)
        {
            return (_context.Follows?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
