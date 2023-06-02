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
    public class UserHashtagController : Controller
    {
        private readonly AppDbContext _context;

        public UserHashtagController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserHashtag
        public async Task<IActionResult> Index()
        {
              return _context.UserHashtags != null ? 
                          View(await _context.UserHashtags.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.UserHashtags'  is null.");
        }

        // GET: UserHashtag/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.UserHashtags == null)
            {
                return NotFound();
            }

            var userHashtag = await _context.UserHashtags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userHashtag == null)
            {
                return NotFound();
            }

            return View(userHashtag);
        }

        // GET: UserHashtag/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserHashtag/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HashtagText,CreatedAt,Id")] UserHashtag userHashtag)
        {
            if (ModelState.IsValid)
            {
                userHashtag.Id = Guid.NewGuid();
                _context.Add(userHashtag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userHashtag);
        }

        // GET: UserHashtag/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.UserHashtags == null)
            {
                return NotFound();
            }

            var userHashtag = await _context.UserHashtags.FindAsync(id);
            if (userHashtag == null)
            {
                return NotFound();
            }
            return View(userHashtag);
        }

        // POST: UserHashtag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("HashtagText,CreatedAt,Id")] UserHashtag userHashtag)
        {
            if (id != userHashtag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userHashtag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserHashtagExists(userHashtag.Id))
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
            return View(userHashtag);
        }

        // GET: UserHashtag/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.UserHashtags == null)
            {
                return NotFound();
            }

            var userHashtag = await _context.UserHashtags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userHashtag == null)
            {
                return NotFound();
            }

            return View(userHashtag);
        }

        // POST: UserHashtag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.UserHashtags == null)
            {
                return Problem("Entity set 'AppDbContext.UserHashtags'  is null.");
            }
            var userHashtag = await _context.UserHashtags.FindAsync(id);
            if (userHashtag != null)
            {
                _context.UserHashtags.Remove(userHashtag);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserHashtagExists(Guid id)
        {
          return (_context.UserHashtags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
