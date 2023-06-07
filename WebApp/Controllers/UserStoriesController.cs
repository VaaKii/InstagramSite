using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain;

namespace WebApp.Controllers
{
    public class UserStoriesController : Controller
    {
        private readonly IAppBll _context;

        public UserStoriesController(IAppBll context)
        {
            _context = context;
        }

        // GET: UserStory
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserStories.Include(u => u.AppUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserStory/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.UserStories == null)
            {
                return NotFound();
            }

            var userStories = await _context.UserStories
                .Include(u => u.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userStories == null)
            {
                return NotFound();
            }

            return View(userStories);
        }

        // GET: UserStory/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname");
            return View();
        }

        // POST: UserStory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,UrlPhoto,CreatedAt,Id")] UserStory userStory)
        {
            if (ModelState.IsValid)
            {
                userStory.Id = Guid.NewGuid();
                _context.Add(userStory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userStory.AppUserId);
            return View(userStory);
        }

        // GET: UserStory/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.UserStories == null)
            {
                return NotFound();
            }

            var userStories = await _context.UserStories.FindAsync(id);
            if (userStories == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userStories.AppUserId);
            return View(userStories);
        }

        // POST: UserStory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AuthorId,UrlPhoto,CreatedAt,Id")] UserStory userStory)
        {
            if (id != userStory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userStory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserStoriesExists(userStory.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userStory.AppUserId);
            return View(userStory);
        }

        // GET: UserStory/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.UserStories == null)
            {
                return NotFound();
            }

            var userStories = await _context.UserStories
                .Include(u => u.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userStories == null)
            {
                return NotFound();
            }

            return View(userStories);
        }

        // POST: UserStory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.UserStories == null)
            {
                return Problem("Entity set 'IAppBll.UserStory'  is null.");
            }
            var userStories = await _context.UserStories.FindAsync(id);
            if (userStories != null)
            {
                _context.UserStories.Remove(userStories);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserStoriesExists(Guid id)
        {
          return (_context.UserStories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
