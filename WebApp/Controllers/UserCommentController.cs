using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain;

namespace WebApp.Controllers
{
    public class UserCommentController : Controller
    {
        private readonly IAppBll _context;

        public UserCommentController(IAppBll context)
        {
            _context = context;
        }

        // GET: UserComment
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserComments.Include(u => u.AppUser).Include(u => u.UserPost);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserComment/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.UserComments == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComments
                .Include(u => u.AppUser)
                .Include(u => u.UserPost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userComment == null)
            {
                return NotFound();
            }

            return View(userComment);
        }

        // GET: UserComment/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname");
            ViewData["LikedId"] = new SelectList(_context.UserPosts, "Id", "Text");
            return View();
        }

        // POST: UserComment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentText,LikedId,AuthorId,CreatedAt,Id")] UserComment userComment)
        {
            if (ModelState.IsValid)
            {
                userComment.Id = Guid.NewGuid();
                _context.Add(userComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userComment.AppUserId);
            ViewData["LikedId"] = new SelectList(_context.UserPosts, "Id", "Text", userComment.UserPostId);
            return View(userComment);
        }

        // GET: UserComment/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.UserComments == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComments.FindAsync(id);
            if (userComment == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userComment.AppUserId);
            ViewData["LikedId"] = new SelectList(_context.UserPosts, "Id", "Text", userComment.UserPostId);
            return View(userComment);
        }

        // POST: UserComment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CommentText,LikedId,AuthorId,CreatedAt,Id")] UserComment userComment)
        {
            if (id != userComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCommentExists(userComment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userComment.AppUserId);
            ViewData["LikedId"] = new SelectList(_context.UserPosts, "Id", "Text", userComment.UserPostId);
            return View(userComment);
        }

        // GET: UserComment/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.UserComments == null)
            {
                return NotFound();
            }

            var userComment = await _context.UserComments
                .Include(u => u.AppUser)
                .Include(u => u.UserPost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userComment == null)
            {
                return NotFound();
            }

            return View(userComment);
        }

        // POST: UserComment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.UserComments == null)
            {
                return Problem("Entity set 'IAppBll.UserComments'  is null.");
            }
            var userComment = await _context.UserComments.FindAsync(id);
            if (userComment != null)
            {
                _context.UserComments.Remove(userComment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCommentExists(Guid id)
        {
          return (_context.UserComments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
