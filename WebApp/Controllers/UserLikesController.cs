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
    public class UserLikesController : Controller
    {
        private readonly AppDbContext _context;

        public UserLikesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserLikes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserLikes.Include(u => u.AppUser).Include(u => u.UserPost);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserLikes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.UserLikes == null)
            {
                return NotFound();
            }

            var userLike = await _context.UserLikes
                .Include(u => u.AppUser)
                .Include(u => u.UserPost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLike == null)
            {
                return NotFound();
            }

            return View(userLike);
        }

        // GET: UserLikes/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Firstname");
            ViewData["UserPostId"] = new SelectList(_context.UserPosts, "Id", "Text");
            return View();
        }

        // POST: UserLikes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserPostId,AppUserId,CreatedAt,Id")] UserLike userLike)
        {
            if (ModelState.IsValid)
            {
                userLike.Id = Guid.NewGuid();
                _context.Add(userLike);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userLike.AppUserId);
            ViewData["UserPostId"] = new SelectList(_context.UserPosts, "Id", "Text", userLike.UserPostId);
            return View(userLike);
        }

        // GET: UserLikes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.UserLikes == null)
            {
                return NotFound();
            }

            var userLike = await _context.UserLikes.FindAsync(id);
            if (userLike == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userLike.AppUserId);
            ViewData["UserPostId"] = new SelectList(_context.UserPosts, "Id", "Text", userLike.UserPostId);
            return View(userLike);
        }

        // POST: UserLikes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserPostId,AppUserId,CreatedAt,Id")] UserLike userLike)
        {
            if (id != userLike.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userLike);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLikeExists(userLike.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Firstname", userLike.AppUserId);
            ViewData["UserPostId"] = new SelectList(_context.UserPosts, "Id", "Text", userLike.UserPostId);
            return View(userLike);
        }

        // GET: UserLikes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.UserLikes == null)
            {
                return NotFound();
            }

            var userLike = await _context.UserLikes
                .Include(u => u.AppUser)
                .Include(u => u.UserPost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userLike == null)
            {
                return NotFound();
            }

            return View(userLike);
        }

        // POST: UserLikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.UserLikes == null)
            {
                return Problem("Entity set 'AppDbContext.UserLikes'  is null.");
            }
            var userLike = await _context.UserLikes.FindAsync(id);
            if (userLike != null)
            {
                _context.UserLikes.Remove(userLike);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLikeExists(Guid id)
        {
          return (_context.UserLikes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
