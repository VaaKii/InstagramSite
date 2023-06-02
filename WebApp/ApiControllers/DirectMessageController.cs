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
    public class DirectMessageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DirectMessageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DirectMessage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectMessage>>> GetDirectMessages()
        {
          if (_context.DirectMessages == null)
          {
              return NotFound();
          }
            return await _context.DirectMessages.ToListAsync();
        }

        // GET: api/DirectMessage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectMessage>> GetDirectMessage(Guid id)
        {
          if (_context.DirectMessages == null)
          {
              return NotFound();
          }
            var directMessage = await _context.DirectMessages.FindAsync(id);

            if (directMessage == null)
            {
                return NotFound();
            }

            return directMessage;
        }

        // PUT: api/DirectMessage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDirectMessage(Guid id, DirectMessage directMessage)
        {
            if (id != directMessage.Id)
            {
                return BadRequest();
            }

            _context.Entry(directMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectMessageExists(id))
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

        // POST: api/DirectMessage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DirectMessage>> PostDirectMessage(DirectMessage directMessage)
        {
          if (_context.DirectMessages == null)
          {
              return Problem("Entity set 'AppDbContext.DirectMessages'  is null.");
          }
            _context.DirectMessages.Add(directMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDirectMessage", new { id = directMessage.Id }, directMessage);
        }

        // DELETE: api/DirectMessage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirectMessage(Guid id)
        {
            if (_context.DirectMessages == null)
            {
                return NotFound();
            }
            var directMessage = await _context.DirectMessages.FindAsync(id);
            if (directMessage == null)
            {
                return NotFound();
            }

            _context.DirectMessages.Remove(directMessage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DirectMessageExists(Guid id)
        {
            return (_context.DirectMessages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
