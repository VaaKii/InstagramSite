using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain;

namespace WebApp.Controllers
{
    public class UserPostsController : Controller
    {
        private readonly IAppBll _context;

        public UserPostsController(IAppBll context)
        {
            _context = context;
        }

        // GET: UserPosts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserPosts.Include(u => u.AppUser).Include(u => u.Topic);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserPosts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.UserPosts == null)
            {
                return NotFound();
            }

            var userPost = await _context.UserPosts
                .Include(u => u.AppUser)
                .Include(u => u.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPost == null)
            {
                return NotFound();
            }

            return View(userPost);
        }

        // GET: UserPosts/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname");
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id");
            return View();
        }

        // POST: UserPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Text,TopicId,UrlPhoto,AuthorId,CreatedAt,Id")] UserPost userPost)
        {
            if (ModelState.IsValid)
            {
                userPost.Id = Guid.NewGuid();
                _context.Add(userPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userPost.AppUserId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", userPost.TopicId);
            return View(userPost);
        }

        // GET: UserPosts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.UserPosts == null)
            {
                return NotFound();
            }

            var userPost = await _context.UserPosts.FindAsync(id);
            if (userPost == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userPost.AppUserId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", userPost.TopicId);
            return View(userPost);
        }

        // POST: UserPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Text,TopicId,UrlPhoto,AuthorId,CreatedAt,Id")] UserPost userPost)
        {
            if (id != userPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPostExists(userPost.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userPost.AppUserId);
            ViewData["TopicId"] = new SelectList(_context.Topics, "Id", "Id", userPost.TopicId);
            return View(userPost);
        }

        // GET: UserPosts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.UserPosts == null)
            {
                return NotFound();
            }

            var userPost = await _context.UserPosts
                .Include(u => u.AppUser)
                .Include(u => u.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPost == null)
            {
                return NotFound();
            }

            return View(userPost);
        }

        // POST: UserPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.UserPosts == null)
            {
                return Problem("Entity set 'IAppBll.UserPosts'  is null.");
            }
            var userPost = await _context.UserPosts.FindAsync(id);
            if (userPost != null)
            {
                _context.UserPosts.Remove(userPost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPostExists(Guid id)
        {
          return (_context.UserPosts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
