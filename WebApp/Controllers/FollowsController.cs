using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain;

namespace WebApp.Controllers
{
    public class FollowsController : Controller
    {
        private readonly IAppBll _context;

        public FollowsController(IAppBll context)
        {
            _context = context;
        }

        // GET: Follows
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Follows.Include(f => f.AppUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Follows/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Follows == null)
            {
                return NotFound();
            }

            var follow = await _context.Follows
                .Include(f => f.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (follow == null)
            {
                return NotFound();
            }

            return View(follow);
        }

        // GET: Follows/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname");
            return View();
        }

        // POST: Follows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,CreatedAt,Id")] Follow follow)
        {
            if (ModelState.IsValid)
            {
                follow.Id = Guid.NewGuid();
                _context.Add(follow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname", follow.AppUserId);
            return View(follow);
        }

        // GET: Follows/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Follows == null)
            {
                return NotFound();
            }

            var follow = await _context.Follows.FindAsync(id);
            if (follow == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname", follow.AppUserId);
            return View(follow);
        }

        // POST: Follows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AuthorId,CreatedAt,Id")] Follow follow)
        {
            if (id != follow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(follow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowExists(follow.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "Id", "Firstname", follow.AppUserId);
            return View(follow);
        }

        // GET: Follows/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Follows == null)
            {
                return NotFound();
            }

            var follow = await _context.Follows
                .Include(f => f.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (follow == null)
            {
                return NotFound();
            }

            return View(follow);
        }

        // POST: Follows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Follows == null)
            {
                return Problem("Entity set 'IAppBll.Follows'  is null.");
            }
            var follow = await _context.Follows.FindAsync(id);
            if (follow != null)
            {
                _context.Follows.Remove(follow);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FollowExists(Guid id)
        {
          return (_context.Follows?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
