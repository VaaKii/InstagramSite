using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Controllers
{
    public class UserStoriesController : Controller
    {
        private readonly AppDbContext _context;

        public UserStoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserStories
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserStories.Include(u => u.AppUser);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserStories/Details/5
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

        // GET: UserStories/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Firstname");
            return View();
        }

        // POST: UserStories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,UrlPhoto,CreatedAt,Id")] UserStories userStories)
        {
            if (ModelState.IsValid)
            {
                userStories.Id = Guid.NewGuid();
                _context.Add(userStories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userStories.AppUserId);
            return View(userStories);
        }

        // GET: UserStories/Edit/5
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
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userStories.AppUserId);
            return View(userStories);
        }

        // POST: UserStories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,UrlPhoto,CreatedAt,Id")] UserStories userStories)
        {
            if (id != userStories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userStories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserStoriesExists(userStories.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userStories.AppUserId);
            return View(userStories);
        }

        // GET: UserStories/Delete/5
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

        // POST: UserStories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.UserStories == null)
            {
                return Problem("Entity set 'AppDbContext.UserStories'  is null.");
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
