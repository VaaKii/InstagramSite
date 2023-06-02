using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserPostController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserPostController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserPost
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPost>>> GetUserPosts()
        {
          if (_context.UserPosts == null)
          {
              return NotFound();
          }
            return await _context.UserPosts.ToListAsync();
        }

        // GET: api/UserPost/5

        [HttpGet("{id}")]
        public async Task<ActionResult<UserPost>> GetUserPost(Guid id)
        {
          if (_context.UserPosts == null)
          {
              return NotFound();
          }
            var userPost = await _context.UserPosts.FindAsync(id);

            if (userPost == null)
            {
                return NotFound();
            }

            return userPost;
        }

        // PUT: api/UserPost/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPost(Guid id, UserPost userPost)
        {
            if (id != userPost.Id)
            {
                return BadRequest();
            }

            _context.Entry(userPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPostExists(id))
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

        // POST: api/UserPost
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserPost>> PostUserPost(UserPost userPost)
        {
          if (_context.UserPosts == null)
          {
              return Problem("Entity set 'AppDbContext.UserPosts'  is null.");
          }
            _context.UserPosts.Add(userPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPost", new { id = userPost.Id }, userPost);
        }

        // DELETE: api/UserPost/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteUserPost(Guid id)
        {
            if (_context.UserPosts == null)
            {
                return NotFound();
            }
            var userPost = await _context.UserPosts.FindAsync(id);
            if (userPost == null)
            {
                return NotFound();
            }

            _context.UserPosts.Remove(userPost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserPostExists(Guid id)
        {
            return (_context.UserPosts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
